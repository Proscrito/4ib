using System;

namespace Solana.Web.Admin.Models.Requests.Schools.NestedModels
{
    public class TimeZoneViewModel
    {
        private readonly TimeZoneInfo _timeZone;

        public TimeZoneViewModel(string id)
        {
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
        }

        public string Id => _timeZone.Id;
        public string Text => $"{_timeZone}";
    }
}
