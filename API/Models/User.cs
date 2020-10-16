using System.Collections.Generic;
using BancoChiloe.Models;

namespace API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Cuenta> Characters { get; set; }

    }
}