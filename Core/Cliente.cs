﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Core
{
    public class Cliente : BaseEntity
    {
        [Required]
        public string Rut { get; set; } //string unico? mmm validaremos esto en el controlador de cliente

        [Required, MinLength(3)]
        public string Nombre { get; set; }

        [Required, MinLength(3)]
        public string Apellidos { get; set; }

        [Required] //validacion va al controlador        
        public DateTime FechaNacimiento { get; set; }

        [Required, MinLength(10)]
        public string Direccion { get; set; }

        [MinLength(8)]
        public string Telefono { get; set; }

    }
}
