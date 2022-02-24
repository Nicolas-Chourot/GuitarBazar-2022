using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuitarBazar.Models
{
    public class SelectListItemConverter<T>
    {
        public static SelectList Convert(List<T> collection, bool noEmptyEntry = true)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            foreach(T item in collection)
            {
                items.Add(
                    new SelectListItem() { 
                    Value = typeof(T).GetProperty("Id").GetValue(item,null).ToString(), 
                    Text = typeof(T).GetProperty("Name").GetValue(item, null).ToString() 
                });
            }
            if (!noEmptyEntry)
                items.Insert(0, new SelectListItem { Value = "", Text = "Veuillez faire une sélection" });
            return new SelectList(items, "Value", "Text");
        }
    }
}