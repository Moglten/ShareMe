using AutoMapper;
using File_Sharing.Data;
using File_Sharing.Data.DBModels;
using File_Sharing.Services.EmailService.Mail;
using File_Sharing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace File_Sharing.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUserExtender> userManager;
        private readonly SignInManager<AppUserExtender> signInManager;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUserExtender> _userManager,
                                 SignInManager<AppUserExtender> _signManager,
                                 IEnumerable<IEmailService> emailService,
                                 IMapper mapper)
        {
            userManager = _userManager;
            _emailService = emailService.FirstOrDefault(e => e.GetType() == typeof(SendConfirmationEmail));
            signInManager = _signManager;
            _mapper = mapper;

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginVM, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(
                                                LoginVM.Email, LoginVM.Password, true, false);
                if (result.Succeeded)
                {
                    // Get the ShortName or the Username of logged in user and add it to the ClaimsPrincipal
                    var targetedUser = await userManager.FindByEmailAsync(LoginVM.Email);
                    userManager.AddClaimAsync(targetedUser, new Claim(ClaimTypes.GivenName, targetedUser.ShortName)).Wait();

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("Create", "Upload");
                }else{
                    ModelState.AddModelError("", "Maybe the Email or the Password is incorrect");
                }
            }
            return View(LoginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserExtender
                {
                    ShortName = registerVM.ShortName,
                    Email = registerVM.Email,
                    UserName = registerVM.Email,
                    PhoneNumber = registerVM.PhoneNumber
                };

                var result = await userManager.CreateAsync(user,registerVM.Password);

                if (result.Succeeded)
                {
                    // Generate the token for the email confirmation
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("ConfirmEmail"
                                                        , "Account"
                                                        , new { userId = user.Id, token }, Request.Scheme);

                    // End of the code for adding the ShortName to the ClaimsPrincipal
                    ViewBag.ConfirmLink = confirmationLink;
                    return View("ConfirmtionEmail");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }else
            {
                ModelState.AddModelError("", "Invalid Registeration");
            }
            return View(registerVM);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ConfirmtionEmail()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId,[FromQuery] string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var currentUser = await userManager.FindByIdAsync(userId);
            if (currentUser == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(currentUser, token);

            if (result.Succeeded)
            {
                    // Get the ShortName or the Username of logged in user and add it to the ClaimsPrincipal
                    var targetedUser = await userManager.FindByEmailAsync(currentUser.Email);
                    userManager.AddClaimAsync(targetedUser, new Claim(ClaimTypes.GivenName, currentUser.ShortName)).Wait();

                await signInManager.SignInAsync(currentUser, false);
                return RedirectToAction("Create", "Upload");
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var currentUser =await userManager.GetUserAsync(User);

            if (currentUser == null)
                return NotFound();

            var model = _mapper.Map<UserViewModel>(currentUser);
            return View(model);
        }

        public IActionResult ExternalLogin(string provider){
           var properities = signInManager.ConfigureExternalAuthenticationProperties(provider, "/Account/ExternalResponse");
            return Challenge(properities, provider);
        }

        public async Task<IActionResult> ExternalResponse(){
            var info = await signInManager.GetExternalLoginInfoAsync();

            if(info == null){
                TempData["Message"] = "Login failed";
                return RedirectToAction("Login");
            }

            var LoginResult = await signInManager
                                    .ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, false);

            if(!LoginResult.Succeeded){
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                var firstname = info.Principal.FindFirstValue(ClaimTypes.GivenName);
                var lastname = info.Principal.FindFirstValue(ClaimTypes.Surname);

                var userToCreate = new AppUserExtender(){
                    Email = email,
                    UserName = email,
                    ShortName = firstname + " " + lastname
                };
            var createResult = await userManager.CreateAsync(userToCreate);

                if(createResult.Succeeded ){
                    var exLoginResult = await userManager.AddLoginAsync(userToCreate, info);
                    if(exLoginResult.Succeeded){
                        await signInManager.SignInAsync(userToCreate, false, info.LoginProvider);
                        return RedirectToAction("Index", "Home");
                    }else {
                        await userManager.DeleteAsync(userToCreate);
                    }
                }
            return RedirectToAction("Login");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
