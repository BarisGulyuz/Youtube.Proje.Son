using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeDersler.Helper;
using YoutubeDersler.Models.Context;
using YoutubeDersler.Models.ViewModels;

namespace YoutubeDersler.Controllers
{
    public class ProductController : Controller
    {
        private readonly YoutubeContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _env;

        public ProductController(YoutubeContext context, IToastNotification toastNotification, IWebHostEnvironment env)
        {
            _context = context;
            _toastNotification = toastNotification;
            _env = env;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var products = _context.Product.Include(x => x.Category).ToList();
            return View(products.OrderByDescending(x => x.Price));
        }
        public IActionResult ProdcutsByCategoryId(int id)
        {
            ViewBag.CategoryName = _context.Categories.FirstOrDefault(x => x.Id == id).CategoryName;
            var products = _context.Product.Where(x => x.CategoryId == id).ToList();
            return PartialView("_ProductByCategory", products);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var categories = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ProductVM productVM)
        {
            var categories = _context.Categories.ToList();
            ViewBag.CategoryId = new SelectList(categories, "Id", "CategoryName");

            FileUploadHelper fileUploadHelper = new FileUploadHelper(_env);
            if (ModelState.IsValid)
            {
                if (productVM.Picture != null)
                {
                    string filePath = await fileUploadHelper.ImageUpload(productVM.Picture, productVM.Product.Name);
                    productVM.Product.Picture = filePath;
                }
                else
                {
                    productVM.Product.Picture = "Default.jpg";
                }
                _context.Product.Add(productVM.Product);
                _context.SaveChanges();
                _toastNotification.AddSuccessToastMessage($"{productVM.Product.Name} successfully added");
                return RedirectToAction("Index");
            }
            return View(productVM);
        }

        [HttpGet]
        public IActionResult GetProduct(int id)
        {

            var product = _context.Product.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            if (product != null)
            {
                return PartialView("_GetProductPartial", product);
            }
            return NotFound();
        }
        public JsonResult MultipleDelete(int[] id)
        {
            foreach (var item in id)
            {
                var product = _context.Product.FirstOrDefault(x => x.Id == item);
                _context.Product.Remove(product);
                _context.SaveChanges();

            }
            return Json("");
        }
    }
}
