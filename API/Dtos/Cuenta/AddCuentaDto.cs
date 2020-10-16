using System;
using API.Dtos.Cliente;

namespace API.Dtos.Cuenta
{
    public class AddCuentaDto
    {
        public TipoCuenta TipoCuenta { get; set; } = TipoCuenta.VISTA;
        public DateTime FechaApertura { get; set; }
        public decimal SaldoInicial { get; set; } = 0;
        public decimal Saldo { get; set; } = 0;
        public GetClienteDto Cliente { get; set; }
        public bool IsActive { get; set; } = true;
        public string NombreUsuario { get; set; }
    }
}