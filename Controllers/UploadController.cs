using File_Sharing.Data;
using File_Sharing.Services;
using File_Sharing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IWebHostEnvironment env;
        private readonly IUploadServices _uploadServices;
        public UploadController(IUploadServices uploadServices, IWebHostEnvironment _env)
        {
            _uploadServices = uploadServices;
            env = _env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _uploadServices.GetByUserId(userId );

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


               await _uploadServices.CreateAsync(new InputUpload
                {
                        OriginalFileName = UploadVm.File.FileName,
                        FileName = filename,
                        ContentType = UploadVm.File.ContentType,
                        Size = UploadVm.File.Length,
                        UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                });

                return RedirectToAction("Index");
            }
            return View(UploadVm);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string Id)
        {
            var currentLoggedinUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var selectedUpload = await _uploadServices.FindAsync(Id, currentLoggedinUserId);

            if (selectedUpload == null)
                return NotFound();

            return View(selectedUpload);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirm(string Id)
        {
            var currentLoggedinUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

           var DeleteConfirmed = await _uploadServices.DeleteAsync(Id, currentLoggedinUserId);

           if(!DeleteConfirmed)
                return NotFound();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SearchResult(string term, int RequiredPage = 1)
        {
            var results = _uploadServices.Search(term);
            return View(model:await GetPagedData(results, RequiredPage));
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Browse(int RequiredPage = 1)
        {

            // extract the total required page of uploads
            var results = _uploadServices.GetAll();

            return View(model:await GetPagedData(results, RequiredPage));
        }


        private async Task<List<UploadViewModel>> GetPagedData(IQueryable<UploadViewModel> uploadsData, int RequiredPage)
        {
            // Pagination paramater helper
            const int pageSize = 1;
            int totalPages = (int)Math.Ceiling((double)_uploadServices.GetTotalUploads() / pageSize);
            //Handle the Uplimit and the downlimit of the required page
            RequiredPage = RequiredPage < 1 ? 1 : RequiredPage;
            if (RequiredPage > totalPages)
            {
                RequiredPage = 1;
            }
            int skipCount = pageSize * (RequiredPage - 1);

            ViewBag.CurrentPage = RequiredPage;
            ViewBag.TotalPages = totalPages;

            return await uploadsData.Skip(skipCount).Take(pageSize).ToListAsync();
        }


        [HttpGet]
        public async Task<IActionResult> Download(string Id)
        {
            var selectedFile = await _uploadServices.FindDBAsync(Id);

            if (selectedFile == null)
            {
                return NotFound();
            }

            selectedFile.DownloadCount++;

            await _uploadServices.UpdateAsync(selectedFile);

            var path = "~/uploads/" + selectedFile.FileName;
            Response.Headers.Add("Expires", DateTime.Now.AddDays(-3).ToLongDateString());
            Response.Headers.Add("Cache-Control", "no-cache");
            return File(path, selectedFile.ContentType, selectedFile.OriginalFileName);
        }
    }
}
