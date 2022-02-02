using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeDersler.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IFormFile Picture { get; set; }
    }
}
