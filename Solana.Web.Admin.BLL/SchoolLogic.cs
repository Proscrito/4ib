using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Acc;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.App;
using Horizon.Common.Repository.Legacy.Models.Common;
using Horizon.Common.Repository.Legacy.Models.Inv;
using Horizon.Common.Repository.Legacy.Models.Pos;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.Schools;
using Solana.Web.Admin.Models.Requests.Schools.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class SchoolLogic: ISchoolLogic
    {
        private readonly ISolanaRepository _repo;
        private readonly IMapper _autoMapper;

        public SchoolLogic(ISolanaRepository repo, IMapper autoMapper)
        {
            _repo = repo;
            _autoMapper = autoMapper;
        }

        public async Task AppViewsAdmUsersPreferencesInit(int admUserId, string id = "")
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                bool isNumeric = int.TryParse(id, out _);
                int admSiteId = Convert.ToInt32(id);

                if (isNumeric)
                {
                    var appView = (await _repo.GetListAsync<AppView>(a => a.ControllerName.Equals("Schools")
                                                                          && a.ViewName.Equals("Index")
                                                                          && a.AppObjectID.Equals("Schools"))).FirstOrDefault();
                    if (appView == null)
                        return;

                    var appPreference = appView.AppViewsAdmUsersPreferences.FirstOrDefault(p => p.AppViewID == appView.AppViewID && p.AdmUserID == admUserId);
                    if (appPreference != null)
                    {
                        appPreference.LastViewedID = admSiteId;
                    }
                    else
                    {
                        appView.AppViewsAdmUsersPreferences.Add(new AppViewsAdmUsersPreference { AppViewID = appView.AppViewID, AdmUserID = admUserId, LastViewedID = admSiteId });
                    }

                    await _repo.UpdateAsync(appView);
                }
            }
        }

        public async Task<int> GetParentLevel(int admSiteid)
        {
            var site = (await _repo.GetListAsync<AdmSite>(p => p.AdmSiteID == admSiteid)).FirstOrDefault();
            return Convert.ToInt32(site.AdmManagementLevelID);
        }

        public async Task<int> GetSiteIdInUse(string siteId, int admSiteId)
        {
            var site = admSiteId == 0
                ? await _repo.GetListAsync<AdmSite>(s => s.SiteID == siteId)
                : await _repo.GetListAsync<AdmSite>(s =>
                    s.SiteID == siteId && s.AdmSiteID != admSiteId);

            return site.Count;
        }

        public async Task<int> GetSiteNameInUse(string siteName, int admSiteId)
        {
            var site = admSiteId == 0 
                ? await _repo.GetListAsync<AdmSite>(s => s.SiteDescription == siteName) 
                : await _repo.GetListAsync<AdmSite>(s => s.SiteDescription == siteName && s.AdmSiteID != admSiteId);

            return site.Count;
        }

        public async Task<string> GetStateInfo(int admSiteId)
        {
            var admsite = (await _repo.GetListAsync<AdmSite>(s => s.AdmSiteID == admSiteId)).FirstOrDefault();
            return admsite.State;
        }

        public async Task SaveSchoolsInformation(PutSchoolsInformationRequest model)
        {
            var validations = await ValidateSchoolData(model);
            if (!validations.Any())
            {
                if (model.AdmSiteId != 0)
                {
                    var site = await _repo.FindAsync<AdmSite>(model.AdmSiteId);

                    site.SiteID = model.SiteId;
                    site.SiteDescription = model.SiteDescription;
                    site.AdmManagementLevelID = model.AdmManagementLevelId;
                    site.ParentAdmSiteID = model.ParentAdmSiteId;
                    site.InvSiteTypeID = model.InvSiteTypeId;
                    site.State = model.State;
                    site.IsDistrict = model.IsDistrict;
                    site.IsActive = model.IsActive;
                    site.MenAgeGroupID = model.MenAgeGroupID;
                    site.Address1 = model.Address1;
                    site.Address2 = model.Address2;
                    site.City = model.City;
                    site.ZipCode = model.ZipCode;
                    site.SchoolPhoneNumber = model.SchoolPhoneNumber;
                    site.PrincipalPhoneNumber = model.ManagerPhoneNumber;
                    site.PrincipalName = model.ManagerName;
                    site.PrincipalEmail = model.ManagerEmail;
                    if (string.IsNullOrWhiteSpace(model.AccLockoutDate))
                    {
                        DateTime schoolStartDate = model.Calendar.SchoolStartDate.ParseUsDate() ?? DateTime.MinValue;
                        site.AccLockoutDate = schoolStartDate;
                    }
                    else
                    {
                        site.AccLockoutDate = model.AccLockoutDate.ParseUsDate();
                    }

                    // Create or Update the Site Options
                    if (site.AdmSitesOption == null)
                    {
                        var options = new AdmSitesOption
                        {
                            AdmSiteID = model.AdmSiteId,
                            SchoolStartDate = model.Calendar.SchoolStartDate.ParseUsDate(),
                            IsYearRoundSchool = model.Calendar.IsYearRoundSchool
                        };
                        site.AdmSitesOption = options;
                    }
                    else
                    {
                        site.AdmSitesOption.SchoolStartDate = model.Calendar.SchoolStartDate.ParseUsDate();
                        site.AdmSitesOption.IsYearRoundSchool = model.Calendar.IsYearRoundSchool;
                        if (model.Calendar.IsYearRoundSchool == false)
                        {
                            site.AdmSitesOption.SchoolAltStartDate = null;
                            site.AdmSitesOption.SchoolTempStatusExpDate = null;
                            site.AdmSitesOption.SchoolStartDate = model.Calendar.SchoolStartDate.ParseUsDate();
                        }

                    }
                    //PMM 9/21/15
                    site.AdmSitesOption.IsClaimChildSitesSeparately = model.SiteOptions.IsClaimChildSitesSeparately;
                    site.AdmSitesOption.IsRefundAllowed = model.SiteOptions.IsRefundAllowed;
                    site.AdmSitesOption.SiteAttendanceFactor = model.SiteOptions.SiteAttendanceFactor;
                    site.AdmSitesOption.IsCEP = model.SiteOptions.IsCEP;
                    site.AdmSitesOption.IsDontInactivateMissingStudent = model.SiteOptions.IsDontInactivateMissingStudent;
                    site.AdmSitesOption.IsSevereNeed = model.SiteOptions.IsSevereNeed;

                    //AMH SLN-3147 Provision 2 settings
                    site.AdmSitesOption.IsProvisionSite = model.SiteOptions.IsProvisionSite;
                    site.AdmSitesOption.BaseYearStart = model.SiteOptions.BaseYearStart;
                    site.AdmSitesOption.BaseYearEnrollment = model.SiteOptions.BaseYearEnrollment;
                    site.AdmSitesOption.BaseYearFree = model.SiteOptions.BaseYearFree;
                    site.AdmSitesOption.BaseYearReduced = model.SiteOptions.BaseYearReduced;

                    //time zone
                    await SaveCentralOfficeTimeZone(model.COTimeZoneName);
                    site.AdmSitesOption.TimeZoneName = model.TimeZoneName;

                    //AMH S-03093 if CW, create InvVendor for the CW
                    if (site.AdmSitesOption.IsWarehouse)
                    {
                        InvVendors cwVendor = await _repo.GetAsync<InvVendors>(v => v.AdmSiteID == model.AdmSiteId);
                        cwVendor.Name = site.SiteDescription;
                        cwVendor.Active = site.IsActive;
                        cwVendor.VendorNumber = site.SiteID;
                        cwVendor.Address = site.Address1;
                        cwVendor.Address2 = site.Address2;
                        cwVendor.City = site.City;
                        cwVendor.State = site.State;
                        cwVendor.ZipCode = site.ZipCode;
                        cwVendor.PhoneNumber = site.PrincipalPhoneNumber;
                        cwVendor.Email = site.PrincipalEmail;
                        await _repo.UpdateAsync<InvVendors>(cwVendor);
                    }

                    // Create or Update the Site PIN Configuration
                    AdmSitesPINConfiguration pinConfig = site.AdmSitesPINConfiguration;
                    if (pinConfig == null)
                    {
                        pinConfig = new AdmSitesPINConfiguration { AdmSiteID = model.AdmSiteId };
                    }


                    pinConfig.PINLength = model.PinConfiguration.PINLength;
                    pinConfig.PadPINWithZeros = model.PinConfiguration.PadPINWithZeros;
                    pinConfig.StudentRandomlyGeneratePIN = model.PinConfiguration.StudentRandomlyGeneratePIN;
                    pinConfig.StudentIsLeftOfID = model.PinConfiguration.StudentIsLeftOfId;
                    pinConfig.StudentLeftOfID = model.PinConfiguration.StudentLeftOfId;
                    pinConfig.StudentRightOfID = model.PinConfiguration.StudentRightOfId;
                    pinConfig.StudentAppendToPIN = model.PinConfiguration.StudentAppendToPIN;
                    pinConfig.StudentPreventLeadingZeros = model.PinConfiguration.StudentPreventLeadingZeros;
                    pinConfig.StudentAppendPosition = model.PinConfiguration.StudentAppendPosition;
                    pinConfig.StudentAppendText = model.PinConfiguration.StudentAppendText;
                    pinConfig.UseAdultSettings = model.PinConfiguration.UseAdultSettings;

                    if (model.PinConfiguration.UseAdultSettings)
                    {
                        pinConfig.AdultRandomlyGeneratePIN = model.PinConfiguration.StudentRandomlyGeneratePIN;
                        pinConfig.AdultIsLeftOfID = model.PinConfiguration.StudentIsLeftOfId;
                        pinConfig.AdultLeftOfID = model.PinConfiguration.StudentLeftOfId;
                        pinConfig.AdultRightOfID = model.PinConfiguration.StudentRightOfId;
                        pinConfig.AdultAppendToPIN = model.PinConfiguration.StudentAppendToPIN;
                        pinConfig.AdultPreventLeadingZeros = model.PinConfiguration.StudentPreventLeadingZeros;
                        pinConfig.AdultAppendPosition = model.PinConfiguration.StudentAppendPosition;
                        pinConfig.AdultAppendText = model.PinConfiguration.StudentAppendText;
                    }
                    else
                    {
                        pinConfig.AdultRandomlyGeneratePIN = model.PinConfiguration.AdultRandomlyGeneratePIN;
                        pinConfig.AdultIsLeftOfID = model.PinConfiguration.AdultIsLeftOfId;
                        pinConfig.AdultLeftOfID = model.PinConfiguration.AdultLeftOfId;
                        pinConfig.AdultRightOfID = model.PinConfiguration.AdultRightOfId;
                        pinConfig.AdultAppendToPIN = model.PinConfiguration.AdultAppendToPIN;
                        pinConfig.AdultPreventLeadingZeros = model.PinConfiguration.AdultPreventLeadingZeros;
                        pinConfig.AdultAppendPosition = model.PinConfiguration.AdultAppendPosition;
                        pinConfig.AdultAppendText = model.PinConfiguration.AdultAppendText;
                    }
                    site.AdmSitesPINConfiguration = pinConfig;

                    if (model.Terminals != null) //terminals collection deserialized in the validate method
                    {
                        foreach (var term in model.Terminals)
                        {
                            if (term.IsDeleted)
                            {
                                _repo.MarkForDeletion(term);
                            }
                            else
                            {
                                PosTerminal terminal;
                                if (term.PosTerminalId == 0)
                                {
                                    terminal = new PosTerminal();
                                    site.PosTerminals.Add(terminal);
                                }
                                else
                                {
                                    terminal = site.PosTerminals.FirstOrDefault(t => t.PosTerminalID == term.PosTerminalId);
                                }

                                if (terminal != null)
                                {
                                    terminal.MachineName = term.MachineName.Trim();
                                    terminal.TerminalNumber = term.TerminalNumber;
                                    terminal.IsAutoSale = term.IsAutoSale;
                                    terminal.IsActive = term.IsActive;
                                }
                            }
                        }
                    }

                    site.AdmSitesServePOS.Clear();
                    model.ServingSites.ForEach(s => site.AdmSitesServePOS.Add(new AdmSitesServePOS() { AdmSiteID = s.AdmSiteId, OtherAdmSiteID = s.OtherAdmSiteId }));

                    // set the customer number
                    site.CustomerNumber = model.CustomerNumber;

                    await _repo.UpdateAsync<AdmSite>(site);
                }
            }

            if (model.SiteOptions.IsCEP)
            {
                var admGlobalOptions = (await _repo.GetListAsync<AdmGlobalOption>()).FirstOrDefault();
                var claimOption = -1;
                if (admGlobalOptions != null && (CEPSchoolOption.District == admGlobalOptions.CEPSchoolOption && model.IsDistrict))
                {
                    claimOption = 0;
                }
                else if (admGlobalOptions != null && ((CEPSchoolOption.Schools == admGlobalOptions.CEPSchoolOption || CEPSchoolOption.SchoolGroups == admGlobalOptions.CEPSchoolOption) && model.IsDistrict == false))
                {
                    claimOption = 1;
                }
                if (claimOption != -1)
                {
                    await PopulateAccCEPRates(claimOption, model.AdmSiteId, 0);
                }
            }
            else if (model.SiteOptions.IsProvisionSite)
            {
                var userInfo = await _repo.FindAsync<AdmUser>(model.AdmUserId);
                var createdBy = userInfo.FirstName + " " + userInfo.LastName;
                var exists = (await _repo.GetListAsync<AccP2Rates>(w => w.AdmSiteID == model.AdmSiteId)).Any();
                if (exists == false)
                {
                    var baseYear = "7/1/" + model.SiteOptions.BaseYearStart;
                    AccP2Rates claimPct = new AccP2Rates
                    {
                        AdmSiteID = model.AdmSiteId,
                        IsBreakfastOnly = false,
                        BRFreeRate = 0,
                        BRReducedRate = 0,
                        BRFullPayRate = 0,
                        LUFreeRate = 0,
                        LUReducedRate = 0,
                        LUFullPayRate = 0,
                        EffectiveDate = DateTime.Parse(baseYear),
                        CreatedDateTime = DateTime.Now,
                        CreatedBy = createdBy,
                        LastModifiedDateTime = DateTime.Now,
                        LastModifiedBy = createdBy
                    };
                    await _repo.CreateAsync<AccP2Rates>(claimPct);
                }

            }
        }

        public async Task<List<KeyValuePair<string, string>>> ValidateSchoolData(PutSchoolsInformationRequest request)
        {
            var res = new List<KeyValuePair<string, string>>();
            if (request.Terminals != null)
            {
                List<TerminalModel> terms = request.Terminals;

                foreach (var item in terms)
                {
                    var matchTerminals = await GetMatchTerminals(item.MachineName);
                    if (matchTerminals > 0 && (item.PosTerminalId == 0))
                    {
                        res.Add(new KeyValuePair<string, string>("MachineName", "Duplicate machine names detected."));
                    }
                }
                if (terms.GroupBy(g => g.TerminalNumber).Select(group => group.Count()).Any(g => g > 1))
                {
                    res.Add(new KeyValuePair<string, string>("TerminalNumber", "Duplicate terminal numbers detected."));
                }
                if (terms.GroupBy(g => g.MachineName.ToUpper().Trim()).Select(group => group.Count()).Any(g => g > 1))
                {
                    res.Add(new KeyValuePair<string, string>("MachineName", "Duplicate machine names detected."));
                }
                request.Terminals = terms;
            }

            return res;
        }

        public async Task<List<GradeTransfer>> GetGradeTransferList(int id)
        {
            var gradeTransferList = await _repo.GetListAsync<AdmGradeTransfer>(t => t.FromAdmSiteID == id && !t.IsDeleted);

            return gradeTransferList.Select(g => new GradeTransfer()
                {
                    GradeTransferId = g.GradeTransferID,
                    PosGradeId = g.PosGradeID,
                    FromAdmSiteId = g.FromAdmSiteID,
                    ToAdmSiteId = g.AdmSiteID,
                    LastUpdateDate = g.LastUpdateDate.Date,
                    AdmUserId = g.AdmUserID
                }).ToList();
        }

        public async Task<List<ServingSites>> GetServingSite(int id)
        {
            var servingSiteList = await _repo.GetListAsync<AdmSitesServePOS>(t => t.AdmSiteID == id);
            return _autoMapper.Map<List<ServingSites>>(servingSiteList);
        }

        public async Task<List<SchoolTypesViewModel>> GetSchoolTypes()
        {
            var schoolTypes = await _repo.GetListAsync<InvSiteType>();
            return _autoMapper.Map<List<SchoolTypesViewModel>>(schoolTypes);
        }

        public async Task<List<AdmSiteModel>> GetSchoolNames(string searchValue)
        {
            var schools = await _repo.GetListAsync<AdmSite>(x => x.SiteDescription.StartsWith(searchValue));
            return _autoMapper.Map<List<AdmSiteModel>>(schools);
        }

        public async Task<List<AdmSiteModel>> GetSchoolSiteIDs(string searchValue)
        {
            var schools = await _repo.GetListAsync<AdmSite>(x => x.SiteID.StartsWith(searchValue));
            return _autoMapper.Map<List<AdmSiteModel>>(schools);
        }

        public async Task<int> CreateMissingSchoolCalendar(PostMissingSchoolCalendarRequest request)
        {
            var site = await _repo.FindAsync<AdmSite>(request.AdmSiteId);

            request.Calendars.ForEach(m =>
            {
                var operationalDate = m.OperationalDateString.ParseUsDate();
                if (operationalDate.HasValue)
                {
                    var day = site.AdmOperationalCalendars.FirstOrDefault(c => c.OperationalDate == operationalDate);
                    if (day == null)
                    {
                        day = new AdmOperationalCalendar
                        {
                            AdmSiteID = site.AdmSiteID,
                            OperationalDate = operationalDate.Value,
                            OperationalStatus = m.OperationalStatus
                        };
                        site.AdmOperationalCalendars.Add(day);
                    }

                    if (m.OperationalStatus == 0)
                    {
                        day.DayDescription = "Non School Day";
                        day.CreatedBySite = false;
                        day.OperationalStatus = 0;
                    }
                    else if (m.OperationalStatus == 2)
                    {
                        day.DayDescription = "Holiday";
                        day.CreatedBySite = true;
                        day.OperationalStatus = 2;
                    }
                    else
                    {
                        day.DayDescription = "School Day";
                        day.CreatedBySite = true;
                        day.OperationalStatus = 1;
                    }
                }
            });

            await _repo.UpdateAsync<AdmSite>(site);
            return site.AdmSiteID;
        }

        public async Task<int> CopySiteCalendar(PutSiteCalendarRequest model)
        {
            var admSiteIds = model
                .ToAdmSiteIds
                .Where(id => id != model.FromAdmSiteId)
                .ToList();
            foreach (int toSite in admSiteIds)
            {
                var timeZoneName = _repo.GetSchoolTimeZoneName();

                _repo.ExecuteStoredProc(StoredProcs.usp_AdmOperationalCalendars_Copy, new
                {
                    fromSiteId = model.FromAdmSiteId,
                    toSiteId = toSite,
                    startDate = model.StartDate.ParseUsDate(),
                    endDate = model.EndDate.ParseUsDate()
                });

                var s = await _repo.FindAsync<AdmSitesOption>(toSite);
                if (s == null)
                {
                    s = new AdmSitesOption { AdmSiteID = toSite };
                    s = await _repo.CreateAsync<AdmSitesOption>(s);
                }

                if (!string.IsNullOrWhiteSpace(model.SchoolStartDate))
                {
                    s.SchoolStartDate = model.SchoolStartDate.ParseUsDate().AdjustTimeZone(timeZoneName, false);
                }
                if (!string.IsNullOrWhiteSpace(model.SchoolEndDate))
                {
                    s.SchoolEndDate = model.SchoolEndDate.ParseUsDate().AdjustTimeZone(timeZoneName, false);
                }
                await _repo.UpdateAsync<AdmSitesOption>(s);
            }

            return model.FromAdmSiteId;
        }

        private async Task<int> GetMatchTerminals(string terminalName)
        {
            var terminals = await _repo.GetListAsync<PosTerminal>(t => t.MachineName.ToUpper() == terminalName.ToUpper());
            return terminals.Count;
        }

        private async Task SaveCentralOfficeTimeZone(string modelCoTimeZoneName)
        {
            var options = await _repo.GetListAsync<AdmSitesOption>();

            foreach (var option in options)
            {
                option.COTimeZoneName = modelCoTimeZoneName;
            }
        }

        private async Task PopulateAccCEPRates(int claimOption, int siteId, int schoolGroupId)
        {
            AccCEPRates createPct = null;
            switch (claimOption)
            {
                case 0:
                    createPct = await _repo.GetAsync<AccCEPRates>(w => w.AdmSchoolGroupID == null && w.AdmSiteID == null);
                    break;
                case 1:
                    createPct = await _repo.GetAsync<AccCEPRates>(w => (w.AdmSiteID ?? 0) == siteId);
                    break;
                case 2:
                    createPct = await _repo.GetAsync<AccCEPRates>(w => (w.AdmSchoolGroupID ?? 0) == schoolGroupId);
                    break;
            }
            if (createPct == null)
            {
                switch (claimOption)
                {
                    case 1:
                        await _repo.CreateAsync<AccCEPRates>(
                            new AccCEPRates()
                            {
                                AdmSiteID = siteId,
                                CEPFreeRate = 0,
                                CEPFullPayRate = 0
                            });
                        break;
                    case 2:
                        await _repo.CreateAsync<AccCEPRates>(
                            new AccCEPRates()
                            {
                                AdmSchoolGroupID = schoolGroupId,
                                CEPFreeRate = 0,
                                CEPFullPayRate = 0
                            });
                        break;
                    default:
                        await _repo.CreateAsync<AccCEPRates>(
                            new AccCEPRates()
                            {
                                CEPFreeRate = 0,
                                CEPFullPayRate = 0
                            });
                        break;
                }
            }
        }
    }
}