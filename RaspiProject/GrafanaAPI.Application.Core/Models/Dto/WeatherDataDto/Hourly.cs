using System;

namespace GrafanaAPI.Application.Core.Models.Dto
{
    public partial class Hourly
    {
        public DateTimeOffset Date
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(Dt);
            }
        }
    }
}
