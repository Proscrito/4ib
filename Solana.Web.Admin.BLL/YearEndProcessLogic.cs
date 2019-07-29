using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Far;
using Horizon.Common.Repository.Legacy.Models.Pos;
using Horizon.Common.Repository.Legacy.Models.View;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.YearEndProcess;
using Solana.Web.Admin.Models.Responses.YearEndProcess;
using Solana.Web.Admin.Models.Responses.YearEndProcess.NestedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solana.Web.Admin.BLL
{
    public class YearEndProcessLogic : IYearEndProcessLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public YearEndProcessLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<GetYearEndProcessSetUpOptionsResponse> GetYearEndProcessSetUpOptions(int admSiteId)
        {
            var today = _repository.GetSchoolNow(admSiteId);
            int currentSchoolYear = today.Year;

            var options = (await _repository.GetListAsync<AdmYearEndProcessOptions>(p =>
                p.OptionsForTheYear == currentSchoolYear)).FirstOrDefault();

            var model = options == null
                ? await MapFromAdmSitesOption(admSiteId)
                : _autoMapper.Map<GetYearEndProcessSetUpOptionsResponse>(options);

            model.hasYearRoundSchool = await IsYearRoundSchoolExist();

            return model;
        }

        public async Task<LoadSchoolsResponse> LoadSchools(bool isRolloverDone)
        {
            var grades = await _repository.GetListAsync<PosGrade>();
            var schools = await _repository.GetListAsync<AdmSite>();

            return new LoadSchoolsResponse
            {
                Grades = _autoMapper.Map<ICollection<PosGradeModel>>(grades),
                Schools = _autoMapper.Map<ICollection<AdmSiteModel>>(schools)
            };
        }

        public LoadEndOfYearPreviewResponse LoadEndOfYearPreview(LoadEndOfYearPreviewRequest request)
        {
            const string yes = "Yes";
            const string no = "No";
            const string notApplicable = "n/a";
            const string carryOverAllBalances = "Carry over all balances";
            const string setNegativeBalanceToZero = "Set negative balances to zero";

            return new LoadEndOfYearPreviewResponse
            {
                PromoteStudents = request.IsPromoteStudents ? yes : no,
                PurgeGrads = request.IsPurgeGraduates ? yes : no,
                PurgeMembers = request.IsPurgeHHMembers ? yes : no,
                BalanceOption = (request.AccountBalanceOption == 0)
                        ? carryOverAllBalances
                        : setNegativeBalanceToZero,
                SchoolStart = string.IsNullOrWhiteSpace(request.DefaultStartDate) ? notApplicable : request.DefaultStartDate,
                SchoolEnd = string.IsNullOrWhiteSpace(request.DefaultEndDate) ? notApplicable : request.DefaultEndDate,
                Expiration = string.IsNullOrWhiteSpace(request.DefaultTempStatusExpDate) ? notApplicable : request.DefaultTempStatusExpDate,
                BreakupDC = request.IsBreakupDCHouseholds ? yes : no,
                StartingAppNumber = Convert.ToString(request.StartingAppNumber)
            };
        }

        public GetTotalPreviewResponse GetTotalPreview(GetTotalPreviewRequest request)
        {
            var previewTotals = _repository
                .StoredProcList<TotalPreviewModel>(StoredProcs.usp_EOYTotalCounts, request)
                .ToList();
            return new GetTotalPreviewResponse { Items = previewTotals };
        }

        public GetEndOfYearEligibilityCountsResponse GetEndOfYearEligibilityCounts(GetEndOfYearEligibilityCountsRequest request)
        {
            var eligibilityCounts = _repository
                .StoredProcList<EligibilityCountModel>(StoredProcs.usp_EOYEligibilityCounts, request)
                .ToList();
            return new GetEndOfYearEligibilityCountsResponse { Items = eligibilityCounts };
        }

        public GetSchoolPreviewResponse GetSchoolPreview(GetSchoolPreviewRequest request)
        {
            var countsBySchool = _repository
                .StoredProcList<SchoolPreviewModel>(StoredProcs.usp_EOYCountsBySchool, request)
                .ToList();
            return new GetSchoolPreviewResponse { Items = countsBySchool };
        }

        public async Task<GetGradeSitePromotionsResponse> GetGradeSitePromotions()
        {
            var gradeSitePromotions = await _repository.GetListAsync<view_AdmGradeSitePromotion>();
            return new GetGradeSitePromotionsResponse
            {
                Items = _autoMapper.Map<ICollection<GradeSitePromotionModel>>(gradeSitePromotions)
            };
        }

        public async Task<GetSiteAlternativeDatesResponse> GetSiteAlternativeDates(GetSiteAlternativeDatesRequest request)
        {
            var siteAlternativeDates = (await _repository.GetListAsync<view_AdmSiteAlternativeDates>())
                .Select(p => new SiteAlternativeDateModel()
                {
                    AdmSiteID = p.AdmSiteID,
                    SiteID = p.SiteID,
                    SiteDescription = p.SiteDescription,
                    SchoolAlterStartDate = (p.SchoolAltStartDate ?? request.StartDate).ToUsDateString(),
                    SchoolTempStatusExpDate = (p.SchoolTempStatusExpDate ?? request.TempExpDate).ToUsDateString()
                })
                .ToList();
            return new GetSiteAlternativeDatesResponse
            {
                Items = siteAlternativeDates
            };
        }

        public async Task<GetSiteStartDatesPreviewResponse> GetSiteStartDatesPreview(GetSiteStartDatesPreviewRequest request)
        {
            var siteStartDates = (await _repository.GetListAsync<view_AdmSiteAlternativeDates>())
                .Select(p => new StartDatePreviewModel()
                {
                    AdmSiteID = p.AdmSiteID,
                    IDDescCombined = $"{p.SiteID} {p.SiteDescription}",
                    Start = (p.SchoolAltStartDate ?? request.StartDate).ToUsDateString(),
                    Expiration = (p.SchoolTempStatusExpDate ?? request.TempExpDate).ToUsDateString()
                })
                .ToList();
            return new GetSiteStartDatesPreviewResponse
            {
                Items = siteStartDates
            };
        }

        public async Task<GetSchoolsListResponse> GetSchoolsList()
        {
            var sites = await _repository.GetListAsync<AdmSite>(s => s.IsActive);
            return new GetSchoolsListResponse
            {
                Items = _autoMapper.Map<ICollection<EndOfYearSchoolItemModel>>(sites)
            };
        }

        private async Task<bool> IsYearRoundSchoolExist()
        {
            var yearRoundSchool = await _repository.GetListAsync<AdmSitesOption>(o => o.IsYearRoundSchool);
            return yearRoundSchool.Any();
        }

        public string GetTempStatusExpDate(DateTime startDate)
        {
            DateTime tempStatusExpDate = _repository.ExecuteScalar<DateTime>(
                $"Exec dbo.usp_AdmGetTempStatusExpDate '{startDate.ToStoredProcDateString()}', 29");

            return tempStatusExpDate.ToUsDateString();
        }

        public async Task<SaveGradeSitePromotionsResponse> SaveGradeSitePromotions(SaveGradeSitePromotionsRequest request)
        {
            var toSave = new List<AdmGradeSitePromotion>();
            var promotions = _repository.GetQueryable<AdmGradeSitePromotion>().ToList();
            await _repository.DeleteAsync<AdmGradeSitePromotion>(promotions);

            var modelsList = request.Promotions.Where(l => l.PromotingPosGradeID.HasValue && l.PromotingPosGradeID != 0);

            foreach (var model in modelsList)
            {
                DateTime today = _repository.GetSchoolNow(model.AdmSiteID);
                var site = promotions.FirstOrDefault(s => s.AdmSiteID == model.AdmSiteID);
                if (site == null)
                {
                    site = new AdmGradeSitePromotion()
                    {
                        AdmSiteID = model.AdmSiteID,
                        PosGradeID = model.PromotingPosGradeID ?? 0,
                        LastUpdatedBy = request.AdmSiteId,
                        LastUpdatedDate = today
                    };
                }

                if (site.PosGradeID != model.PromotingPosGradeID || site.PromoteToAdmSiteID != model.PromoteToAdmSiteID)
                {
                    site.LastUpdatedBy = request.AdmSiteId;
                    site.LastUpdatedDate = today;
                }

                site.PosGradeID = model.PromotingPosGradeID ?? 0;
                site.PromoteToAdmSiteID = model.PromoteToAdmSiteID;
                toSave.Add(site);
            }

            var createdPromotions = await _repository.CreateAsync<AdmGradeSitePromotion>(toSave);

            return new SaveGradeSitePromotionsResponse
            {
                Items = _autoMapper.Map<ICollection<SavedSitePromotionModel>>(createdPromotions)
            };
        }

        public async Task<SaveYearEndProcessSetupResponse> SaveYearEndProcessSetup(SaveYearEndProcessSetupRequest request)
        {
            var newOptions = new AdmYearEndProcessOptions();
            DateTime today = _repository.GetSchoolNow(request.AdmUserId);
            if (request.DefaultTempStatusExpDate == null)
            {
                request.DefaultTempStatusExpDate = today.ToUsDateString();
            }

            var options =
                await _repository.GetListAsync<AdmYearEndProcessOptions>(o => o.OptionsForTheYear == request.OptionsForTheYear);

            if (options.Any())
            {
                newOptions = await _repository.FindAsync<AdmYearEndProcessOptions>(request.OptionsForTheYear);
                _autoMapper.Map(request, newOptions);
                InitNewOption(request, newOptions, today);
                newOptions = _repository.Update(newOptions);
            }
            else
            {
                _autoMapper.Map(request, newOptions);
                InitNewOption(request, newOptions, today);
                newOptions = _repository.Create<AdmYearEndProcessOptions>(newOptions);
            }

            await UpdateSchoolDates(request.DefaultStartDate, request.DefaultEndDate, request.DefaultTempStatusExpDate);

            var farOptions = (await _repository.GetListAsync<FarOption>()).FirstOrDefault();
            if (farOptions != null)
            {
                farOptions.StartingAppNumber = request.StartingAppNumber;
                farOptions = await _repository.UpdateAsync(farOptions);
            }

            return _autoMapper.Map<SaveYearEndProcessSetupResponse>(request);
        }

        public async Task SaveSchoolAlternativeStartDates(SaveSchoolAlternativeStartDatesRequest request)
        {
            foreach (var alternativeStartDate in request.AlternativeStartDates)
            {
                var siteToUpdate = await _repository.FindAsync<AdmSitesOption>(alternativeStartDate.AdmSiteID);

                siteToUpdate.SchoolAltStartDate = alternativeStartDate.SchoolAlterStartDate.ParseUsDate();
                siteToUpdate.SchoolStartDate = alternativeStartDate.SchoolAlterStartDate.ParseUsDate();
                siteToUpdate.SchoolTempStatusExpDate = alternativeStartDate.SchoolTempStatusExpDate.ParseUsDate();

                await _repository.UpdateAsync<AdmSitesOption>(siteToUpdate);
            }
        }

        public int ExecuteEndOfYear(int schoolYear)
        {
            return _repository.ExecuteStoredProc(
                StoredProcs.usp_EOYStartProcess,
                new { SchoolYear = schoolYear });
        }

        private async Task UpdateSchoolDates(string startDate, string endDate, string expDate)
        {
            var siteOption = await _repository.GetListAsync<AdmSitesOption>(t => !t.IsYearRoundSchool);

            foreach (var site in siteOption)
            {
                int siteId = site.AdmSiteID;

                var updateSite = await _repository.FindAsync<AdmSitesOption>(siteId);

                if (!string.IsNullOrEmpty(startDate)) updateSite.SchoolStartDate = startDate.ParseUsDateExact();
                if (!string.IsNullOrEmpty(endDate)) updateSite.SchoolEndDate = endDate.ParseUsDateExact();

                updateSite.SchoolTempStatusExpDate = Convert.ToDateTime(expDate);

                await _repository.UpdateAsync(updateSite);
            }
        }

        private void InitNewOption(SaveYearEndProcessSetupRequest request, AdmYearEndProcessOptions newOptions, DateTime today)
        {
            newOptions.DefaultStartDate = request.DefaultStartDate.ParseUsDateExact();
            newOptions.DefaultEndDate = request.DefaultEndDate.ParseUsDateExact();
            newOptions.DefaultStartAltDate = today;
            newOptions.DefaultTempStatusExpDate = request.DefaultTempStatusExpDate.ParseUsDateExact();
            newOptions.LastUpdatedBy = request.AdmUserId;
            newOptions.LastUpdatedDate = today;
        }

        private async Task<GetYearEndProcessSetUpOptionsResponse> MapFromAdmSitesOption(int admSiteId)
        {
            var site = await _repository.GetAsync<AdmSite>(s => s.IsDistrict);
            var today = _repository.GetSchoolNow(admSiteId);
            int currentSchoolYear = today.Year;

            return new GetYearEndProcessSetUpOptionsResponse
            {
                OptionsForTheYear = currentSchoolYear,
                DefaultStartAltDate = site.AdmSitesOption.SchoolAltStartDate.ToUsDateString(),
                DefaultStartDate = site.AdmSitesOption.SchoolStartDate.ToUsDateString(),
                DefaultEndDate = site.AdmSitesOption.SchoolEndDate.ToUsDateString(),
                DefaultTempStatusExpDate = site.AdmSitesOption.SchoolTempStatusExpDate.ToUsDateString()
            };
        }
    }
}
