
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.ViewModels
{
    public class  EmailViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Message { get; set; }
    }
}