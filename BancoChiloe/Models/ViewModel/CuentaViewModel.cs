using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoChiloe.Models.ViewModel
{
    public class CuentaViewModel
    {
        public Cuenta Cuenta { get; set; }
        public IEnumerable<Cliente> ListaClientes { get; set; }
    }
}
