using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace File_Sharing.Models
{
    public class InputUploadViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }

    public class UploadViewModel
    {
        public string Id { get; set; }
        public string OriginalFileName { get; set; }
        public string FileName { get; set; }

        public long Size { get; set; }

        public string FileType { get; set; }

        public long DownloadCount { get; set; }

        public DateTime UploadDate { get; set; }
    }
}
