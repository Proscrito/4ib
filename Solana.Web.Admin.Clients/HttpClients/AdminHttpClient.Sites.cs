using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.Clients.HttpClients
{
    public partial class AdminHttpClient
    {
        public async Task<IList<GetAvailableSiteSummaryResponse>> GetAvailableSiteSummaries(int admUserID, bool showInact, bool includeDistrict, bool excludeCEP = false, bool CEPOnly = false, int? MenAgeGroupID = null, bool showSchoolGroups = false, bool showAllSelection = false)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Client.BaseAddress}/api/sites/AvailableSiteSummaries?admUserID={admUserID}&showInact={showInact}&includeDistrict={includeDistrict}&excludeCEP={excludeCEP}&CEPOnly={CEPOnly}&MenAgeGroupID={MenAgeGroupID}&showSchoolGroups={showSchoolGroups}&showAllSelection={showAllSelection}"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                }
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<IList<GetAvailableSiteSummaryResponse>>();
            return result;
        }

        public async Task<GetAdmSiteResponse> GetAdmSite(int admSiteID)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Client.BaseAddress}/api/sites/AdmSite?admSiteID={admSiteID}"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                }
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<GetAdmSiteResponse>();
            return result;
        }

        public async Task<GetServingPeriodsResponse> GetServingPeriods()
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Client.BaseAddress}/api/sites/ServingPeriods"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                }
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<GetServingPeriodsResponse>();
            return result;
        }
    }
}
