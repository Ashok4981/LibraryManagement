using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using LibraryManagement.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryManagement.Controllers
{
        [Route("api/books")]
        [ApiController]
        public class BooksController : ControllerBase
        {
            private readonly LibraryDBContext _context;

            public BooksController(LibraryDBContext context)
            {
                _context = context;
            }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetAllBooks(int page = 1, int pageSize = 10)
        {
            var books = _context.Books
                .Include(b => b.Authors)
                .Select(b => new BookDto
                {
                    BookId = b.BookId,
                    BookName = b.BookName,
                    PublishedOn = b.PublishedOn,
                    Language = b.Language,
                    Genre = b.Genre,
                    FirstName = b.Authors.FirstName,
                    LastName = b.Authors.LastName
                })
            .ToList();

            
            var totalRecords = books.Count();

            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pagedData = books.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return Ok(new

            {

                TotalRecords = totalRecords,

                TotalPages = totalPages,

                Page = page,

                PageSize = pageSize,

                Data = pagedData

            });
        }
        

        [HttpGet("{id}")]
            public ActionResult<BookDto> GetBookById(int id)
            {
                var book = _context.Books
                    .Include(b => b.Authors)
                    .FirstOrDefault(b => b.BookId == id);

                if (book == null)
                    return NotFound();

                var bookDto = new BookDto
                {
                    BookId = book.BookId,
                    BookName = book.BookName,
                    PublishedOn = book.PublishedOn,
                    Language = book.Language,
                    Genre = book.Genre,
                    FirstName = book.Authors?.FirstName,
                    LastName = book.Authors?.LastName
                };

                return Ok(bookDto);
            }



            [HttpPut("{id}")]
            public ActionResult UpdateBook(int id, BookDto updatedBookDto)
            {
                 var bookToUpdate = _context.Books
                   .Include(b => b.Authors)
                   .FirstOrDefault(b => b.BookId == id);

                 if (bookToUpdate == null)
                     return NotFound();

            // To Update book properties
            bookToUpdate.BookName = updatedBookDto.BookName;
            bookToUpdate.PublishedOn = updatedBookDto.PublishedOn;
            bookToUpdate.Language = updatedBookDto.Language;
            bookToUpdate.Genre = updatedBookDto.Genre;

            
            if (bookToUpdate.Authors != null)
            {
                bookToUpdate.Authors.FirstName = updatedBookDto.FirstName;
                bookToUpdate.Authors.LastName = updatedBookDto.LastName;
            }
            _context.SaveChanges();

            return NoContent();
            }



            [HttpPost]
            public ActionResult<Book> CreateBook(Book newBook)
            {
            if (newBook == null)
                return BadRequest();

            _context.Books.Add(newBook);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetBookById), new { id = newBook.BookId }, newBook);
            
            }



            [HttpDelete("{id}")]
            public ActionResult DeleteBook(int id)
            {
               var bookToDelete = _context.Books
                .Include(b => b.Authors) 
                .FirstOrDefault(b => b.BookId == id);

               if (bookToDelete == null)
               return NotFound();

               _context.Books.Remove(bookToDelete);
               _context.SaveChanges();

                return NoContent();
            }

        [HttpGet]
        [Route("GetBook/{name}")]
        public IActionResult GetBookByName(string name)
        {
            var booksearch = _context.Books.SingleOrDefault(x => x.BookName == name);
            if (booksearch == null)
            {
                return NotFound($"Book with name '{name}' not found.");
            }
            var query = from book in _context.Books
                        where book.BookName.Equals(booksearch.BookName)
                        join author in _context.Author on book.AuthorId equals author.AuthorId
                        join publisher in _context.Publications on book.PublicationId equals publisher.PublicationId
                        select new
                        {
                            book.BookId,
                            book.BookName,
                            book.PublishedOn,
                            book.Language,
                            book.Genre,
                            author.FirstName,
                            author.LastName,
                            publisher.PublishingCompanyName
                        };
            return Ok(query);

        }
    }
}




