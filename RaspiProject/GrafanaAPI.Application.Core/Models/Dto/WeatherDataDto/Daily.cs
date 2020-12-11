using System;

namespace GrafanaAPI.Application.Core.Models.Dto
{
    public partial class Daily
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
