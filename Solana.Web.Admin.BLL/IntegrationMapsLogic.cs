using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.App;
using Horizon.Common.Repository.Legacy.Models.SProcs;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.IntegrationMaps;
using Solana.Web.Admin.Models.Requests.IntegrationMaps.NestedModels;
using Solana.Web.Admin.Models.Responses.IntegrationMaps;
using Solana.Web.Admin.Models.Responses.IntegrationMaps.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class IntegrationMapsLogic : IIntegrationMapsLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public IntegrationMapsLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public List<TranslationViewModel> GetTranslations()
        {
            var translatableValues = _repository.StoredProcList<TranslatableValue>(StoredProcs.usp_GetAllTranslations);
            return _autoMapper.Map<List<TranslationViewModel>>(translatableValues);
        }

        public async Task<PutIntegrationMapsResponse> SaveIntegrationMap(PutIntegrationMapsRequest request)
        {
            var map = request.AdmIntegrationMapID != 0
                ? await _repository.FindAsync<AdmIntegrationMap>(request.AdmIntegrationMapID)
                : new AdmIntegrationMap();

            if (map == null)
            {
                Debug.WriteLine($"Integration map not found in database. Id: {request.AdmIntegrationMapID}");
                throw new ApplicationException($"{request.AdmIntegrationMapID} integration map not found");
            }

            var response = new PutIntegrationMapsResponse();

            //automapper can handle all this stuff, however without proper testing I will just follow the old logic
            MapKnownFields(map, request);

            if (request.DeletePreviewFile && map.PreviewFile != null)
            {
                _repository.MarkForDeletion(map.PreviewFile);
            }

            await ProcessTriageFile(map, request);

            if (request.Columns != null && request.Columns.Any())
            {
                ProcessColumns(map, request.Columns, request.UseHeaderDetail);
            }

            if (map.AdmIntegrationMapID == 0)
            {
                await _repository.CreateAsync(map);
            }
            else
            {
                await _repository.UpdateAsync(map);
            }

            response.IntegrationMapId = map.AdmIntegrationMapID;
            return response;
        }

        public async Task<GetIntegrationMapResponse> GetIntegrationMap(GetIntegrationMapRequest request)
        {
            var map = await _repository.FindAsync<AdmIntegrationMap>(request.MapId);

            if (map == null)
            {
                return null;
            }

            var response = _autoMapper.Map<GetIntegrationMapResponse>(map);
            var appObjects = await _repository.GetListAsync<AppObject>(x => x.IsImportable);
            response.AvailableAppObjects = _autoMapper.Map<List<AvailableAppObjectViewModel>>(appObjects);

            return response;
        }

        public async Task SaveIntegrationMapColumns(PutIntegrationMapColumnsRequest request)
        {
            var map = await _repository.FindAsync<AdmIntegrationMap>(request.MapId);

            if (map != null)
            {
                ProcessColumns(map, request.Items);
            }
        }

        public async Task<List<IntegrationMapColumnViewModel>> GetIntegrationMapColumns(int mapId)
        {
            var columns = await _repository.GetListAsync<AdmIntegrationMapsColumn>(x => x.AdmIntegrationMapID == mapId);
            return _autoMapper.Map<List<IntegrationMapColumnViewModel>>(columns);
        }

        public async Task<int> SaveAppTriageFile(PutAppTriageFileRequest request)
        {
            var file = _autoMapper.Map<AppTriageFile>(request);
            await _repository.CreateAsync(file);
            return file.AppTriageFileID;
        }

        public async Task DeleteAppTriageFile(int fileId)
        {
            var file = await _repository.FindAsync<AppTriageFile>(fileId);

            if (file != null)
            {
                await _repository.DeleteAsync(file);
            }
        }

        public async Task<ICollection<IntegrationMapViewModel>> GetIntegrationMaps()
        {
            var maps = await _repository.GetListAsync<AdmIntegrationMap>(x => !x.Preloaded);
            return _autoMapper.Map<List<IntegrationMapViewModel>>(maps.OrderBy(x => x.Name));
        }

        public async Task<GetIntegrationMapResponse> DeleteIntegrationMap(int id)
        {
            var map = await _repository.FindAsync<AdmIntegrationMap>(id);

            if (map != null)
            {
                if (map.AdmIntegrationJobs.Any())
                {
                    Debug.WriteLine($"Cannot delete AdmIntegrationMap id = {id}. It has active jobs.");
                    throw new InvalidOperationException($"Cannot delete AdmIntegrationMap because there are jobs in use.");
                }

                await _repository.DeleteAsync(map);
            }

            return _autoMapper.Map<GetIntegrationMapResponse>(map);
        }

        #region SaveIntegrationMaps
        private void ProcessColumns(AdmIntegrationMap map, IEnumerable<IntegrationMapColumnSaveModel> columns, bool useHeaderDetail = true)
        {
            var existingColumnId = new List<int>();
            var count = 0;

            foreach (var column in columns)
            {
                if (!useHeaderDetail)
                {
                    column.IsHeader = false;
                }

                column.Position = ++count;

                if (column.AdmIntegrationMapsColumnID != 0)
                {
                    existingColumnId.Add(column.AdmIntegrationMapsColumnID);
                    var existingColumn = map.AdmIntegrationMapsColumns.First(x => x.AdmIntegrationMapsColumnID == column.AdmIntegrationMapsColumnID);
                    ProcessColumn(existingColumn, column);
                }
                else
                {
                    var newColumn = ProcessColumn(null, column);
                    map.AdmIntegrationMapsColumns.Add(newColumn);
                }
            }

            foreach (var mapColumn in map.AdmIntegrationMapsColumns.Where(x => !existingColumnId.Contains(x.AdmIntegrationMapsColumnID)))
            {
                _repository.MarkForDeletion(mapColumn);
            }
        }

        private AdmIntegrationMapsColumn ProcessColumn(AdmIntegrationMapsColumn existingColumn, IntegrationMapColumnSaveModel requestColumnSave)
        {
            return _autoMapper.Map(requestColumnSave, existingColumn);
        }

        private async Task ProcessTriageFile(AdmIntegrationMap map, PutIntegrationMapsRequest request)
        {
            if (request.TriageFileID != 0)
            {
                var previewFile = await _repository.FindAsync<AppTriageFile>(request.TriageFileID);

                if (previewFile != null)
                {
                    if (map.PreviewFile != null) _repository.MarkForDeletion(map.PreviewFile);

                    map.PreviewFile = new AdmIntegrationMapsPreviewFile
                    {
                        FileName = Path.GetFileName(previewFile.FileName),
                        Size = previewFile.Size,
                        UploadDate = DateTime.Now,

                        AdmIntegrationMapsFilesData = new AdmIntegrationMapsPreviewFileData()
                        {
                            Data = previewFile.AppTriageFilesData.Data
                        }
                    };
                }
            }
        }

        private static void MapKnownFields(AdmIntegrationMap map, PutIntegrationMapsRequest request)
        {
            map.AdmIntegrationMapID = request.AdmIntegrationMapID;
            map.AppObjectID = request.AppObjectID;
            map.FileLocation = request.FileLocation ?? "";
            map.FileLocationType = request.FileLocationType;
            map.FileType = request.FileType;
            map.FileUserDefinedDelimiter = request.FileUserDefinedDelimiter;
            map.IsExport = request.IsExport;
            map.IsImport = request.IsImport;
            map.Name = request.MapName;
            map.MapType = (int)request.MapType;
            map.UseDoubleQuote = request.UseDoubleQuote;
            map.UseHeaderDetail = request.UseHeaderDetail;
            map.MapDateFormat = request.MapDateFormat;
            map.FileUserDefinedDelimiter = request.FileUserDefinedDelimiter;
            map.BeginOnLineNumber = request.BeginOnLine;
        } 
        #endregion
    }
}