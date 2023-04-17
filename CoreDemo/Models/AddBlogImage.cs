using Microsoft.AspNetCore.Http;
using System;

namespace CoreDemo.Models
{
    public class AddBlogImage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Status { get; set; }
        public int CategoryID { get; set; }

        public string BlogThumbnailImage { get; set; }
        public IFormFile ThumbnailImage { get; set; }

        public string BlogImage { get; set; }
        public IFormFile Image { get; set; }
    }
}
