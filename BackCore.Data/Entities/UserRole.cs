using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{

    public class UserRole:IdentityUserRole<string>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
