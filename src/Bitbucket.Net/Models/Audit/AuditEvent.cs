using Bitbucket.Net.Models.Core.Users;

namespace Bitbucket.Net.Models.Audit
{
    public class AuditEvent
    {
        public string Action { get; set; }
        public long Timestamp { get; set; }
        public string Details { get; set; }
        public User User { get; set; }
    }
}
