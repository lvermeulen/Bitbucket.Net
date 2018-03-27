using System;
using Bitbucket.Net.Common.Converters;
using Newtonsoft.Json;

namespace Bitbucket.Net.Models.Core.Admin
{
    public class LicenseDetails : LicenseInfo
    {
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? CreationDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? PurchaseDate { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? ExpiryDate { get; set; }
        public int NumberOfDaysBeforeExpiry { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? MaintenanceExpiryDate { get; set; }
        public int NumberOfDaysBeforeMaintenanceExpiry { get; set; }
        [JsonConverter(typeof(UnixDateTimeOffsetConverter))]
        public DateTimeOffset? GracePeriodEndDate { get; set; }
        public int NumberOfDaysBeforeGracePeriodExpiry { get; set; }
        public int MaximumNumberOfUsers { get; set; }
        public bool UnlimitedNumberOfUsers { get; set; }
        public string ServerId { get; set; }
        public string SupportEntitlementNumber { get; set; }
        public LicenseStatus Status { get; set; }
    }
}
