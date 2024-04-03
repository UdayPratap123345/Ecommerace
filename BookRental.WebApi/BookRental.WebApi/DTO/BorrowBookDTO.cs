using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class BorrowBookDTO
    {
        public int BorrowId { get; set; }
        public int UserId { get; set; }
        public bool ReturnType { get; set; }
        public int BookId { get; set; }
    }
}
