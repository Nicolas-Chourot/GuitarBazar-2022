using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GuitarBazar.Models
{
    [MetadataType(typeof(GuitarView))]
    public partial class Guitar
    {
        public Guitar()
        {
            AddDate = DateTime.Now;
        }
       
    }
    public  class GuitarView
    {
        public int Id { get; set; }

        [Display(Name = "Fabriquant"), Required(ErrorMessage = "Obligatoire")]
        public string Maker { get; set; }

        [Display(Name = "Modèle"), Required(ErrorMessage = "Obligatoire")]
        public string Model { get; set; }

        [Display(Name = "Description"), Required(ErrorMessage = "Obligatoire")]
        public string Description { get; set; }

        [Display(Name = "Année"), Range(1800, 3000, ErrorMessage = "Invalide")]
        public int Year { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime AddDate { get; set; }

        [Display(Name = "État")]
        public int ConditionId { get; set; }

        [Display(Name = "Type")]
        public int GuitarTypeId { get; set; }

        [Display(Name = "Photo  "), Url(ErrorMessage = "Invalide")]
        public string ImageURL { get; set; }

        [Display(Name = "Rotation")]
        public bool RotateImage { get; set; }

        [Display(Name = "Prix")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "Vendu")]
        public bool Sold { get; set; }

        [Display(Name = "Vendeur"), Required(ErrorMessage = "Obligatoire")]
        public int SellerId { get; set; }
    }
}