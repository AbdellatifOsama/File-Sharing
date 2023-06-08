using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.PL.Models
{
    public class ResetPasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
