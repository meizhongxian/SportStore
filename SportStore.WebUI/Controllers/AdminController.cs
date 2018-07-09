using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;

namespace SportStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        /// <summary>
        /// 产品列表页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        /// <summary>
        /// 创建产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ViewResult Create()
        {
            return View("Edit",new Product());
        }

        /// <summary>
        /// 编辑产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public ViewResult Edit(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            return View(product);
        }

        /// <summary>
        /// 编辑产品：保存
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has beensave", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }


        public ActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);

            if (deleteProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }
}