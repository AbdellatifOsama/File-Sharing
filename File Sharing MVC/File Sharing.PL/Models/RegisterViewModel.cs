using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.PL.Models
{
    public class RegisterViewModel
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MaxLength(20)]
        [MinLength(2)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [EmailAddress]
        public string Email { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Phone]
        public string phoneNumber { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password Isn't Matched")]
        public string ConfirmPassword { get; set; }
    }
}
