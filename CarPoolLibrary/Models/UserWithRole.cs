using Microsoft.AspNetCore.Identity;

namespace CarPoolLibrary.Models;
public class UserWithRole
{
        public IdentityUser? User { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
        
}
