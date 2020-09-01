using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoChiloe.Extensiones
{
    public static  class ExtensionListaClientes
    {
        public static IEnumerable<SelectListItem> ToSelectListItem<T>(this IEnumerable<T> items, string selectedValue)
        {
            return from item in items
                   select new SelectListItem
                   {
                       Text =item.GetPropertyValue("Rut") + " - " + item.GetPropertyValue("Nombre") +" "+ item.GetPropertyValue("Apellidos"),
                       Value = item.GetPropertyValue("Id"),
                       Selected = item.GetPropertyValue("Id").Equals(selectedValue)
                   };
        }
    }
}
