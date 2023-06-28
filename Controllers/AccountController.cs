using File_Sharing.Data;
using File_Sharing.Models;
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
        public AccountController(UserManager<AppUserExtender> _userManager, SignInManager<AppUserExtender> _signManager)
        {
            userManager = _userManager;
            signInManager = _signManager;
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
                var result = await signInManager.PasswordSignInAsync(LoginVM.Email, LoginVM.Password, true, false);
           
                if (result.Succeeded)
                {
                    
                    // Get the ShortName or the Username of logged in user and add it to the ClaimsPrincipal
                    var user = await userManager.FindByEmailAsync(LoginVM.Email);
                    userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, user.ShortName)).Wait();

                    // End of the code for adding the ShortName to the ClaimsPrincipal

                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("Create", "Upload");
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
                                                        , new { userId = user.Id, token = token }, Request.Scheme);

                    // Get the ShortName or the Username of logged in user and add it to the ClaimsPrincipal
                    var targetedUser = await userManager.FindByEmailAsync(user.Email);
                    userManager.AddClaimAsync(targetedUser, new Claim(ClaimTypes.GivenName, user.ShortName)).Wait();
                

                    // End of the code for adding the ShortName to the ClaimsPrincipal
                    return RedirectToAction("ConfirmEmail", "Account",confirmationLink);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
              
            return View(registerVM);
        }



        //Undone Confirm Email method should be done next vacation.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token){
            if(userId == null || token == null){
                return RedirectToAction("Index", "Home");
            }
            
            var user = await userManager.FindByIdAsync(userId);
            
            if(user == null){
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if(result.Succeeded){
               
                return View("Home","Index");
            }

            return View("Error");
            
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Info()
        {
            return View();
        }
    }
}
