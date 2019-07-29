using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Common;
using Horizon.Common.Repository.Legacy.Models.Far;
using Horizon.Common.Repository.Legacy.Models.Inv;
using Horizon.Common.Repository.Legacy.Models.Men;
using Horizon.Common.Repository.Legacy.Models.Pos;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.GlobalOptions;
using Solana.Web.Admin.Models.Requests.GlobalOptions.NestedModels;
using Solana.Web.Admin.Models.Responses.GlobalOptions;
using Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels;
using CEPSchoolOptionsModel = Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels.CEPSchoolOptionsModel;
using MenLeftoverCodeModel = Solana.Web.Admin.Models.Responses.GlobalOptions.NestedModels.MenLeftoverCodeModel;

namespace Solana.Web.Admin.BLL
{
    public class GlobalOptionsLogic : IGlobalOptionsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public GlobalOptionsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<GetAdmGlobalOptionsResponse> GetAdmGlobalOptions()
        {
            var admGlobalOptions = await _repository.GetListAsync<AdmGlobalOption>();

            if (!admGlobalOptions.Any())
            {
                Debug.WriteLine("No admin options found in database.");
                throw new ApplicationException("No admin options found");
            }

            var result = _autoMapper.Map<GetAdmGlobalOptionsResponse>(admGlobalOptions.FirstOrDefault());

            var farOptions = await _repository.GetListAsync<FarOption>();

            if (farOptions.Any())
            {
                _autoMapper.Map(farOptions.FirstOrDefault(), result);
            }

            result.CepSchoolOptionsItemList.AddRange(GetCepSchoolOptions());

            return result;
        }

        public async Task<PutAdmGlobalOptionsResponse> SaveAdmGlobalOption(PutAdmGlobalOptionsRequest request)
        {
            var admGlobalOptions = await _repository.GetListAsync<AdmGlobalOption>();
            var farOptions = await _repository.GetListAsync<FarOption>();

            if (!admGlobalOptions.Any())
            {
                Debug.WriteLine("No admin options found in database.");
                throw new ApplicationException("No admin options found");
            }

            if (!farOptions.Any())
            {
                Debug.WriteLine("No FAR options found in database.");
                throw new ApplicationException("No FAR options found");
            }

            var admGlobalOption = admGlobalOptions.First();
            var farOption = farOptions.First();

            if (admGlobalOption.SodiumTargetId != request.SodiumTargetId)
            {
                UpdateSodiumComplianceLevels(request.SodiumTargetId);
            }

            _autoMapper.Map(request, admGlobalOption);
            _autoMapper.Map(request, farOption);

            await _repository.UpdateAsync(admGlobalOption);
            await _repository.UpdateAsync(farOption);

            //await SaveAgeGroups(request.AgeGroups);

            return new PutAdmGlobalOptionsResponse
            {
                AdmGlobalOptionId = admGlobalOption.AdmGlobalOptionsID,
                FarOptionId = farOption.FarOptionID
            };
        }

        public async Task<List<MenAgeGroupsModel>> GetMenAgeGroups()
        {
            var menAgeGroups = await _repository.GetListAsync<MenAgeGroups>();
            var list = menAgeGroups
                .OrderBy(x => x.FromAge)
                .ThenBy(x => x.ToAge);

            return _autoMapper.Map<List<MenAgeGroupsModel>>(list);
        }

        public async Task<bool> GetIsGenerateAppNumbers()
        {
            var farOptions = await _repository.GetListAsync<FarOption>();

            if (!farOptions.Any())
            {
                Debug.WriteLine("No FAR options found in database.");
                throw new ApplicationException("No FAR options found");
            }

            var farOption = farOptions.First();
            return farOption.GenerateAppNumbers;
        }

        public async Task<List<PosReasonModel>> GetPosReasons()
        {
            var posReasons = await _repository.GetListAsync<PosReason>();
            return _autoMapper.Map<List<PosReasonModel>>(posReasons);
        }

