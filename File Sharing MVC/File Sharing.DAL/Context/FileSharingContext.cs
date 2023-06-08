using File_Sharing.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace File_Sharing.DAL.Context
{
    public class FileSharingContext : IdentityDbContext
    {
        public FileSharingContext(DbContextOptions<FileSharingContext> _context) : base(_context)
        {

        }
        DbSet<FileEntity> Files { get; set; }
        DbSet<ContactEntity> Contacts { get; set; }
    }
}
