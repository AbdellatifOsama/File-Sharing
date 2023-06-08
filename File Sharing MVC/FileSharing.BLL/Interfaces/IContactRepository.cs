using File_Sharing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.BLL.Interfaces
{
    public interface IContactRepository
    {
        public Task Add(ContactEntity contact);
    }
}
