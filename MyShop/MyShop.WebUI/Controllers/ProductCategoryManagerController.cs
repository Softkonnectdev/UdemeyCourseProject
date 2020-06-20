using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;
        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCates = context.ListOfProductCates().ToList();

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
                context.InsertProduct(prodCate);
                context.CommitToCache();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
        {
            var prodCate = context.FindProd(Id);

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
            var prodCateToEdit = context.FindProd(Id);

            if (prodCateToEdit != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(pc);

                }

                else
                {
                    prodCateToEdit.Name = pc.Name;

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
            var prodCateToDelete = context.FindProd(Id);
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
            var prodCateToDelete = context.FindProd(Id);

            if (prodCateToDelete == null)
            {
                throw new Exception("Product Category not found!");
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