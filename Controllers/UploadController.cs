using File_Sharing.Data;
using File_Sharing.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace File_Sharing.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment env;
        public UploadController(ApplicationDbContext context, IWebHostEnvironment _env)
        {
            _db = context;
            env = _env;
        }


        [HttpGet]
        public IActionResult Index()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _db.Uploads.Where(u => u.UserId == userId)
                                    .OrderByDescending(u => u.UploadDate)
                                    .Select(u => new UploadViewModel
                                    {
                                        Id = u.Id,
                                        OriginalFileName = u.OriginalFileName,
                                        FileName = u.FileName,
                                        FileType = u.ContentType,
                                        Size = u.Size,
                                        UploadDate = u.UploadDate,
                                        DownloadCount = u.DownloadCount
   
                                    }); 

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(InputUploadViewModel UploadVm)
        {

            if (ModelState.IsValid)
            {
                var name = Guid.NewGuid().ToString();
                var extension = Path.GetExtension(UploadVm.File.FileName);
                var filename = name + extension;
                var root = env.WebRootPath;
                var path = Path.Combine(root, "uploads", filename);
                
                using (var fileStream = System.IO.File.Create(path))
                {
                    await UploadVm.File.CopyToAsync(fileStream);
                }


               await _db.Uploads.AddAsync(new Uploads
               {
                    OriginalFileName = UploadVm.File.FileName,
                    FileName = filename,  
                    ContentType = UploadVm.File.ContentType,
                    Size = UploadVm.File.Length,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
               });

                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public IActionResult Delete(string Id)
        {
            var selectedUpload = _db.Uploads.Find(Id);
            var uploadVM = new UploadViewModel
            {
                Id = selectedUpload.Id,
                FileName = selectedUpload.FileName,
                FileType = selectedUpload.ContentType,
                OriginalFileName = selectedUpload.OriginalFileName,
                Size = selectedUpload.Size,
                UploadDate = selectedUpload.UploadDate
            };

            return View(uploadVM);
        }

        [HttpPost]
        public IActionResult DeleteConfirm(string Id)
        {
            var upload = _db.Uploads.Find(Id);

            if (upload.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return NotFound();
            }

            _db.Uploads.Remove(upload);

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult SearchResult(string term)
        {
            var results = _db.Uploads.Where(u => u.OriginalFileName.Contains(term))
                .OrderByDescending(u => u.UploadDate)
                .Select(u => new UploadViewModel
                {
                    Id = u.Id,
                    OriginalFileName = u.OriginalFileName,
                    FileName = u.FileName,
                    FileType = u.ContentType,
                    Size = u.Size,
                    UploadDate = u.UploadDate

                });

            return View(results);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Browse()
        {
            var results = _db.Uploads.OrderByDescending(u => u.UploadDate)
                                    .Select(u => new UploadViewModel
                                    {
                                        Id = u.Id,
                                        OriginalFileName = u.OriginalFileName,
                                        FileName = u.FileName,
                                        FileType = u.ContentType,
                                        Size = u.Size,
                                        UploadDate = u.UploadDate

                                    });

            return View(results);
        }

        [HttpGet]
        public async Task<IActionResult> Download(string fileName)
        {
            var selectedFile = _db.Uploads.FirstOrDefault(u => u.FileName == fileName);

            if (selectedFile == null)
            {
                return NotFound();
            }

            selectedFile.DownloadCount++;

            _db.Update(selectedFile);
            await _db.SaveChangesAsync();

            var path = "~/uploads/" + selectedFile.FileName;
            Response.Headers.Add("Expires", DateTime.Now.AddDays(-3).ToLongDateString());
            Response.Headers.Add("Cache-Control", "no-cache");
            return File(path, selectedFile.ContentType, selectedFile.OriginalFileName); ;
        }
    }
}
