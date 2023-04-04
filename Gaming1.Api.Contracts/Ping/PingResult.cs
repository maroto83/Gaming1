using System;

namespace Gaming1.Api.Contracts.Ping
{
    public class PingResult
    {
        /// <summary>
        /// DateTime format yyyy-MM-ddTHH:mm:ss.fffZ
        /// </summary>
        public DateTime ServerTime { get; set; }
    }
}