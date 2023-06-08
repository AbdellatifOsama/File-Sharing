using File_Sharing.BLL.Interfaces;
using File_Sharing.DAL.Context;
using File_Sharing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.BLL.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly FileSharingContext context;

        public ContactRepository(FileSharingContext context)
        {
            this.context = context;
        }
        public async Task Add(ContactEntity contact)
        {
            context.Set<ContactEntity>().Add(contact);
            await context.SaveChangesAsync();
        }
    }
}
