using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarritoPedidos.Models.ViewModels
{
    public class OrderProductViewModel
    {
        public ListProductViewModel Product;
        public int Quantity;

        public OrderProductViewModel()
        {

        }

        public OrderProductViewModel(ListProductViewModel product, int quantity)
        {
            this.Product = product;
            this.Quantity = quantity;
        }

        
    }
}