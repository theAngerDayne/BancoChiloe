using System;

namespace API.Dtos.Cliente
{
    public class UpdateClienteDto
    {
        public int Id { get; set; }
        public string Rut { get; set; }

        public string Nombre { get; set; }


        public string Apellidos { get; set; }


        public DateTime FechaNacimiento { get; set; }


        public string Direccion { get; set; }


        public string Telefono { get; set; }
    }
}