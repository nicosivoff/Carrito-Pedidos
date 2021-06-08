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
                addToList(Id, lst);
                Session["Order"] = lst;

            }
            else
            {
                List<OrderProductViewModel> lst = (List<OrderProductViewModel>)Session["Order"];

                //Validates if the product has already been added
                int indexProduct = -1;
                for (int i = 0; i < lst.Count; i++)
                {
                    if (lst[i].Product.Id == Id)
                        indexProduct = i;
                }

                if (indexProduct == -1)
                    addToList(Id, lst);
                else
                    lst[indexProduct].Quantity++;

                Session["Order"] = lst;
            }
            return Redirect("~/Order/");
        }

        private void addToList(int Id, List<OrderProductViewModel> lst)
        {
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
        }

        public ActionResult SummarizeOrder()
        {
            return View();
        }
    }
}