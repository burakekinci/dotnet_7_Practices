using bookDemo.Data;
using bookDemo.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public IActionResult GetBook([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationContext
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();

            if (book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();
                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound();

            book.Id = entity.Id;
            ApplicationContext.Books.Remove(entity);
            ApplicationContext.Books.Add(book);
            return Ok(book);
        }


        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateBook(
            [FromRoute(Name = "id")] int id,
            [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));

            if (entity is null)
                return NotFound();

            bookPatch.ApplyTo(entity);
            return NoContent();
        }


        [HttpDelete]
        public IActionResult DeleteAllBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook([FromRoute(Name = "id")] int id)
        {
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));

            if (entity is null)
                return NotFound(new
                {
                    statusCode = 404,
                    message = $"Book with id:{id} could not found!"
                });

            ApplicationContext.Books.Remove(entity);
            return NoContent(); //returns 204
        }
    }
}
