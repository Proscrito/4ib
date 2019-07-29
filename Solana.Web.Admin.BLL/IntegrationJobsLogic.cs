using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Processes.Legacy;
using Horizon.Common.Processes.Legacy.Integration;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.App;
using Horizon.Common.Repository.Legacy.Models.Common;
using Horizon.Common.Repository.Legacy.Models.Far;
using Horizon.Common.Repository.Legacy.Models.Inv;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.IntegrationJobs;
using Solana.Web.Admin.Models.Responses.IntegrationJobs;
using Solana.Web.Admin.Models.Responses.IntegrationJobs.NestedModels;
using Solana.Web.Services;

namespace Solana.Web.Admin.BLL
{
    public class IntegrationJobsLogic : IIntegrationJobsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public IntegrationJobsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<GetJobsResponse> GetJobs()
        {
            var results = await _repository.GetListAsync<AdmIntegrationJob>(s => !s.IsDeleted);
            return new GetJobsResponse
            {
                Jobs = results
                    .OrderBy(o => o.Name)
                    .Select(s => new IntegrationJob
                    {
                        AdmIntegrationJobID = s.AdmIntegrationJobID,
                        Name = s.Name,
                        MapTypeName = Enum.GetName(typeof(IntegrationMapType), s.AdmIntegrationMap.MapType)
                            ?.Replace("_", " "),
                        JobType = s.AdmIntegrationMap.IsImport ? "Import" : "Export",
                        IsDeleted = s.IsDeleted
                    })
                    .ToList()
            };
        }

        public async Task<GetJobHistoryResponse> GetJobHistory(int jobId)
        {
            var results = (await _repository.GetListAsync<AdmIntegrationResult>(s => s.AdmIntegrationJobID == jobId))
                .OrderByDescending(o => o.EndTime)
                .ToList();

            return new GetJobHistoryResponse
            {
                JobHistories = results
                    .Select(jobHistory => new IntegrationJobHistory()
                    {
                        AdmIntegrationJobID = jobHistory.AdmIntegrationJobID,
                        AdmIntegrationResultID = jobHistory.AdmIntegrationResultID,
                        AdmUserID = jobHistory.AdmUserID ?? 0,
                        UserName = jobHistory.AdmUser == null ? "Import" : jobHistory.AdmUser.UserLogin,
                        NumberOfErrors = jobHistory.NumberOfErrors,
                        NumberOfNewRecordsCreated = jobHistory.NumberOfNewRecordsCreated,
                        NumberOfRecordsImported = jobHistory.NumberOfRecordsImported,
                        NumberOfRecordsExported = jobHistory.NumberExported,
                        ImportFileNames = jobHistory.AdmIntegrationJobsFile == null
                            ? string.Empty
                            : jobHistory.AdmIntegrationJobsFile.FileName,
                        AdmIntegrationJobsFileID = jobHistory.AdmIntegrationJobsFileID ?? 0,
                        EndTime = jobHistory.EndTime
                    })
                    .ToList()
            };
        }

        public async Task<GetJobErrorsResponse> GetJobErrors(int resultId)
        {
            var results =
                await _repository.GetListAsync<AdmIntegrationResultsError>(e => e.AdmIntegrationResultID == resultId);
            return new GetJobErrorsResponse()
            {
                JobErrors = _autoMapper.Map<Collection<IntegrationJobResult>>(
                    results.OrderByDescending(o => o.AdmIntegrationResultsErrorID))
            };
        }

