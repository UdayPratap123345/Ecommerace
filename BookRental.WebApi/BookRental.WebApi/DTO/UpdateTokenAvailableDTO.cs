using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class UpdateTokenAvailableDTO
    {
        public int UserId { get; set; }
        public int AddTokens { get; set; }
    }
}
