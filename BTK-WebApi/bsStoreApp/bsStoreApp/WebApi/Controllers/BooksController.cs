﻿using Entities.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        //Dependency Injection ile Repository Contexti ekliyoruz
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _manager.BookService.GetAllBooks(false);
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
                var book = _manager
                    .BookService
                    .GetOneBookById(id, false);

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

                _manager.BookService.CreateOneBook(book);

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
                if (book is null)
                    return BadRequest();

                _manager.BookService.UpdateOneBook(id, book, true);

                return NoContent(); //204

                //Check book?
                //Update methodunda getbook yaparken trackChanges için izlemek istiyoruz, çünkü entityde değişiklikler yapacağız.
                //var entity = _manager.BookService.GetOneBookById(id, true);

                //if (entity is null)
                //    return NotFound();

                ////Check id?
                //if (id != book.Id)
                //    return BadRequest();

                //_repositoryContext.Books.Remove(entity);

                //Where ile entity'i aradığımız için entity ef core tarafından track ediliyor 
                //bu sebeple herhangi bir id kontrolü yapmamıza gerek kalmıyor.
                //entity.Title = book.Title;
                //entity.Price = book.Price;

                //_manager.Save();
                //return Ok();
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
                _manager.BookService.DeleteOneBook(id, false);
                return NoContent(); //204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPatch("{id:int}")]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] JsonPatchDocument<Book> bookPatch)
        {
            try
            {
                //Check Entity
                var entity = _manager
                    .BookService
                    .GetOneBookById(id, true);

                if (entity is null)
                    return NotFound(); //404

                bookPatch.ApplyTo(entity);
                _manager.BookService.UpdateOneBook(id, entity, true);


                return NoContent(); //204
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
