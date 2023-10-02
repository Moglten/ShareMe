using AutoMapper;
using AutoMapper.QueryableExtensions;
using File_Sharing.Data;
using File_Sharing.Data.DBModels;
using File_Sharing.Services.EmailService.Mail;
using File_Sharing.ViewModels;
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
        private readonly IMapper _mapper;

        public HomeController(ApplicationDbContext db
                                ,IEnumerable<IEmailService> emailService
                                ,IMapper mapper )
        {
            _db = db;
            _mapper = mapper;
            _emailService = emailService.FirstOrDefault(e => e.GetType() == typeof(SendContactEmail));
        }
 

        public IActionResult Index()
        {
            var mostRecentUploads = _db.Uploads
                .OrderByDescending(u => u.UploadDate)
                .Take(3)
                .ProjectTo<UploadViewModel>(_mapper.ConfigurationProvider);

            return View(mostRecentUploads);
        }
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Contact(EmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Database Model go to saved in database
                _db.Contacts.Add( _mapper.Map<Contact>(model));
                await _db.SaveChangesAsync();

                // Send Email by using EmailService with EmailServiceModel
                _ = _emailService.SendEmailAsync( _mapper.Map<EmailServiceModel>(model));

                TempData["Message"] = "Message has been sent successfully";

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
        public IActionResult SetCulture(string Lang)
        {
            if (!string.IsNullOrEmpty(Lang))
            {
                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(Lang)),
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
