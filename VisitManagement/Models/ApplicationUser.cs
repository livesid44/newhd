using Microsoft.AspNetCore.Identity;

namespace VisitManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public AuthenticationType AuthType { get; set; } = AuthenticationType.Password;
        public string? LdapUserId { get; set; }
    }
}
