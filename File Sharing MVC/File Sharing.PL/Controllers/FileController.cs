using File_Sharing.BLL.Interfaces;
using File_Sharing.BLL.Repositories;
using File_Sharing.DAL.Context;
using File_Sharing.DAL.Entities;
using File_Sharing.PL.Helper;
using File_Sharing.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace File_Sharing.PL.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private readonly IFileRepository fileRepository;
        private readonly UserManager<IdentityUser> userManager;

        public FileController(IFileRepository fileRepository, UserManager<IdentityUser> userManager)
        {
            this.fileRepository = fileRepository;
            this.userManager = userManager;
        }
        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [RequestFormLimits(MultipartBodyLengthLimit = 6104857600)]
        [HttpPost]
        public async Task<IActionResult> Upload(UploadFileViewModel UploadedFile)
        {
            var SignInUser = await userManager.GetUserAsync(User);

            if (UploadedFile.File is not null && SignInUser is not null)
            {
                FileEntity fileEntity = new FileEntity();
                fileEntity.Name = DocumentSetting.Upload(UploadedFile.File);
                fileEntity.ContentType = UploadedFile.File.ContentType;
                fileEntity.Size = UploadedFile.File.Length;
                fileEntity.IsPrivate = UploadedFile.IsPrivate;
                fileEntity.UploaderID = SignInUser.Id;
                fileEntity.ShowUploaderName = UploadedFile.ShowUploaderName;
                await fileRepository.Add(fileEntity);
                return RedirectToAction("index", "home");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> MyUploads()
        {
            var SignInUser = await userManager.GetUserAsync(User);
            if (SignInUser is not null)
            {
                return View(fileRepository.UserUploads(SignInUser.Id).Result);
            }
            return RedirectToAction("NotFound", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> DownloadFile([FromRoute] string id)
        {
            if (id != null)
            {
                var items = await fileRepository.Get(id);
                return View(items);
            }
            return RedirectToAction("NotFound", "Error");
        }
        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            if (fileName != null)
            {
                var item = await fileRepository.Get(fileName);
                if (item != null)
                {
                    item.DownloadsCount++;
                    await fileRepository.Update(item);
                    byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", fileName));
                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName.Substring(36));
                }
                return RedirectToAction("NotFound", "Error");
            }
            return RedirectToAction("Error", "Error");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
