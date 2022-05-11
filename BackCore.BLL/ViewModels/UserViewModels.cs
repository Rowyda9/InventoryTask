using System;

namespace BackCore.BLL.ViewModels
{
    public class UserRoleViewModels
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public bool isAdd { get; set; }

        public string RoleName { get; set; }
    }
    public class UserForLoginViewModel
    {
        public string UserNameOrPhone { get; set; }
        public string Password { get; set; }
    }


    public class UserForRegisterViewModel
    {
       
        public string Email { get; set; }

 
        public string UserName { get; set; }

      
        public string Password { get; set; }


        public string ConfirmPassword { get; set; }

       
        public string PhoneNumber { get; set; }

        public bool isArabic { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }



    public class UserForDetailedViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }
        public DateTime Created { get; set; } 

        public string Notes { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public bool isActive { get; set; } 

    }

  
}
