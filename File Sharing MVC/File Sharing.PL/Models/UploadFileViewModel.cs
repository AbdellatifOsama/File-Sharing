using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace File_Sharing.PL.Models
{
    public class UploadFileViewModel
    {
        [Required]
        public IFormFile? File { get; set; }
        [DisplayName("Is File Private?")]
        public bool IsPrivate { get; set; }
        [DisplayName("Show Your Name?")]
        public bool ShowUploaderName { get; set; }
    }
}
