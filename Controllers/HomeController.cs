using File_Sharing.Data;
using File_Sharing.Helpers.Mail;
using File_Sharing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace File_Sharing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailService _emailService;

        public HomeController(ApplicationDbContext db,IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }
 

        public IActionResult Index()
        {
            var mostRecentUploads = _db.Uploads.OrderByDescending(u => u.UploadDate).Take(3).Select(u => new UploadViewModel
            {
                Id = u.Id,
                OriginalFileName = u.OriginalFileName,
                FileName = u.FileName,
                FileType = u.ContentType,
                Size = u.Size,
                UploadDate = u.UploadDate

            });

            return View(mostRecentUploads);
        }

       
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


       
        [HttpPost]
        public async Task<IActionResult> Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Contacts.Add(new Contact
                    {
                    Email=model.Email,
                    Message = model.Message,
                    Subject = model.Subject,
                    Name = model.Name,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    });

                await _db.SaveChangesAsync();
                TempData["Message"] = "Message has been sent successfully";

                _emailService.SendEmail(model);

                return RedirectToAction("Contact");

            }
            return View(model);
        }

   
        [HttpGet]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SetLang(string lang)
        {

            if (!string.IsNullOrEmpty(lang))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)}
                    );
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
