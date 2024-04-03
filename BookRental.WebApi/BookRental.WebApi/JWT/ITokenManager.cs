using BookRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.JWT
{
    public interface ITokenManager
    {
        string GenerateToken(User user, string roleName);
    }
}
