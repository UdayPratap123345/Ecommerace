using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class UpdateBorrowBookDTO
    {
        public int BorrowId { get; set; }
        public bool ReturnType { get; set; }
    }

}
