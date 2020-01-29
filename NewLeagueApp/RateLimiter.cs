using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewLeagueApp
{
    public class RateLimiter
    {
#pragma warning disable CS0169 // The field 'RateLimiter.sec' is never used
        private DateTime sec;
#pragma warning restore CS0169 // The field 'RateLimiter.sec' is never used
#pragma warning disable CS0169 // The field 'RateLimiter.min' is never used
        private DateTime min;
#pragma warning restore CS0169 // The field 'RateLimiter.min' is never used
#pragma warning disable CS0169 // The field 'RateLimiter.secCount' is never used
        private int secCount;
#pragma warning restore CS0169 // The field 'RateLimiter.secCount' is never used
#pragma warning disable CS0169 // The field 'RateLimiter.minCount' is never used
        private int minCount;
#pragma warning restore CS0169 // The field 'RateLimiter.minCount' is never used

        public RateLimiter()
        {
           
        }





    }
}
