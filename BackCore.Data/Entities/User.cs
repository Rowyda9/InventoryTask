using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackCore.Data
{

    public class User:IdentityUser
    {
        public ICollection<UserRole> UserRoles { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
   
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool isActive { get; set; } = true;

    }
}
