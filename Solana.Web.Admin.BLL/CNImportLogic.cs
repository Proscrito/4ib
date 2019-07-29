using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Horizon.Common.Processes.Legacy.Nutritionals.Import;
using Horizon.Common.Repository.Legacy;
using Horizon.Common.Repository.Legacy.Models.Adm;
using Horizon.Common.Repository.Legacy.Models.Men;
using Solana.Web.Admin.BLL.Interfaces;
using Solana.Web.Admin.Models.Requests.CNImport;
using Solana.Web.Admin.Models.Responses;
using Solana.Web.Admin.Models.Responses.CNImport.NestedModels;

namespace Solana.Web.Admin.BLL
{
    public class CNImportLogic : ICNImportLogic
    {
        private readonly ISolanaRepository _repository;
        private readonly IMapper _autoMapper;

        public CNImportLogic(ISolanaRepository repository, IMapper autoMapper)
        {
            _repository = repository;
            _autoMapper = autoMapper;
        }

        public async Task<GetCNImportResponse> GetCNImportViewModel(string contentPath)
        {
            var result = new GetCNImportResponse
            {
                AvailableCNVersion = await GetCurrentCNVersion()
            };

            if (!File.Exists(contentPath))
            {
                Debug.WriteLine($"File {contentPath} not found.");
                throw new FileNotFoundException("File not found", contentPath);
            }

            //microsoft guys promise async System.IO.File.ReadAllText() version in Core for year or so. Huh, let's wait a bit more...
            using (var reader = File.OpenText(contentPath))
            {
                result.AvailableCNVersion = await reader.ReadToEndAsync();
            }

            return result;
        }

        public async Task<string> GetCurrentCNVersion()
        {
            var admGlobalOptions = await _repository.GetListAsync<AdmGlobalOption>();

            if (!admGlobalOptions.Any())
            {
                Debug.WriteLine("No admin options found in database.");
                throw new ApplicationException("No admin options found");
            }

            return admGlobalOptions.First().CurrentCNVersion;
        }

        public async Task<List<MenCnResultsHeaderModel>> GetImportResultsHeader()
        {
            var menCnResultsHeaders = await _repository.GetListAsync<MenCnResultsHeader, DateTime?>(x => x.AdmUser != null, h => h.RunDate);

            return _autoMapper.Map<List<MenCnResultsHeaderModel>>(menCnResultsHeaders);
        }

        public async Task<List<MenCnResultsDetailModel>> GetImportResultsDetails(int menCnResultsHeaderID)
        {
            var menCnResultsHeader = await _repository.FindAsync<MenCnResultsHeader>(menCnResultsHeaderID);
            var menCnResultsDetails = menCnResultsHeader.MenCnResultsDetail.OrderByDescending(x => x.MenCnResultsDetailID);

            return _autoMapper.Map<List<MenCnResultsDetailModel>>(menCnResultsDetails);
        }

        public void RunImportJob(PostRunImportRequest request)
        {
            var process = new CNImporterProcess
            {
                ConnectionString = _repository.ConnectionString,
                Repository = _repository
            };

            var args = new CNImportArguments()
            {
                CustomerID = request.CustomerID,
                AdmUserID = request.AdmUserID,
                AvailableCnVersion = request.AvailableCN,
                ZipPath = request.ZipPath,
                TempPath = request.TempPath
            };
            
            //this part will get refactored after new Processes engine
            Task.Run(() => { process.Run(args); });
        }
    }
}