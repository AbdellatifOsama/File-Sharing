using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.DAL.Entities
{
    public class FileEntity
    {
        public Guid id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public long Size { get; set; }
        [Required]
        public string ContentType { get; set; }
        public int DownloadsCount { get; set; } = 0;
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public bool IsPrivate { get; set; }
        public bool ShowUploaderName { get; set; }
        public IdentityUser Uploader { get; set; }
        [Required]
        public string UploaderID { get; set; }
    }
}
