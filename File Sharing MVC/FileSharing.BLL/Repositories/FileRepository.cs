using File_Sharing.BLL.Interfaces;
using File_Sharing.DAL.Context;
using File_Sharing.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Sharing.BLL.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileSharingContext context;

        public FileRepository(FileSharingContext context)
        {
            this.context = context;
        }
        public async Task Add(FileEntity file)
        {
            context.Add(file);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<FileEntity>> GetPopular()
        {
            return await context.Set<FileEntity>().Where(F => F.IsPrivate == false).OrderByDescending(F => F.DownloadsCount).Take(3).ToListAsync();
        }
        public async Task<IEnumerable<FileEntity>> Search(string name)
        {
            return await context.Set<FileEntity>().Where(f => f.Name.Substring(36).Contains(name)).ToListAsync();
        }
        public async Task<IEnumerable<FileEntity>> UserUploads(string _userId)
        {
            return await context.Set<FileEntity>().Where(f => f.UploaderID == _userId).ToListAsync();
        }

        public async Task<IEnumerable<FileEntity>> GetAll()
        {
            return await context.Set<FileEntity>().Where(F => F.IsPrivate == false).ToListAsync();
        }
        public async Task<FileEntity> Get(string _name)
        {
            return await context.Set<FileEntity>().FirstOrDefaultAsync(F => F.Name == _name);
        }
        public async Task Update(FileEntity file)
        {
            context.Update(file);
            await context.SaveChangesAsync();
        }
    }
}
