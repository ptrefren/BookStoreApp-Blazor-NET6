using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Models.Book;
using BookStoreApp.API.Static;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public BooksController(BookStoreContext context, IMapper mapper, ILogger logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookReadOnlyDto>>> GetBooks()
        {
            var bookDtos = await _context.Books
                .Include(q => q.Author)
                .ProjectTo<BookReadOnlyDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Ok(bookDtos);
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookDetailDto>> GetBook(int id)
        {
       
          var bookDto = await _context.Books
              .Include(q => q.Author)
              .ProjectTo<BookDetailDto>(_mapper.ConfigurationProvider)
              .FirstOrDefaultAsync(q => q.Id == id);

            if (bookDto == null)
            {
                return NotFound();
            }

            return bookDto;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, BookUpdateDto bookDto)
        {
            if (id != bookDto.Id)
            {
                return BadRequest();
            }

            var book = await _context.Authors.FindAsync(id); 
            
            if (book == null) return NotFound();

            _mapper.Map(bookDto, book);
            _context.Entry(book).State = EntityState.Modified;
 
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BookCreateDto>> PostBook(BookCreateDto bookDto)
        {
            try
            {
                var book = _mapper.Map<Book>(bookDto);
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostBook), new { id = book.Id }, book);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing POST in {nameof(PostBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    _logger.LogWarning($"{nameof(Book)} record not found in {nameof(DeleteBook)} - ID: {id}");
                    return NotFound();
                }

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error performing DELETE in {nameof(DeleteBook)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private bool BookExists(int id)
        {
            return (_context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
