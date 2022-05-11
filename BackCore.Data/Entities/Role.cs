using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{
    
    public class Role:IdentityRole
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
