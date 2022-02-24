using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuitarBazar.Models
{
    [MetadataType(typeof(SellerView))]
    public partial class Seller
    {
    }
    public class SellerView
    {
        public int Id { get; set; }
        [Display(Name = "Nom"), Required(ErrorMessage = "Obligatoire")]
        public string Name { get; set; } // doit etre unique
        [Display(Name = "Courriel"), EmailAddress(ErrorMessage = "Invalide"), Required(ErrorMessage = "Obligatoire")]
        public string Email { get; set; }
        [Display(Name = "Téléphone"), Required(ErrorMessage = "Obligatoire")]
        public string Phone { get; set; }
    }
}