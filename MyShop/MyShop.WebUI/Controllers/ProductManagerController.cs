using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;
        public ProductManagerController()
        {
            context = new ProductRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        //fill Product Details
        public ActionResult Create()
        {
            Product product = new Product();
            return View(product);
        }
        //Product Details post in
        [HttpPost]
        public ActionResult Create(Product product)
        {
            //for the data base check valdiation and if it isnt correct return the view of the product and validation errors
            if(!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();//save the changes.
                return RedirectToAction("index");

            }
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if(product==null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product,string Id)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
               if(!ModelState.IsValid)
                {
                    return View(product);
                }
                else
                {
                    productToEdit.Category = product.Category;
                    productToEdit.Description = product.Description;
                    productToEdit.Image = product.Image;
                    productToEdit.Name = product.Name;
                    productToEdit.Price = product.Price;

                    context.Commit();//save changes
                    return RedirectToAction("Index");

                }
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult confirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}