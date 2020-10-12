using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Cuenta : BaseEntity
    {
        public TipoCuenta TipoCuenta { get; set; }
        public DateTime FechaApertura { get; set; }
        public decimal SaldoInicial { get; set; } = 0;
        public decimal Saldo { get; set; } = 0;
        public Cliente Cliente { get; set; }
        public bool IsActive { get; set; } = true;
        public string NombreUsuario { get; set; }

        [NotMapped]
        public int AuxClienteID { get; set; }
    }

    public enum TipoCuenta
    {
        CORRIENTE, AHORRO, VISTA
    }


}
