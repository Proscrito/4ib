using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Common;
using Horizon.Common.Repository.Legacy.Models.Fro;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.OnlineApplication;
using Solana.Web.Admin.Models.Requests.OnlineApplication.NestedModels;
using Solana.Web.Admin.Models.Responses.OnlineApplications;
using Solana.Web.Admin.Models.Responses.OnlineApplications.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class OnlineApplicationsLogic : IOnlineApplicationsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public OnlineApplicationsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<GetAdmFroOptionsResponse> GetAdmFroOptions()
        {
            var admFroOptions = await _repository.GetListAsync<AdmFroOption>();
            var admFroOption = admFroOptions.FirstOrDefault();

            if (admFroOption == null)
            {
                Debug.WriteLine($"{nameof(OnlineApplicationsLogic)}.{nameof(GetAdmFroOptions)} -> No AdmFroOption found in database");
                throw new ApplicationException("No AdmFroOption found");
            }

            var response = _autoMapper.Map<GetAdmFroOptionsResponse>(admFroOption);
            response.Customizations = await GetCustomizationList();
            return response;
        }

        public async Task SaveAdmFroOptions(PutAdmFroOptionsRequest request)
        {
            var admFroOptions = await _repository.GetListAsync<AdmFroOption>();
            var admFroOption = admFroOptions.FirstOrDefault();

            if (admFroOption == null)
            {
                Debug.WriteLine($"{nameof(OnlineApplicationsLogic)}.{nameof(SaveAdmFroOptions)} -> No AdmFroOption found in database");
                throw new ApplicationException("No AdmFroOption found");
            }

            admFroOption.SupportsSnap = request.SupportsSnap;
            admFroOption.SupportsTANF = request.SupportsTANF;
            admFroOption.SupportsFDPIR = request.SupportsFDPIR;
            admFroOption.DisclosureConsent = request.DisclosureConsent;
            admFroOption.DisplayFoster = request.DisplayFoster;
            admFroOption.DisplayHomeless = request.DisplayHomeless;
            admFroOption.DisplayMigrant = request.DisplayMigrant;
            admFroOption.DisplayRunaway = request.DisplayRunaway;
            admFroOption.HideDistrict = request.HideDistrict;

            if (request.SupportsSnap)
            {
                admFroOption.ProgramSnap = request.ProgramSnap ?? string.Empty;
                admFroOption.DescriptionSnap = request.DescriptionSnap ?? string.Empty;
                admFroOption.StartsWithSnap = request.StartsWithSnap ?? string.Empty;
                admFroOption.LengthSnap = request.LengthSnap;
                admFroOption.MinLengthSNAP = request.MinLengthSNAP;
                admFroOption.MaxLengthSNAP = request.MaxLengthSNAP;
            }

            if (request.SupportsTANF)
            {
                admFroOption.ProgramTANF = request.ProgramTANF ?? string.Empty;
                admFroOption.DescriptionTANF = request.DescriptionTANF ?? string.Empty;
                admFroOption.StartsWithTANF = request.StartsWithTANF ?? string.Empty;
                admFroOption.LengthTANF = request.LengthTANF;
                admFroOption.MinLengthTANF = request.MinLengthTANF;
                admFroOption.MaxLengthTANF = request.MaxLengthTANF;
            }

            if (request.SupportsFDPIR)
            {
                admFroOption.ProgramFDPIR = request.ProgramFDPIR ?? string.Empty;
                admFroOption.DescriptionFDPIR = request.DescriptionFDPIR ?? string.Empty;
                admFroOption.StartsWithFDPIR = request.StartsWithFDPIR ?? string.Empty;
                admFroOption.LengthFDPIR = request.LengthFDPIR;
                admFroOption.MinLengthFDPIR = request.MinLengthFDPIR;
                admFroOption.MaxLengthFDPIR = request.MaxLengthFDPIR;
            }

            await _repository.UpdateAsync(admFroOption);
            await SaveCustomDisclosures(request.Customizations);
        }

        private async Task SaveCustomDisclosures(ICollection<FroCustomizationSaveModel> requestCustomizations)
        {
            // save the disclaimer actives
            foreach (var saveModel in requestCustomizations)
            {
                var custom = await _repository.FindAsync<FroCustomization>(saveModel.FroCustomizationID);
                // should technically always find this, but just in case
                if (custom != null)
                {
                    // only update if there was a change
                    if (custom.IsActive != saveModel.IsActive)
                    {
                        custom.IsActive = saveModel.IsActive;
                        await _repository.UpdateAsync(custom);
                    }
                }
            }
        }

        private async Task<List<FroCustomizationViewModel>> GetCustomizationList()
        {
            // get the current year to see if we want to show these
            var currentYear = _repository.ExecuteScalar<int>("EXEC usp_PosGetCurrentArchiveYear");

            var froCustomizations = await _repository
                .GetListAsync<FroCustomization>(x => (x.AppYear >= currentYear || !x.AppYear.HasValue) && x.Type == (int)FroCustomizationType.Checkbox);

            var froCustomizationsSorted = froCustomizations.OrderBy(x => x.Page).ThenBy(x => x.SortOrder);
            return _autoMapper.Map<List<FroCustomizationViewModel>>(froCustomizationsSorted);
        }
    }
}
