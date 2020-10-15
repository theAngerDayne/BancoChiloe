using System;

namespace API.Dtos.Cliente
{
    public class AddClienteDto
    {

        public string Rut { get; set; }

        public string Nombre { get; set; }


        public string Apellidos { get; set; }


        public DateTime FechaNacimiento { get; set; }


        public string Direccion { get; set; }


        public string Telefono { get; set; }
    }
}