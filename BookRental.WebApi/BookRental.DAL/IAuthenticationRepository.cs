using BookRental.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookRental.DAL
{
    public interface IAuthenticationRepository
    {
        int RegisterUser(User user);
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        User? CheckCredentials(User user);
        Task<User?> GetUserById(int userId); // Add this method to get a user by ID
        Task<int> UpdateUser(User user);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        string GetUserRole(int roleId);
    }
}
