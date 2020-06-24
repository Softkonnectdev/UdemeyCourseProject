using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> context;
        public ProductCategoryManagerController()
        {
            context = new InMemoryRepository<ProductCategory>();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCates = context.Collection().ToList();

            return View(productCates);
        }

        public ActionResult Create()
        {
            ProductCategory prodCate = new ProductCategory();
            return View(prodCate);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductCategory prodCate)
        {
            if (!ModelState.IsValid)
            {
                return View(prodCate);
            }
            else
            {
                context.Insert(prodCate);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            var prodCate = context.Find(Id);

            if (prodCate == null)
            {
                return HttpNotFound();
            }
            else
            {

                return View(prodCate);
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductCategory pc, string Id)
        {
            var prodCateToEdit = context.Find(Id);

            if (prodCateToEdit != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(pc);

                }

                else
                {
                    prodCateToEdit.Name = pc.Name;

                    context.Commit();

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
            var prodCateToDelete = context.Find(Id);
            if (prodCateToDelete == null)
            {
                throw new Exception("Product Category does not exist!");
            }

            else
            {
                return View(prodCateToDelete);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult ConfrimDelete(string Id)
        {
            var prodCateToDelete = context.Find(Id);

            if (prodCateToDelete == null)
            {
                throw new Exception("Product Category not found!");
            }
            else
            {
                context.Delete(prodCateToDelete);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}