        public async Task DeletePosReason(int id)
        {
            var posReason = await _repository.FindAsync<PosReason>(id);

            if (posReason != null)
            {
                await _repository.DeleteAsync(posReason);
            }
        }

        public async Task<bool> GetIsReasonExist(string reason)
        {
            var posReason = await _repository.GetAsync<PosReason>(x => x.Reason == reason.Trim().ToLower());
            return posReason != null;
        }

        public async Task<int> SavePosReason(string reason)
        {
            var posReason = await _repository.GetAsync<PosReason>(x => x.Reason == reason.Trim().ToLower());

            if (posReason != null)
            {
                posReason.IsDeleted = false;
                await _repository.UpdateAsync(posReason);
            }
            else
            {
                posReason = await _repository.CreateAsync(new PosReason
                {
                    Reason = reason
                });
            }

            return posReason.PosReasonID;
        }

        public async Task<List<MenLeftoverCodeModel>> GetMenLeftoverCodes()
        {
            var menLeftoverCodes = await _repository.GetListAsync<MenLeftoverCodes>();
            return _autoMapper.Map<List<MenLeftoverCodeModel>>(menLeftoverCodes);
        }

        public async Task<List<InvTransactionTypeModel>> GetInvTransactionTypes()
        {
            var predefinedItemsIds = new[] { 2, 3, 10 };

            var invTransactionTypes = await _repository.GetListAsync<InvTransactionTypes>(x =>
                predefinedItemsIds.Contains(x.InvTransactionTypeID) || x.IsUserDefined && !x.IsDeleted);

            return _autoMapper.Map<List<InvTransactionTypeModel>>(invTransactionTypes);
        }

        public async Task DeleteMenLeftoverCode(int id)
        {
            var menLeftoverCode = await _repository.FindAsync<MenLeftoverCodes>(id);

            if (menLeftoverCode != null)
            {
                await _repository.DeleteAsync(menLeftoverCode);
            }
        }

        public async Task<List<int>> SaveMenLeftoverCodes(IEnumerable<MenLeftoverCodeSaveModel> menLeftoverCodeModels)
        {
            var result = new List<int>();

            foreach (var menLeftoverCodeModel in menLeftoverCodeModels)
            {
                var menLeftoverCode = _autoMapper.Map<MenLeftoverCodes>(menLeftoverCodeModel);

                if (menLeftoverCodeModel.MenLeftoverCodeID > 0)
                {
                    await _repository.UpdateAsync(menLeftoverCode);
                }
                else
                {
                    await _repository.CreateAsync(menLeftoverCode);
                }

                result.Add(menLeftoverCode.MenLeftoverCodeID);
            }

            return result;
        }

        public async Task<List<int>> SaveInvTransactionTypes(IEnumerable<InvTransactionTypeSaveModel> invTransactionTypeModels)
        {
            var result = new List<int>();

            foreach (var invTransactionTypeModel in invTransactionTypeModels)
            {
                var invTransactionType = _autoMapper.Map<InvTransactionTypes>(invTransactionTypeModel);

                if (invTransactionType.InvTransactionTypeID > 0)
                {
                    await _repository.UpdateAsync(invTransactionType);
                }
                else
                {
                    await _repository.CreateAsync(invTransactionType);
                }

                result.Add(invTransactionType.InvTransactionTypeID);
            }

            return result;
        }

        public async Task DeleteInvTransactionType(int id)
        {
            var invTransactionType = await _repository.FindAsync<InvTransactionTypes>(id);

            if (invTransactionType != null)
            {
                await _repository.DeleteAsync(invTransactionType);
            }
        }

