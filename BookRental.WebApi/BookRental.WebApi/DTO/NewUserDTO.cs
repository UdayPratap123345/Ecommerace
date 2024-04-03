using BookRental.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class NewUserDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int Token_Available { get; set; }

        public User ToUser()
        {
            return new User
            {
                Email = this.Email,
                Password = this.Password,
                RoleId = this.RoleId,
                UserName = this.UserName,
                Token_Available = this.Token_Available
                // Include other properties if needed
            };
        }
    }
}