        public async Task<GetIntegrationJobDetailsResponse> GetIntegrationJobDetails(GetIntegrationJobDetailsRequest request)
        {
            GetIntegrationJobDetailsResponse model;
            AdmIntegrationJob job = null;

            switch (request.JobId)
            {
                case 0: // find first                       
                    var view = await _repository.GetAsync<AppView>(v => v.AppObjectID == "Integration" && v.ViewName == "Index");
                    if (view != null)
                    {
                        var pref = (await _repository.GetListAsync<AppViewsAdmUsersPreference>(p => p.AdmUserID == request.CurrentAdmUserId &&
                                                                                                   p.AppViewID == view.AppViewID))
                            .FirstOrDefault();
                        if (pref?.LastViewedID != null) request.JobId = pref.LastViewedID.Value;
                    }

                    if (request.JobId != 0)
                    {
                        job = (await _repository.GetListAsync<AdmIntegrationJob>(j => j.AdmIntegrationJobID == request.JobId && !j.IsDeleted))
                            .FirstOrDefault();
                    }

                    if (job == null)
                    {
                        job = (await _repository.GetListAsync<AdmIntegrationJob>())
                                        .OrderBy(o => o.Name)
                                        .FirstOrDefault(o => !o.IsDeleted);
                    }

                    break;
                case -1: //new one
                    break;
                default:
                    job = await _repository.FindAsync<AdmIntegrationJob>(request.JobId);
                    await SaveUserLastViewed(request.JobId, request.CurrentAdmUserId);
                    break;
            }

            if (job == null)
            {
                model = new GetIntegrationJobDetailsResponse()
                { 
                    JobName = "",
                    JobDescription = "",  
                    TaskModel = new AdmScheduledTaskItem(),
                    IsImport = request.IsImport.HasValue && request.IsImport.Value,
                    MapType = (int)GetMapType(request.MapTypeName),
                    GradeSchoolChangeThreshhold = 10,
                    CNIPsVendorNumber = "",
                    CNIPsCDSNumber = "",
                    CNIPsLicenseNumber = "",
                    CNIPsSponsor = "",
                    CNIPsID = "" 
                };
            }
            else
            {
                model = _autoMapper.Map<GetIntegrationJobDetailsResponse>(job);
                model.JobName = job.Name;
                model.JobDescription = job.Description;
                model.IsImport = job.AdmIntegrationMap.IsImport;
                model.JobCriteriaColumns = BuildJobCriteriaColumns(job);
                // krp 2do  - does this work? without recalling the db to populate the AdmIntegrationMap
                model.MapType = job.AdmIntegrationMap.MapType; // GetImportType(job.AdmIntegrationMapID)
                model = await UpdateModelWithCniPs(model);
                model.TaskModel = new AdmScheduledTaskItem();

                var sched = await _repository.GetAsync<AdmScheduledTask>(t => t.AdmIntegrationJobID == job.AdmIntegrationJobID);
                if (sched != null)
                {
                    model.TaskModel = _autoMapper.Map<AdmScheduledTaskItem>(sched);
                    if (sched.StartTime.HasValue) model.TaskModel.StartTime =
                        new DateTime(((TimeSpan)sched.StartTime).Ticks);
                }
            }

            var integrationMaps =
                await _repository.GetListAsync<AdmIntegrationMap>(q =>
                    q.MapType == model.MapType && q.IsImport == model.IsImport);
            model.Maps = _autoMapper.Map<Collection<IntegrationMapItem>>(integrationMaps);

            if (model.Maps.Count == 0 && model.AdmIntegrationMapID == 0 &&
                (model.MapType == (int)IntegrationMapType.Gordon_Foods_Order_Export)
                                      || (model.MapType == (int)IntegrationMapType.CNIPs))
            {
                var map = await GetNewMap(model.MapType);
                model.Maps.Add(_autoMapper.Map<IntegrationMapItem>(map));
            }
            return model;
        }

        public bool LaunchJob(PostLaunchJobRequest request)
        {
            ProcessBase process = IntegrationFactory.GetIntegrationProcess(
               request.CustomerId, request.AdmUserId, request.JobId);
            IProcessArguments pargs = new IntegrationJobArguments()
            {
                CustomerID = request.CustomerId,
                AdmIntegrationJobID = request.JobId,
                AdmUserID = request.AdmUserId,
                AdmIntegrationJobsFileID = request.FileId
                //IsImport = true
            };

            var job = JobManager.Instance.DoJobAsync(a => process.Run(pargs));
            process.OnStatusChanged += (sender, e) =>
            {
                Trace.Write(e.StatusMessage);
            };
            process.OnStepChanged += (sender, e) =>
            {
                Trace.Write(e.StatusMessage);
                job.ReportProgress(e.PercentComplete, e.StatusMessage);
            };
            process.OnComplete += (sender, result) =>
            {
                job.OnSendResults(result);
                job.ReportComplete();
            };
            JobStorage.Instance.StoreGlobalJobId(job.Id);

            return true;
        }

