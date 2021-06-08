using CarritoPedidos.Models;
using CarritoPedidos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarritoPedidos.Controllers
{
    public class OrderController : Controller
    {
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


        public ActionResult AddProduct(int Id)
        {
            
            if (Session["Order"] == null)
            {
                List<OrderProductViewModel> lst = new List<OrderProductViewModel>();
                using (CarritoPedidosEntities db = new CarritoPedidosEntities())
                {
                    var oProduct = db.Product.Find(Id);
                    var Product = new ListProductViewModel();
                    Product.Id = oProduct.id;
                    Product.Name = oProduct.name;
                    Product.Description = oProduct.description;
                    Product.Price = (float)oProduct.price;
                    lst.Add(new OrderProductViewModel(Product, 1));
                }
                Session["Order"] = lst;

            } else
            {
                List<OrderProductViewModel> lst = (List<OrderProductViewModel>)Session["Order"];
                using (CarritoPedidosEntities db = new CarritoPedidosEntities())
                {

                    var oProduct = db.Product.Find(Id);
                    var Product = new ListProductViewModel();
                    Product.Id = oProduct.id;
                    Product.Name = oProduct.name;
                    Product.Description = oProduct.description;
                    Product.Price = (float)oProduct.price;
                    lst.Add(new OrderProductViewModel(Product, 1));
                }
                Session["Order"] = lst;
            }
            return View();
        }

    }
}