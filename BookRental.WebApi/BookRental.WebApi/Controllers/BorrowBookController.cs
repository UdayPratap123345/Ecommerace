using BookRental.WebApi.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookRental.DAL;
using BookRental.Model;
using System.Linq;

namespace BookRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowBookController : ControllerBase
    {
        private readonly IRepository<BorrowedBook> _borrowBookRepository;

        public BorrowBookController(IRepository<BorrowedBook> borrowBookRepository)
        {
            _borrowBookRepository = borrowBookRepository;
        }

        [HttpPost("AddBorrow")]
        public async Task<IActionResult> AddBorrow(NewBorrowBookDTO newBorrowBookDTO)
        {
            var borrowBook = new BorrowedBook
            {
                UserId = newBorrowBookDTO.UserId,
                BookId = newBorrowBookDTO.BookId,
                ReturnType = newBorrowBookDTO.ReturnType
            };

            await _borrowBookRepository.AddAsync(borrowBook);

            return Ok();
        }

        [HttpPut("UpdateReturnType/{borrowId}")]
        public async Task<IActionResult> UpdateReturnType(int borrowId)
        {
            var borrowBook = await _borrowBookRepository.GetByIdAsync(borrowId);
            if (borrowBook == null)
                return NotFound();

            borrowBook.ReturnType = true;

            await _borrowBookRepository.UpdateAsync(borrowBook);

            return Ok();
        }

        [HttpGet("GetAllDetailsByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<BorrowedBook>>> GetAllDetailsByUserId(int userId)
        {
            var borrowBooks = await _borrowBookRepository.GetAllAsync();
            var userBorrows = borrowBooks.Where(b => b.UserId == userId).ToList();

            if (userBorrows.Count == 0)
                return NotFound();

            return Ok(userBorrows);
        }
    }
}
