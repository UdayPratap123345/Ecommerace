using BookRental.Model;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookRental.DAL;
using BookRental.WebApi.DTO;
using System.Linq;
using System;

namespace BookRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _repository;

        public BookController(IRepository<Book> repository)
        {
            _repository = repository;
        }

        // GET: api/Book
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            var books = await _repository.GetAllAsync();
            // Map books to BookDTO if needed
            return Ok(books);
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookById(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Map book to BookDTO if needed
            return Ok(book);
        }

        // GET: api/Book/Available
        [HttpGet("Available")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAvailableBooks()
        {
            var availableBooks = (await _repository.GetAllAsync()).Where(b => b.IsBookAvailable);
            // Map availableBooks to BookDTO if needed
            return Ok(availableBooks);
        }

        // POST: api/Book
        [HttpPost]
        public async Task<IActionResult> AddBook(NewBookDTO newBookDTO)
        {
            // Map NewBookDTO to Book entity
            var book = new Book
            {
                // Map properties from newBookDTO to Book entity
                // For example:
                BookName = newBookDTO.BookName,
                Rating = newBookDTO.Rating,
                Description=newBookDTO.Description,
                CoverImage=newBookDTO.CoverImage,
                LentByUserId=newBookDTO.LentByUserId,
                IsBookAvailable=newBookDTO.IsBookAvailable,
                Genre=newBookDTO.Genre
                // ...other properties
            };

            await _repository.AddAsync(book);
            return Ok();
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAvailability(int id, bool isAvailable)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.IsBookAvailable = isAvailable;
                await _repository.UpdateAsync(book);

                return Ok(new { book });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("MakeBookAvailable/{id}")]
        public async Task<IActionResult> MakeBookAvailable(int id)
        {
            try
            {
                var book = await _repository.GetByIdAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.IsBookAvailable = true;
                await _repository.UpdateAsync(book);

                return Ok(new { book });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("LentBooks/{lentUserId}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooksByLentUserId(int lentUserId)
        {
            var lentBooks = (await _repository.GetAllAsync()).Where(b => b.LentByUserId == lentUserId);
            // Map lentBooks to BookDTO if needed
            return Ok(lentBooks);
        }
    }
}