        public async Task<bool> DeleteJob(int jobId)
        {
            var job = (await _repository.GetListAsync<AdmIntegrationJob>(j => j.AdmIntegrationJobID == jobId && !j.IsDeleted))
                .FirstOrDefault();
            var isAvaileble = job != null && !job.IsAutomated;
            if (isAvaileble)
            {
                job.IsDeleted = true;
                await _repository.UpdateAsync(job);
                return true;
            }

            throw new InvalidOperationException("Couldn't find or unavailable job");
        }

        public async Task<PostJobDetailsResponse> SaveJobDetails(PostJobDetailsRequest request)
        {
            DateTime today = _repository.GetSchoolNow(request.admSiteId);
            if (!string.IsNullOrEmpty(request.FileLocation)) request.FileLocation = request.FileLocation.Trim();
            if (!string.IsNullOrEmpty(request.FileFtpUserName)) request.FileFtpUserName = request.FileFtpUserName.Trim();
            if (!string.IsNullOrEmpty(request.FileFtpPassword)) request.FileFtpPassword = request.FileFtpPassword.Trim();

            if (!request.IsImport &&
                ((request.MapType == IntegrationMapType.Gordon_Foods_Order_Export)
                 || (request.MapType == IntegrationMapType.CNIPs))
                && request.AdmIntegrationMapID == 0)
            {
                var map = await GetNewMap((int)request.MapType);
                request.AdmIntegrationMapID = map.AdmIntegrationMapID;
            }

            var dbJob = request.AdmIntegrationJobID == 0 ? new AdmIntegrationJob() :
                await _repository.FindAsync<AdmIntegrationJob>(request.AdmIntegrationJobID);

            dbJob.AdmIntegrationJobID = request.AdmIntegrationJobID;
            dbJob.AdmIntegrationMapID = request.AdmIntegrationMapID;
            dbJob.Name = request.JobName;
            dbJob.Description = request.JobDescription;
            dbJob.IsInactivateObjectsNotInSource = request.IsInactivateObjectsNotInSource;
            dbJob.IsThreshholdNewRecords = request.IsThreshholdNewRecords;
            dbJob.ThreshholdNewRecordsPercent = request.ThreshholdNewRecordsPercent;
            dbJob.IsThreshholdErrors = request.IsThreshholdErrors;
            dbJob.ThreshholdErrorsPercent = request.ThreshholdErrorsPercent;
            dbJob.IsUpdateOnly = request.IsUpdateOnly;
            dbJob.IsDCImport = request.IsDCImport;
            dbJob.IsAppImport = request.IsAppImport;
            dbJob.IsAutomated = request.IsAutomated;
            dbJob.FileSourceType = request.FileSourceType;
            dbJob.FileLocation = request.FileLocation;
            dbJob.FileFtpUserName = request.FileFtpUserName;
            dbJob.FileFtpPassword = request.FileFtpPassword;
            dbJob.IsOverrideGradeSchoolChangeThreshhold = request.IsOverrideGradeSchoolChangeThreshhold; //PMM 5/3/16

            // The sledgehammer method:
            // we could traverse all the Criteria columns and values, updating existing ones
            // adding new ones and deleting ones no longer used and we'd also have to traverse
            // all the column values within each column, adding new values and removing values.
            // but that's a lot of complicated code. We don't save exports that often so 
            // simply deleting what's already in the DB and re-adding everything is 
            // soon much simpler.  the only cost is the delete/insert actions on a very small table
            // ....and we're done.  so...........

            //delete all criteria columns and values.

            dbJob.AdmIntegrationJobCriteriaColumns.ToList().ForEach(e =>
            {
                e.AdmIntegrationJobCriteriaColumnValues.ToList().ForEach(e2 => _repository.MarkForDeletion(e2));
                _repository.MarkForDeletion(e);
            });

            //add Criteria Columns and Values from the request
            if (request.JobCriteriaColumns != null && request.JobCriteriaColumns.Any())
            {
                foreach (JobCriteraColumn col in request.JobCriteriaColumns)
                {
                    //create the column, add the values, then add column to the DBJob

                    bool isIdComparison = col.ColumnName == "isActive";  //hard coding this for most jobs so it compares the value.

                    var dbCol = new AdmIntegrationJobCriteriaColumn()
                    {
                        ColumnName = col.ColumnName,
                        IsIDComparison = isIdComparison
                    };

                    foreach (JobCriteriaColumnValue val in col.ColumnCriteriaValues)
                    {
                        dbCol.AdmIntegrationJobCriteriaColumnValues.Add(
                            new AdmIntegrationJobCriteriaColumnValue()
                            {
                                CriteriaID = val.Id,
                                CriteriaValue = val.Value ?? ""
                            }
                            );
                    }
                    dbJob.AdmIntegrationJobCriteriaColumns.Add(dbCol);
                }
            }

            if (dbJob.IsAutomated)
            {
                AdmScheduledTask task;
                if (dbJob.AdmScheduledTasks.Count == 0)
                {
                    task = new AdmScheduledTask { CreatedDate = today };
                    dbJob.AdmScheduledTasks.Add(task);
                }
                else
                {
                    task = dbJob.AdmScheduledTasks.First();
                }
                task.AdmScheduledTaskName = dbJob.Name;
                task.AdmScheduledTaskType = ScheduledTaskType.IntegrationJob;
                if (_repository.CurrentAdmUserId != null) task.AdmUserID = (int)_repository.CurrentAdmUserId;
                task.Frequency = request.TaskModel.Frequency;
                task.StartDate = request.TaskModel.StartDate;
                task.RunOnStartup = request.TaskModel.RunOnStartup;

                task.IsDeleted = false;
                if (request.TaskModel.StartTime != null)
                    task.StartTime = request.TaskModel.StartTime;
            }
            else
            {
                if (dbJob.AdmScheduledTasks.Count != 0) dbJob.AdmScheduledTasks.First().IsDeleted = true;
            }

            var userInfo = await _repository.FindAsync<AdmUser>(request.AdmUserID);
            var createdBy = userInfo.FirstName + " " + userInfo.LastName;
            if (dbJob.AdmIntegrationJobID == 0)
            {
                dbJob.CreatedBy = createdBy;
                dbJob.CreatedDateTime = DateTime.Now;
            }
            else
            {
                dbJob.LastModifiedBy = createdBy;
                dbJob.LastModifiedDateTime = DateTime.Now;
            }
            dbJob = dbJob.AdmIntegrationJobID == 0 ?
                await _repository.CreateAsync(dbJob) :
               await _repository.UpdateAsync(dbJob);

            await SaveUserLastViewed(dbJob.AdmIntegrationJobID);

            return _autoMapper.Map<PostJobDetailsResponse>(dbJob);
        }

