using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgilitiesBoard.Helpers
{
    public class DateTimeHelper
    {
        public static string ElapsedDateTime(DateTime wasPosted)
        {
            var elapsed = DateTime.Now - wasPosted;
            if (elapsed.Days > 1)
                return $"Posted {elapsed.Days} days ago";
            if (elapsed.Days == 1)
                return $"Posted yesterday";
            if (elapsed.Days <= 0)
            {
                if (elapsed.Hours < 1)
                {
                    if(elapsed.Minutes <= 1)
                        return $"Posted now";

                    return $"Posted {elapsed.Minutes} minutes ago";
                }
            }

            return elapsed.ToString();
        }
    }
}