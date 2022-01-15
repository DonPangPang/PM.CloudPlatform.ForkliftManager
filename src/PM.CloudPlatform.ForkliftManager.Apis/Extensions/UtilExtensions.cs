using System;
using Microsoft.VisualBasic;

namespace PM.CloudPlatform.ForkliftManager.Apis.Extensions
{
    public static class UtilExtensions
    {
        public static DateTime? ToDateTimeOrNull(this object obj)
        {
            if (string.IsNullOrEmpty(obj.ToString()))
            {
                return null;
            }

            DateTime.TryParse(obj.ToString(), out var result);
            return result;
        }

        public static int HourDiff(this DateTime d1, DateTime d2)
        {
            TimeSpan ts = d2 - d1;
            return ts.Hours;
        }
    }
}