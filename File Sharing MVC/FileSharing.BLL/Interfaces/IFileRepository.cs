using File_Sharing.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.BLL.Interfaces
{
    public interface IFileRepository
    {

        public Task Add(FileEntity file);
        public Task<IEnumerable<FileEntity>> GetPopular();
        public Task<IEnumerable<FileEntity>> Search(string name);
        public Task<IEnumerable<FileEntity>> GetAll();
        public Task<IEnumerable<FileEntity>> UserUploads(string _userId);
        public Task<FileEntity> Get(string _name);
        public Task Update(FileEntity file);


    }
}
