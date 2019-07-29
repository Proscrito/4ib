using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using Solana.Web.Admin.Models;
using Solana.Web.Admin.Models.Requests;
using Solana.Web.Admin.Models.Responses;

namespace Solana.Web.Admin.Clients.HttpClients
{
    public partial class AdminHttpClient
    {
        //TODO: Should this require a bearer token to be passed to the httpRequestMessage?
        public async Task<GetAdmUserResponse> GetAdmUser(string userName, bool isActive, bool isDeleted, bool allowLogin)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Client.BaseAddress}/api/users/AdmUser?userName={userName}&isActive={isActive}&isDeleted={isDeleted}&allowLogin={allowLogin}"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                }
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<GetAdmUserResponse>();
            return result;
        }

        public async Task<GetAdmUserPreferenceResponse> GetAdmUserPreference(int admUserID)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{Client.BaseAddress}/api/users/AdmUserPreference?admUserID={admUserID}"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                }
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<GetAdmUserPreferenceResponse>();
            return result;
        }

        public async Task<PatchAdmUserResponse> PatchAdmUser(int admUserID, JsonPatchDocument<PatchAdmUser> jsonPatchDocument)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = new HttpMethod("PATCH"),
                RequestUri = new Uri($"{Client.BaseAddress}/api/users/AdmUser/{admUserID}"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                },
                Content = new StringContent(JsonConvert.SerializeObject(jsonPatchDocument), Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<PatchAdmUserResponse>();
            return result;
        }

        public async Task<PostAdmUsersActivityResponse> PostAdmUserActivity(PostAdmUsersActivityRequest postAdmUsersActivityRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{Client.BaseAddress}/api/users/AdmUserActivity"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                },
                Content = new StringContent(JsonConvert.SerializeObject(postAdmUsersActivityRequest), Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<PostAdmUsersActivityResponse>();
            return result;
        }

        public async Task<PutAdmUserResponse> PutAdmUser(PutAdmUserRequest putAdmUserRequest)
        {
            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{Client.BaseAddress}/api/users/AdmUser"),
                Headers = {
                    { "AdmUserId", SolanaIdentityUser.AdmUserId.ToString() },
                    { "CustomerId", SolanaIdentityUser.CustomerId.ToString() },
                    { "UserLogin", SolanaIdentityUser.UserLogin }
                },
                Content = new StringContent(JsonConvert.SerializeObject(putAdmUserRequest), Encoding.UTF8, "application/json")
            };

            var response = await Client.SendAsync(httpRequestMessage);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<PutAdmUserResponse>();
            return result;
        }
    }
}
