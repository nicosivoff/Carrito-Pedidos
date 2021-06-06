using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarritoPedidos.Models.ViewModels
{
    public class ListProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Precio")]
        public float Price { get; set; }
    }
}