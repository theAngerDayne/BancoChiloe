using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;

namespace API.Identity
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string username, string password);
        Task<bool> UserExists(string username);

        Task<ServiceResponse<List<User>>> GetAllUsers();
        /*
        falta update y delete
        */
    }
}