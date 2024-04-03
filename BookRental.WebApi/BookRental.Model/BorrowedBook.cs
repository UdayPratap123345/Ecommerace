using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookRental.Model
{
    public class BorrowedBook
    {
        [Key]
        public int BorrowId { get; set; }
        public int UserId { get; set; }
        public bool ReturnType { get; set; }
        public int BookId { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public Book? Book { get; set; }
        public User? User { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    }
}
