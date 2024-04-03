using BookRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class CheckCredentialsDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public User ToUser()
        {
            return new User
            {
                Email = this.Email,
                Password = this.Password
                // Include other properties if needed
            };
        }
    }
}
