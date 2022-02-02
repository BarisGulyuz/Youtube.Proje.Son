using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using YoutubeDersler.Extension;
using YoutubeDersler.Models;
using YoutubeDersler.Models.Context;
using YoutubeDersler.Models.ViewModels;

namespace YoutubeDersler.Controllers
{
    public class CategoryController : Controller
    {
        private readonly YoutubeContext _context;
        private readonly IToastNotification _toastNotification;

        public CategoryController(YoutubeContext context, IToastNotification toastNotification)
        {
            _context = context;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _context.Categories.Include(x => x.Products).ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartial");
        }
        [HttpPost]
        public async Task<IActionResult> Add(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage($"{category.CategoryName} category successfully added");
                var categoryModel = JsonSerializer.Serialize(new CategoryVM
                {
                    Category = category,
                    CategoryPartial = await this.RenderViewToStringAsync<Category>("_CategoryAddPartial", category),
                    IsValid = true
                });
                return Json(categoryModel);
            }
            var categoryErrorModel = JsonSerializer.Serialize(new CategoryVM
            {
                CategoryPartial = await this.RenderViewToStringAsync<Category>("_CategoryAddPartial", category),
                IsValid = false,
                Errors = ModelState.Values.SelectMany(x => x.Errors).ToList()
            });
            return Json(categoryErrorModel);
        }

        public JsonResult MultipleDelete(int[] id)
        {
            foreach (var item in id)
            {
                var category = _context.Categories.FirstOrDefault(x => x.Id == item);
                _context.Categories.Remove(category);
                _context.SaveChanges();

            }
            return Json("");
        }


    }
}