        public async Task<bool> ValidateName(PostValidateNameRequest request)
        {
            return (await _repository.GetListAsync<AdmIntegrationJob>(j =>
                       j.AdmIntegrationJobID != request.Id &&
                       j.Name == request.Name && j.IsDeleted == false))
                .Any();
        }

        public async Task<GetEligibilitiesResponse> GetEligibilities()
        {
            return new GetEligibilitiesResponse()
            {
                Eligibilities = _autoMapper.Map<Collection<JobEligibility>>(
                    await _repository.GetListAsync<FarElig>())
            };
        }

        public async Task<GetVendorsResponse> GetVendors()
        {
            var vendors = await _repository.GetListAsync<InvVendors>();
            return new GetVendorsResponse()
            {
                Vendors =
                    vendors.Select(s => new JobVendor
                    {
                        Id = s.InvVendorID,
                        Value = s.Name
                    }).ToList()
            };
        }

        public async Task<GetIntegrationMapTypesResponse> GetIntegrationMapTypes(GetIntegrationMapTypesRequest request)
        {
            List<JobSelectListItem> integrationMapTypes = GetSelectList(typeof(IntegrationMapType));

            if (request.IsImport)
                return new GetIntegrationMapTypesResponse
                {
                    Types = GetImportIntegrationMapTypes(integrationMapTypes)
                };

            return new GetIntegrationMapTypesResponse
            {
                Types = await GetExportIntegrationMapTypes(integrationMapTypes, request.AdmSiteId)
            };
        }

        private async Task<string> GetSchoolsState(int admSiteId)
        {
            return (await _repository.GetListAsync<AdmSite>(w => w.AdmSiteID == admSiteId))
                .FirstOrDefault()?.State;
        }

