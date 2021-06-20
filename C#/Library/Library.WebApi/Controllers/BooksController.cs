using Library.Persistence;
using Library.Persistence.DTO;
using Library.Persistence.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Librarian")]
    public class BooksController : ControllerBase
    {
        private readonly ILibraryService _service;

        public BooksController(ILibraryService service)
        {
            _service = service;
        }

        // GET: api/Books
        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> GetBooks()
        {
            return _service.GetBooks().Select(list => (BookDto)list).ToList();
        }
        public ActionResult<BookDto> GetBook(int id)
        {
            try
            {
                return (BookDto)_service.GetBookByID(id);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles ="Librarian")]
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, BookDto book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (_service.UpdateBook((Book)book))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(int id)
        {
            if (_service.DeleteBook(id))
            {
                return Ok();
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [Authorize(Roles = "Librarian")]
        [HttpPost]
        public ActionResult<BookDto> PostBook(BookDto bookDto)
        {
            var book = _service.CreateBook((Book)bookDto);
            if (book == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            else
            {
                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, (BookDto)book);
            }


        }
    }
}
