using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.DAL.Entities
{
    public class ContactEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(2, ErrorMessage = "The Minimum Length is 2")]
        public string Message { get; set; }
    }
}
