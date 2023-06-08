using File_Sharing.PL.Helper;
using File_Sharing.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace File_Sharing.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //Get Login View
        public IActionResult Login()
        {
            return View();
        }
        //Get Register View
        public IActionResult Register()
        {
            return View();
        }
        //Register Post Action
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel _user)
        {
            if (ModelState.IsValid)
            {
                if (!await IsEmailDuplicated(_user.Email))
                {
                    var User = new IdentityUser
                    {
                        UserName = _user?.Name.Trim(),
                        Email = _user?.Email,
                        PhoneNumber = _user?.phoneNumber
                    };
                    var result = await userManager.CreateAsync(User, _user.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(User, isPersistent: false);
                        return RedirectToAction("index", "home");
                    }
                }
                ModelState.AddModelError(string.Empty, "Email Is Already in Use");
            }
            return View(_user);
        }
        //Logout action
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }
        //Login Post Action 
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel _user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser? signedUser = await userManager.FindByEmailAsync(_user?.Email);
                if (signedUser is not null)
                {
                    var result = await signInManager.PasswordSignInAsync(signedUser?.UserName, _user?.Password, _user.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "home");
                    }
                    ModelState.AddModelError(string.Empty, "We can't log you in. Please check for an email from us, reset your password, or try again.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "We can't log you in. Please check for an email from us, reset your password, or try again.");
                }
            }
            return View(_user);
        }
        //forgetPassword And Email Sent
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgetPassword(string RecieverEmail)
        {
            var user = await userManager.FindByEmailAsync(RecieverEmail);
            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email isn't found");
                return View();
            }
            var UserToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var PasswordResetLink = Url.Action("resetPassword", "Account", new { Email = RecieverEmail, Token = UserToken }, Request.Scheme);
            await EmailSetting.SendEmail(RecieverEmail, "Password reset Link", PasswordResetLink);
            return RedirectToAction("EmailSentSuccess", "Account");
        }
        //Email Sent Successfully
        public IActionResult EmailSentSuccess()
        {
            return View();
        }
        //reset Page
        public IActionResult ResetPassword()
        {
            return View();
        } 
        public IActionResult ResetPasswordFailed()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            ResetPasswordViewModel ErrorModel = new ResetPasswordViewModel();
            if (model.Email is not null)
            {
                ErrorModel = model;
                ErrorModel.Email = model.Email;
                ErrorModel.Token = model.Token;
            }
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email??ErrorModel.Email);
                var result = userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.IsCompletedSuccessfully)
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            return RedirectToAction("ResetPasswordFailed","Account");
        }
        //Duplicate Email Check
        private async Task<bool> IsEmailDuplicated(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user is not null;
        }
    }
}
