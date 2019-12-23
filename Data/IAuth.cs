using AdoptionApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdoptionApi.Data
{
    public interface IAuth
    {
        Task<Users> Register(Users user, string password);

        Task<Users> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}
