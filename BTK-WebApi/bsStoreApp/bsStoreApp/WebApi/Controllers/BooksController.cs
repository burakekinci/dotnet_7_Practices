using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Models.Repositories;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Dependency Injection ile Repository Contexti ekliyoruz
        private readonly RepositoryContext _repositoryContext;

        public BooksController(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _repositoryContext.Books.ToList();
                return Ok(books);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById([FromRoute(Name = "id")] int id)
        {
            try
            {
                var book = _repositoryContext.Books.Where(b => b.Id.Equals(id)).FirstOrDefault();

                if (book is null)
                    return NotFound();

                return Ok(book);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();

                _repositoryContext.Books.Add(book);
                _repositoryContext.SaveChanges();
                return StatusCode(201, book);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{id:int}")]
        public IActionResult UpdateBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {
            try
            {
                //Check book?
                var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).FirstOrDefault();

                if (entity is null)
                    return NotFound();

                //Check id?
                if (id != book.Id)
                    return BadRequest();

                _repositoryContext.Books.Remove(entity);

                //Where ile entity'i aradığımız için entity ef core tarafından track ediliyor 
                //bu sebeple herhangi bir id kontrolü yapmamıza gerek kalmıyor.
                entity.Title = book.Title;
                entity.Price = book.Price;

                _repositoryContext.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name = "id")] int id)
        {
            try
            {
                var entity = _repositoryContext.Books.Where(b => b.Id.Equals(id)).FirstOrDefault();
                if (entity is null)
                    return NotFound(new
                    {
                        statusCode = 404,
                        message = $"Book with id:{id} not found!"
                    });

                _repositoryContext.Books.Remove(entity);
                _repositoryContext.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
