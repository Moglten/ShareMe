using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.ViewModels
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long")]
        [MaxLength(12, ErrorMessage = "Name must be less than 20 characters long")]
        [Display(Name = "Username")]
        public string ShortName { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string PhoneNumber { get; set; }
    }

    public class ChangePasswordViewModel{
        [Required]
        public string CurrentPassword{get; set;}

        [Required]
        public string NewPassword{get; set;}

        [Compare("NewPassword")]
        public string ConfirmPassword{get; set;}
    }

    public class ForgetPasswordViewModel{
        [Required]
        [EmailAddress]
        public string Email{get; set;}
    }

    public class ResetPasswordViewModel{
        [Required]
        public string Token{get; set;}

        [Required]
        [EmailAddress]
        public string Email{get; set;}

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword{get; set;}

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string NewPasswordConfirmation{get; set;}   
    }
}