        private async Task<List<JobSelectListItem>> GetExportIntegrationMapTypes(List<JobSelectListItem> integrationMapTypes,
            int admSiteId)
        {
            for (int i = integrationMapTypes.Count - 1; i >= 0; i--)
            {
                JobSelectListItem item = integrationMapTypes[i];
                int value = Convert.ToInt32(item.Value);
                if (value == Convert.ToInt32(IntegrationMapType.Direct_Cert_Free)
                    || value == Convert.ToInt32(IntegrationMapType.Direct_Cert_Reduced)
                    || value == Convert.ToInt32(IntegrationMapType.Inventory)
                    || value == Convert.ToInt32(IntegrationMapType.Nutritionals)
                    || value == Convert.ToInt32(IntegrationMapType.Orders)
                    //|| value == Convert.ToInt32(IntegrationMapType.US_Foods_Order_Export)
                    || value == Convert.ToInt32(IntegrationMapType.Nutriscan)
                    || value == Convert.ToInt32(IntegrationMapType.Online)
                    || value == Convert.ToInt32(IntegrationMapType.Schools)
                    || value == Convert.ToInt32(IntegrationMapType.Direct_Cert_All)
                    || value == Convert.ToInt32(IntegrationMapType.Gordon_Foods_Order_Export))
                {
                    integrationMapTypes.RemoveAt(i);
                }


                else if (value == Convert.ToInt32(IntegrationMapType.CNIPs) && await GetSchoolsState(admSiteId) != "CA")
                {
                    integrationMapTypes.RemoveAt(i);
                }
            }

            return integrationMapTypes;

        }

        private List<JobSelectListItem> GetImportIntegrationMapTypes(List<JobSelectListItem> integrationMapTypes)
        {
            // this is hardcoded logic;
            for (int i = integrationMapTypes.Count - 1; i >= 0; i--)
            {
                JobSelectListItem item = integrationMapTypes[i];
                int value = Convert.ToInt32(item.Value);

                if (value == Convert.ToInt32(IntegrationMapType.Orders) || value == Convert.ToInt32(IntegrationMapType.CNIPs) || value == Convert.ToInt32(IntegrationMapType.Gordon_Foods_Order_Export))
                {
                    integrationMapTypes.RemoveAt(i);
                }

                //if (value == Convert.ToInt32(IntegrationMapType.US_Foods_Order_Export)
                //    || value == Convert.ToInt32(IntegrationMapType.Orders))
                //{
                //    integrationMapTypes.RemoveAt(i);
                //}
            }

            return integrationMapTypes;
        }

        private static List<JobSelectListItem> GetSelectList(Type type)
        {
            List<JobSelectListItem> items = new List<JobSelectListItem>();
            foreach (var value in Enum.GetValues(type))
            {
                items.Add(new JobSelectListItem()
                {
                    Text = value.ToString().Replace("__", "/").Replace('_', ' '),
                    Value = ((int)value).ToString()
                });
            }
            return items;
        }

        private static IntegrationMapType GetMapType(string mapTypeName)
        {
            //PMM 11/17/15 if no map exists, default
            if (string.IsNullOrEmpty(mapTypeName)) return IntegrationMapType.Community;
            return (IntegrationMapType)Enum.Parse(typeof(IntegrationMapType), mapTypeName);
        }

        private async Task SaveUserLastViewed(int jobId, int currentAdmUserId)
        {
            var view = await _repository.GetAsync<AppView>(v => v.AppObjectID == "Integration" && v.ViewName == "Index");
            if (view != null)
            {
                var pref = (await _repository.GetListAsync<AppViewsAdmUsersPreference>(p => p.AdmUserID == currentAdmUserId &&
                                                                                           p.AppViewID == view.AppViewID))
                    .FirstOrDefault();
                if (pref == null)
                {
                    pref = new AppViewsAdmUsersPreference() { AppViewID = view.AppViewID, AdmUserID = currentAdmUserId };
                    pref = await _repository.CreateAsync(pref);
                }
                pref.LastViewedID = jobId;
                await _repository.UpdateAsync(pref);
            }
        }