        private async Task SaveAgeGroups(IReadOnlyCollection<AgeGroupsViewSaveModel> requestAgeGroups)
        {
            var ids = requestAgeGroups.Select(x => x.MenAgeGroupID).ToList();
            var ageGroups = await _repository.GetListAsync<MenAgeGroups>(x => ids.Contains(x.MenAgeGroupID));

            foreach (var ageGroup in requestAgeGroups)
            {
                var dbAgeGroup = ageGroups.FirstOrDefault(x => x.MenAgeGroupID == ageGroup.MenAgeGroupID);

                if (dbAgeGroup != null)
                {
                    dbAgeGroup.IsActive = ageGroup.IsActive;
                }
            }

            _repository.SaveChanges();
        }

        public void UpdateSodiumComplianceLevels(int levelId)
        {
            #region I do not understand a single line
            var elementaryBreakfasts = _repository.GetQueryable<MenAgeGroupStandard>(
                        c => c.MenAgeGroup.FromGrade == "k" && c.MenAgeGroup.ToGrade == "5" &&
                        c.ServingPeriodType == 1 && c.MenNutrient.NutrientCode == 307);

            var middleSchoolBreakfasts = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "6" && c.MenAgeGroup.ToGrade == "8" &&
                c.ServingPeriodType == 1 && c.MenNutrient.NutrientCode == 307);

