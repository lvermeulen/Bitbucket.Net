using System;

namespace Bitbucket.Net.Models.Admin
{
    public class LicenseDetails : LicenseInfo
    {
        public DateTime? CreationDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int NumberOfDaysBeforeExpiry { get; set; }
        public DateTime? MaintenanceExpiryDate { get; set; }
        public int NumberOfDaysBeforeMaintenanceExpiry { get; set; }
        public DateTime? GracePeriodEndDate { get; set; }
        public int NumberOfDaysBeforeGracePeriodExpiry { get; set; }
        public int MaximumNumberOfUsers { get; set; }
        public bool UnlimitedNumberOfUsers { get; set; }
        public string ServerId { get; set; }
        public string SupportEntitlementNumber { get; set; }
        public LicenseStatus Status { get; set; }
    }
}
