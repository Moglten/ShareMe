
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.Models
{
    public class  EmailServiceModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}