using System;
using System.Net.Http;
using Horizon.Common.Models.Configurations.HttpClientSettings;
using Horizon.Common.Models.Interfaces;
using Microsoft.Extensions.Options;

namespace Solana.Web.Admin.Clients.HttpClients
{
    //TODO: Add exception handling and/or logging
    //TODO: Might need to refactor out the SolanaIdentityUser headers into a base class
    public partial class AdminHttpClient
    {
        public HttpClient Client { get; }
        public ISolanaIdentityUser SolanaIdentityUser { get; set; }

        public AdminHttpClient(IOptions<HttpClientSettings> options, HttpClient client, ISolanaIdentityUser solanaIdentityUser)
        {
            client.BaseAddress = new Uri(options.Value.SolanaWebAdmin.BaseAddress);
            Client = client;
            SolanaIdentityUser = solanaIdentityUser;
        }
    }
}
