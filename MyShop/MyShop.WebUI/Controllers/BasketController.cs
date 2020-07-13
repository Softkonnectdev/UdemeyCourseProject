using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyShop.Core.Contracts;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;

        public BasketController(IBasketService Basketservice)
        {
            this.basketService = Basketservice;
        }

        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItem(this.HttpContext);
            return View(model);
        }

        public ActionResult AddToBasket(string id)
        {
            basketService.AddToBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string id)
        {
            basketService.RemoveBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketsum = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketsum);
        }
    }
}