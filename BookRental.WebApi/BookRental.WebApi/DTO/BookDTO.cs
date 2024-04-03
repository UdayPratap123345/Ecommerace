using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRental.WebApi.DTO
{
    public class BookDTO
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public double Rating { get; set; }
        public bool IsBookAvailable { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int LentByUserId { get; set; }
        public string CoverImage { get; set; }
    }
}
