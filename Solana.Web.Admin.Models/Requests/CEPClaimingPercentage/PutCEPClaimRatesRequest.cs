namespace Solana.Web.Admin.Models.Requests.CEPClaimingPercentage
{
    public class PutCEPClaimRatesRequest
    {
        public int AdmSiteId { get; set; }
        public int CepOption { get; set; }
        public decimal CepFreeRate { get; set; }
        public decimal CepFullPayRate { get; set; }
    }
}
