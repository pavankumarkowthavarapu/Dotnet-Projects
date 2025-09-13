using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInventory.Data;
using ProductInventory.Models;
using System.Data.Entity;
using System.Diagnostics.Eventing.Reader;

namespace ProductInventory.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(AppDbContext context,IWebHostEnvironment webHostEnvironment)
        {
            appDbContext = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (ModelState.IsValid)
            {

                var model = appDbContext.products.ToList();
                return View(model);
            }
            else
            {
                return NotFound();
            }



        }

        [HttpGet]
        public IActionResult Details(int id)
        {

           var details= appDbContext.products.Find(id);
           return View(details);
        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if(product.ProductImage !=null)
                {
                    string folder = "images/products/";
                    folder +=Guid.NewGuid().ToString()+"_"+product.ProductImage.FileName;
                    //"/" +
                    product.ImageUrl = folder;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);
                    product.ProductImage.CopyTo(new FileStream(serverFolder,FileMode.Create));

                }
                var products = new Product()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Category = product.Category,
                    ImageUrl=product.ImageUrl



                };
                appDbContext.products.Add(products);
                appDbContext.SaveChanges();
                return RedirectToAction("Index", "Product");
            }
            else
            {
                return View(product);
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
           var productmodel= appDbContext.products.Find(id);
            return View(productmodel);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid)
                return View(product); // show form again with validation errors

            var productModel = appDbContext.products.Find(product.Id);
            if (productModel == null)
                return NotFound();

            // Update basic fields
            productModel.Name = product.Name;
            productModel.Price = product.Price;
            productModel.Quantity = product.Quantity;
            productModel.Category = product.Category;

            // Handle image upload if a new one is selected
            if (product.ProductImage != null)
            {
                // Delete the old image
                if (!string.IsNullOrEmpty(productModel.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath,  productModel.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {

                        System.IO.File.Delete(oldImagePath);
                    }

                }

                // Save the new image
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ProductImage.FileName;
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    product.ProductImage.CopyTo(stream);
                }

                productModel.ImageUrl = "images/products/"+ uniqueFileName; // store only the filename
            }

            appDbContext.SaveChanges();

            return RedirectToAction("Index", "Product");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var productmodel=appDbContext.products.Find(id);
            return View(productmodel);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            if (!ModelState.IsValid)
            {
               var model =appDbContext.products.Find(product.Id);
                if(model==null)
                {
                    return NotFound();
                }
                else
                {
                    // Delete the old image
                if (!string.IsNullOrEmpty(model.ImageUrl))
                {
                    var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, model.ImageUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {

                        System.IO.File.Delete(oldImagePath);
                    }

                }
                    appDbContext.products.Remove(model);
                    appDbContext.SaveChanges();
                    return RedirectToAction("Index", "Product");

                }

            }
            else
            {
                return NotFound();
            }    
               
        }




    }
}
