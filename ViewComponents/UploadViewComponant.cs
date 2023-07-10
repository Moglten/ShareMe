using File_Sharing.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.ViewComponents
{
    [ViewComponent(Name="UploadsList")]
    public class UploadViewComponant : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(IEnumerable<UploadViewModel> Uploads)
        {
            return Task.FromResult<IViewComponentResult>(View(Uploads));
        }
    }
}
