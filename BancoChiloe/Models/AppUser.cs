using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BancoChiloe.Models
{
    public class AppUser : IdentityUser
    {
        public string Nombre { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime UltimoAcceso { get; set; }

        [NotMapped]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string AuxPass { get; set; }

        [NotMapped]
        [Display(Name = "Confirmación de contraseña")]
        [Compare("AuxPass", ErrorMessage = "Su contraseña y su confimación de contraseña deben coincidir.")]
        public string ConfPass { get; set; }
    }
}
