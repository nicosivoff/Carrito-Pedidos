using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarritoPedidos.Models;
using CarritoPedidos.Models.ViewModels;

namespace CarritoPedidos.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            List<ListProductViewModel> lst;
            using (CarritoPedidosEntities db = new CarritoPedidosEntities())
            {
                lst = (from d in db.Product
                           select new ListProductViewModel
                           {
                               Id = d.id,
                               Name = d.name,
                               Description = d.description,
                               Price = (float)d.price
                           }).ToList();
            }


            return View(lst);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ListProductViewModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CarritoPedidosEntities db = new CarritoPedidosEntities())
                    {
                        var oProduct = new Product();
                        oProduct.name = Model.Name;
                        oProduct.description = Model.Description;
                        oProduct.price = (float)Model.Price;

                        db.Product.Add(oProduct);
                        db.SaveChanges();
                    }
                    return Redirect("~/Product/");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ActionResult Edit(int Id)
        {
            ListProductViewModel model = new ListProductViewModel();
            using (CarritoPedidosEntities db = new CarritoPedidosEntities())
            {
                var oProduct = db.Product.Find(Id);
                model.Name = oProduct.name;
                model.Description = oProduct.description;
                model.Price = (float)oProduct.price;
                model.Id = oProduct.id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ListProductViewModel Model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CarritoPedidosEntities db = new CarritoPedidosEntities())
                    {
                        var oProduct = db.Product.Find(Model.Id);
                        oProduct.name = Model.Name;
                        oProduct.description = Model.Description;
                        oProduct.price = (float)Model.Price;

                        db.Entry(oProduct).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Redirect("~/Product/");
                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            using (CarritoPedidosEntities db = new CarritoPedidosEntities())
            {
                var oProduct = db.Product.Find(Id);
                db.Product.Remove(oProduct);
                db.SaveChanges();
            }
            return Redirect("~/Product/");
        }
    }
}