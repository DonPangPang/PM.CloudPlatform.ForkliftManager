using System;

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
    }
}