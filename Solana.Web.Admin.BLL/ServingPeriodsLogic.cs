using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.ServingPeriods;
using Solana.Web.Admin.Models.Responses.ServingPeriods;
using Solana.Web.Admin.Models.Responses.ServingPeriods.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class ServingPeriodsLogic : IServingPeriodsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public ServingPeriodsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }
        
        public async Task<GetServingPeriodsResponse> GetServingPeriods()
        {
            return new GetServingPeriodsResponse
            {
                ServingPeriods = (await _repository.GetListAsync<AdmServingPeriod>())
                    .OrderBy(x => x.DisplayOrder)
                    .Select(x => new ServingPeriod()
                    {
                        AdmServingPeriodId = x.AdmServingPeriodID,
                        Description = x.Description,
                        DisplayOrder = x.DisplayOrder,
                        PeriodType = x.PeriodType,
                        ConversionFactor = x.ConversionFactor,
                        Abbreviation = x.Abbreviation,
                        MenuAnalysisType = x.MenuAnalysisType,
                        AssignedSiteIDs = x.AdmSitesServingPeriods.Select(p => p.AdmSiteID).ToList()
                    })
                    .ToList()
            };
        }


        public async Task<PutServingPeriodsResponse> SaveServingPeriods(PutServingPeriodsRequest request)
        {
            if (request.Periods == null)
                return new PutServingPeriodsResponse { Success = false, Error = string.Empty };
            request.Periods.Where(p => p.Description != null).ToList().ForEach(x => x.Description = x.Description.Trim());
            request.Periods.Where(p => p.Abbreviation != null).ToList().ForEach(x => x.Abbreviation = x.Abbreviation.Trim());

            if (request.Periods.Any(p => string.IsNullOrEmpty(p.Description)))
            {
                return new PutServingPeriodsResponse
                {
                    Success = false,
                    Error = "Description is required.  Please check the data and try again."
                };
            }

            if (request.Periods.Any(p => string.IsNullOrEmpty(p.Abbreviation)))
            {
                return new PutServingPeriodsResponse
                {
                    Success = false,
                    Error = "Abbreviation is required.  Please check the data and try again."
                };
            }

            //validate duplicates
            var dupDescriptions = request.Periods.GroupBy(g => g.Description)
                                            .Where(c => c.Count() > 1)
                                            .Select(g => g.Key)
                                            .ToList();
            if (dupDescriptions.Count > 0)
            {
                StringBuilder error = new StringBuilder();
                foreach (string desc in dupDescriptions)
                {
                    if (error.Length > 0) error.Append(", ");
                    error.Append(desc);
                }
                return new PutServingPeriodsResponse
                {
                    Success = false,
                    Error =
                        $"Duplicate Description{(dupDescriptions.Count == 1 ? string.Empty : "s")} detected: {error}.  Please check the data and try again."
                };
            }

            dupDescriptions = request.Periods.GroupBy(g => g.Abbreviation)
                                            .Where(c => c.Count() > 1)
                                            .Select(g => g.Key)
                                            .ToList();
            if (dupDescriptions.Count > 0)
            {
                StringBuilder error = new StringBuilder();
                foreach (string desc in dupDescriptions)
                {
                    if (error.Length > 0) error.Append(", ");
                    error.AppendLine(desc);
                }
                return new PutServingPeriodsResponse
                {
                    Success = false,
                    Error =
                        $"Duplicate Abbreviation{(dupDescriptions.Count == 1 ? string.Empty : "s")} detected: {error}.  Please check the data and try again."
                };
            }


            var dataPeriods = await _repository.GetListAsync<AdmServingPeriod>();

            // have to re-sync after deletions
            int position = 1;
            foreach (ServingPeriod pos in request.Periods)
            {
                pos.DisplayOrder = position++;
            }

            foreach (var p in request.Periods)
            {
                var editPeriod = p.AdmServingPeriodId == 0 ? new AdmServingPeriod() : dataPeriods.First(x => x.AdmServingPeriodID == p.AdmServingPeriodId);
                editPeriod.Description = p.Description;
                editPeriod.DisplayOrder = p.DisplayOrder;
                editPeriod.PeriodType = p.PeriodType;
                editPeriod.ConversionFactor = p.ConversionFactor;
                editPeriod.Abbreviation = p.Abbreviation;
                editPeriod.MenuAnalysisType = p.MenuAnalysisType;

                //delete periods
                for (int i = editPeriod.AdmSitesServingPeriods.Count - 1; i >= 0; i--)
                {
                    AdmSitesServingPeriod checkPeriod = editPeriod.AdmSitesServingPeriods.ElementAt(i);
                    if (!p.AssignedSiteIDs.Contains(checkPeriod.AdmSiteID)) editPeriod.AdmSitesServingPeriods.Remove(checkPeriod);
                }
                //add new periods
                foreach (var siteId in p.AssignedSiteIDs)
                {
                    if (editPeriod.AdmSitesServingPeriods.FirstOrDefault(x => x.AdmSiteID == siteId) == null)
                    {
                        editPeriod.AdmSitesServingPeriods.Add(new AdmSitesServingPeriod { AdmServingPeriodID = editPeriod.AdmServingPeriodID, AdmSiteID = siteId });
                    }
                }
                if (p.AdmServingPeriodId == 0) await _repository.CreateAsync(editPeriod);
            }

            await _repository.UpdateAsync<AdmServingPeriod>(dataPeriods);

            return new PutServingPeriodsResponse { Success = true, Periods = request.Periods };
        }

        public async Task<bool> DeleteServingPeriods(int id)
        {
            if (id == 0) return false;
            await _repository.DeleteAsync(await _repository.FindAsync<AdmServingPeriod>(id));
            return true;
        }
    }
}