            var highSchoolBreakfasts = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "9" && c.MenAgeGroup.ToGrade == "12"
                && c.ServingPeriodType == 1 && c.MenNutrient.NutrientCode == 307);

            var k8Breakfasts = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "K" && c.MenAgeGroup.ToGrade == "8"
                     && c.ServingPeriodType == 1 && c.MenNutrient.NutrientCode == 307);

            var elementaryLunches = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "k" && c.MenAgeGroup.ToGrade == "5" &&
                c.ServingPeriodType == 2 && c.MenNutrient.NutrientCode == 307);

            var middleSchoolLunches = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "6" && c.MenAgeGroup.ToGrade == "8" &&
                c.ServingPeriodType == 2 && c.MenNutrient.NutrientCode == 307);

            var highSchoolLunches = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "9" && c.MenAgeGroup.ToGrade == "12" &&
                c.ServingPeriodType == 2 && c.MenNutrient.NutrientCode == 307);

            var k8Lunches = _repository.GetQueryable<MenAgeGroupStandard>(
                c => c.MenAgeGroup.FromGrade == "K" && c.MenAgeGroup.ToGrade == "8"
                     && c.ServingPeriodType == 2 && c.MenNutrient.NutrientCode == 307);

            switch (levelId)
            {
                //Update to target 1 values
                case 1:

                    foreach (var elementaryBreakfast in elementaryBreakfasts)
                    {
                        elementaryBreakfast.MaxWeeklyValue = 540;
                    }

                    foreach (var middleSchoolBreakfast in middleSchoolBreakfasts)
                    {
                        middleSchoolBreakfast.MaxWeeklyValue = 600;
                    }

                    foreach (var highSchoolBreakfast in highSchoolBreakfasts)
                    {
                        highSchoolBreakfast.MaxWeeklyValue = 640;
                    }

                    foreach (var k8Breakfast in k8Breakfasts)
                    {
                        k8Breakfast.MaxWeeklyValue = 540;
                    }


                    foreach (var elementaryLunch in elementaryLunches)
                    {
                        elementaryLunch.MaxWeeklyValue = 1230;
                    }

                    foreach (var middleSchoolLunch in middleSchoolLunches)
                    {
                        middleSchoolLunch.MaxWeeklyValue = 1360;
                    }

                    foreach (var highSchoolLunch in highSchoolLunches)
                    {
                        highSchoolLunch.MaxWeeklyValue = 1420;
                    }

                    foreach (var k8Lunch in k8Lunches)
                    {
                        k8Lunch.MaxWeeklyValue = 1230;
                    }


                    break;

                //Update to target 2 values
                case 2:

                    foreach (var elementaryBreakfast in elementaryBreakfasts)
                    {
                        elementaryBreakfast.MaxWeeklyValue = 485;
                    }

                    foreach (var middleSchoolBreakfast in middleSchoolBreakfasts)
                    {
                        middleSchoolBreakfast.MaxWeeklyValue = 535;
                    }

                    foreach (var highSchoolBreakfast in highSchoolBreakfasts)
                    {
                        highSchoolBreakfast.MaxWeeklyValue = 570;
                    }

                    foreach (var k8Breakfast in k8Breakfasts)
                    {
                        k8Breakfast.MaxWeeklyValue = 485;
                    }

                    foreach (var elementaryLunch in elementaryLunches)
                    {
                        elementaryLunch.MaxWeeklyValue = 935;
                    }

                    foreach (var middleSchoolLunch in middleSchoolLunches)
                    {
                        middleSchoolLunch.MaxWeeklyValue = 1035;
                    }

                    foreach (var highSchoolLunch in highSchoolLunches)
                    {
                        highSchoolLunch.MaxWeeklyValue = 1080;
                    }

                    foreach (var k8Lunch in k8Lunches)
                    {
                        k8Lunch.MaxWeeklyValue = 935;
                    }



                    break;

                //Update to target 3 values
                case 3:

                    foreach (var elementaryBreakfast in elementaryBreakfasts)
                    {
                        elementaryBreakfast.MaxWeeklyValue = 430;
                    }

                    foreach (var middleSchoolBreakfast in middleSchoolBreakfasts)
                    {
                        middleSchoolBreakfast.MaxWeeklyValue = 470;
                    }

                    foreach (var highSchoolBreakfast in highSchoolBreakfasts)
                    {
                        highSchoolBreakfast.MaxWeeklyValue = 500;
                    }

                    foreach (var k8Breakfast in k8Breakfasts)
                    {
                        k8Breakfast.MaxWeeklyValue = 430;
                    }

                    foreach (var elementaryLunch in elementaryLunches)
                    {
                        elementaryLunch.MaxWeeklyValue = 640;
                    }

                    foreach (var middleSchoolLunch in middleSchoolLunches)
                    {
                        middleSchoolLunch.MaxWeeklyValue = 710;
                    }

                    foreach (var highSchoolLunch in highSchoolLunches)
                    {
                        highSchoolLunch.MaxWeeklyValue = 740;
                    }

                    foreach (var k8Lunch in k8Lunches)
                    {
                        k8Lunch.MaxWeeklyValue = 640;
                    }

                    break;
            }

            _repository.Update<MenAgeGroupStandard>(elementaryBreakfasts);
            _repository.Update<MenAgeGroupStandard>(middleSchoolBreakfasts);
            _repository.Update<MenAgeGroupStandard>(highSchoolBreakfasts);
            _repository.Update<MenAgeGroupStandard>(k8Breakfasts);
            _repository.Update<MenAgeGroupStandard>(elementaryLunches);
            _repository.Update<MenAgeGroupStandard>(middleSchoolLunches);
            _repository.Update<MenAgeGroupStandard>(highSchoolLunches);
            _repository.Update<MenAgeGroupStandard>(k8Lunches);
            #endregion
        }

        private IEnumerable<CEPSchoolOptionsModel> GetCepSchoolOptions()
        {
            var options = (CEPSchoolOption[])Enum.GetValues(typeof(CEPSchoolOption));
            var hasProvisionSchools = _repository.GetList<AdmSitesOption>(w => w.IsProvisionSite);
            //TODO: I am not really sure we need such complex method to generate options
            foreach (var option in options)
            {
                if (option == CEPSchoolOption.District)
                {
                    if (!hasProvisionSchools.Any())
                    {
                        yield return GetCepSchoolOption(option);
                    }
                }
                else
                {
                    yield return GetCepSchoolOption(option);
                }
            }
        }

        private static CEPSchoolOptionsModel GetCepSchoolOption(CEPSchoolOption option)
        {
            return new CEPSchoolOptionsModel
            {
                Value = (int)option,
                Description = AddWhitespaces(option.ToString())
            };
        }

        //TODO: maybe it worse to make an extension
        private static string AddWhitespaces(string val)
        {
            return Regex.Replace(val, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }
    }
}
