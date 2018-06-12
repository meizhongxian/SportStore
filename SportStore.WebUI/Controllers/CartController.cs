using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Models;

namespace SportStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo,IOrderProcessor proc)
        {
            repository = repo;
            orderProcessor = proc;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel { Cart = cart, ReturnUrl = returnUrl });
        }

        /// <summary>
        /// 加入购物车
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {

            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 从购物车删除
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {

            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        /// <summary>
        /// 购物车摘要
        /// </summary>
        /// <param name="cart"></param>
        /// <returns></returns>
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        /// <summary>
        /// 结算
        /// </summary>
        /// <param name="cart"></param>
        /// <param name="shippingDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails) {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("","Sorry,your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else {
                return View(shippingDetails);
            }
        }
    }
}