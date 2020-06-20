using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository context;
        ProductCategoryRepository Catecontext;
        public ProductManagerController()
        {
            context = new ProductRepository();
            Catecontext = new ProductCategoryRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.ListOfProducts().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            //ProductManagerViewModel viewModel = new ProductManagerViewModel();

            //viewModel.ProductCates = Catecontext.ListOfProductCates();
            //viewModel.Product = new Product();
            Product prod = new Product();
            ViewBag.Id = new SelectList(Catecontext.ListOfProductCates(), "Id", "Name");
            return View(prod);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product prod)
        {
            if (!ModelState.IsValid)
            {
                return View(prod);
            }
            else
            {
                context.InsertProduct(prod);
                context.CommitToCache();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            var prod = context.FindProd(Id);

            if (prod == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = prod;
                viewModel.ProductCates = Catecontext.ListOfProductCates();
                return View(viewModel);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product p, string Id)
        {
            var prodToEdit = context.FindProd(Id);

            if (prodToEdit != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(p);

                }

                else
                {
                    prodToEdit.Name = p.Name;
                    prodToEdit.Category = p.Category;
                    prodToEdit.Image = p.Image;
                    prodToEdit.Price = p.Price;
                    prodToEdit.Description = p.Description;

                    context.CommitToCache();

                    return RedirectToAction("Index");
                }
            }

            else
            {
                return HttpNotFound();
            }
        }


        public ActionResult Delete(string Id)
        {
            var prodToDelete = context.FindProd(Id);
            if (prodToDelete == null)
            {
                throw new Exception("Product does not exist!");
            }

            else
            {
                return View(prodToDelete);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfrimDelete(string Id)
        {
            var prodToDelete = context.FindProd(Id);

            if (prodToDelete == null)
            {
                throw new Exception("Product not found!");
            }
            else
            {
                context.Delete(Id);
                context.CommitToCache();
                return RedirectToAction("Index");
            }
        }

    }

}