        private async Task<GetIntegrationJobDetailsResponse> UpdateModelWithCniPs(GetIntegrationJobDetailsResponse model)
        {
            var jobsCriteria = await _repository.GetListAsync<AdmIntegrationJobCriteriaColumn>(w =>
                 w.AdmIntegrationJobID == model.AdmIntegrationJobID);
            var bCniPs = false;
            if (jobsCriteria != null)
            {
                foreach (var criteria in jobsCriteria)
                {
                    var values = (await _repository.GetListAsync<AdmIntegrationJobCriteriaColumnValue>(w =>
                        w.AdmIntegrationJobCriteriaColumnID == criteria.AdmIntegrationJobCriteriaColumnID))
                        .FirstOrDefault();
                    if (values != null)
                    {
                        switch (criteria.ColumnName)
                        {
                            case "CNIPsVendorNumber":
                                model.CNIPsVendorNumber = values.CriteriaValue;
                                bCniPs = true;
                                break;
                            case "CNIPsLicenseNumber":
                                model.CNIPsLicenseNumber = values.CriteriaValue;
                                bCniPs = true;
                                break;
                            case "CNIPsCDSNumber":
                                model.CNIPsCDSNumber = values.CriteriaValue;
                                bCniPs = true;
                                break;
                            case "CNIPsSponsor":
                                model.CNIPsSponsor = values.CriteriaValue;
                                bCniPs = true;
                                break;
                            case "CNIPsID":
                                model.CNIPsID = values.CriteriaValue;
                                bCniPs = true;
                                break;
                        }
                    }
                }
            }
            if (bCniPs == false)
            {
                model.CNIPsCDSNumber = "";
                model.CNIPsID = "";
                model.CNIPsLicenseNumber = "";
                model.CNIPsSponsor = "";
                model.CNIPsVendorNumber = "";
            }
            return model;
        }
        private static List<JobCriteraColumn> BuildJobCriteriaColumns(AdmIntegrationJob job)
        {
            var cols = new List<JobCriteraColumn>();
            foreach (AdmIntegrationJobCriteriaColumn col in job.AdmIntegrationJobCriteriaColumns)
            {
                var mVals = new List<JobCriteriaColumnValue>();
                foreach (AdmIntegrationJobCriteriaColumnValue colVal in col.AdmIntegrationJobCriteriaColumnValues)
                {
                    mVals.Add(new JobCriteriaColumnValue() { Id = colVal.CriteriaID, Value = colVal.CriteriaValue });
                }
                cols.Add(new JobCriteraColumn()
                {
                    ColumnName = col.ColumnName,
                    ColumnCriteriaValues = mVals
                });
            }
            return cols;
        }


        private async Task<AdmIntegrationMap> GetNewMap(int mapType)
        {

            var map = await _repository.GetAsync<AdmIntegrationMap>(q => q.MapType == mapType && q.IsImport == false);
            if (map == null)
            {
                map = new AdmIntegrationMap()
                {
                    AdmIntegrationMapID = 0,
                    //Name = "Gordon Food Service",
                    IsExport = true,
                    IsImport = false,
                    AppObjectID = "Site Orders",
                    FileLocation = string.Empty,
                    FileLocationType = IntegrationMapEndpoint.Client_File,
                    FileType = IntegrationMapFileType.Comma_Separated,
                    FileUserDefinedDelimiter = null,
                    BeginOnLineNumber = 0,
                    Preloaded = false,
                    MapType = mapType,
                    UseDoubleQuote = false,
                    MapDateFormat = 0,
                    UseHeaderDetail = true
                };
                if (mapType == (int)IntegrationMapType.Gordon_Foods_Order_Export)
                {
                    map.Name = "Gordon Food Service";
                }
                else if (mapType == (int)IntegrationMapType.CNIPs)
                {
                    map.Name = "CNIPs";
                    map.AppObjectID = "Accountability Export";
                    map.FileType = IntegrationMapFileType.Fixed_Width;

                }
                map = await _repository.CreateAsync(map);
            }
            return map;
        }

        private async Task SaveUserLastViewed(int jobId)
        {
            var view = await _repository.GetAsync<AppView>(v =>
                v.AppObjectID == "Integration" && v.ViewName == "Index");
            if (view != null)
            {
                var prefs = await _repository.GetListAsync<AppViewsAdmUsersPreference>(p =>
                    p.AdmUserID == _repository.CurrentAdmUserId && p.AppViewID == view.AppViewID);
                var pref = prefs.FirstOrDefault();
                if (pref == null)
                {
                    if (_repository.CurrentAdmUserId != null)
                        pref = new AppViewsAdmUsersPreference()
                        {
                            AppViewID = view.AppViewID,
                            AdmUserID = _repository.CurrentAdmUserId.Value
                        };
                    pref = await _repository.CreateAsync(pref);
                }
                pref.LastViewedID = jobId;
                await _repository.UpdateAsync(pref);
            }
        }

    }
}