using Microsoft.AspNetCore.Identity;

namespace CarPoolLibrary.Models;
public class User : IdentityUser
{
    public User() : base() { }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public static implicit operator string?(User? v)
    {
        throw new NotImplementedException();
    }
}
