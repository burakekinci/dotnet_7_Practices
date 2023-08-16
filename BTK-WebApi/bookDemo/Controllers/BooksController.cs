using bookDemo.Data;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var book = ApplicationContext
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

            if (book is null)
                return NotFound();

            return Ok(book);
        }

    }
}
