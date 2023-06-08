using File_Sharing.BLL.Interfaces;
using File_Sharing.DAL.Context;
using File_Sharing.DAL.Entities;
using File_Sharing.PL.Helper;
using File_Sharing.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace File_Sharing.PL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileRepository fileRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IContactRepository contactRepository;

        public HomeController(IFileRepository fileRepository, UserManager<IdentityUser> userManager,IContactRepository contactRepository)
        {
            this.fileRepository = fileRepository;
            this.userManager = userManager;
            this.contactRepository = contactRepository;
        }
        public IActionResult Index()
        {
            return View(fileRepository.GetPopular());
        }
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (q is not null)
            {
                var items = await fileRepository.Search(q);
                return View(items);
            }
            return RedirectToAction("Error", "Error");
        }
        public async Task<IActionResult> Browse()
        {
            var items = await fileRepository.GetAll();
            return View(items);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacts(ContactEntity model)
        {
            if (ModelState.IsValid)
            {
                await contactRepository.Add(model);
                return RedirectToAction("Contacts", "Home");
            }
            return View();
        }
    }
}