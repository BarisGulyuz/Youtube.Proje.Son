using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YoutubeDersler.Models
{
    public class Category
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Min 3")]
        [MaxLength(50, ErrorMessage = "Max 50")]
        [Required(ErrorMessage = "Required")]
        public string CategoryName { get; set; }
        public List<Product> Products { get; set; }
    }
}
