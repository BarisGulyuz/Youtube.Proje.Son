using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeDersler.Models.ViewModels
{
    public class CategoryVM
    {
        public Category Category { get; set; }
        public string CategoryPartial { get; set; }
        public bool IsValid { get; set; }
        public List<ModelError> Errors { get; set; }
    }
}
