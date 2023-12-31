﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public Book CreateOneBook(Book book)
        {
            _manager.Book.CreateOneBook(book);
            _manager.Save();
            return book;
        }

        public void DeleteOneBook(int id, bool trackChanges)
        {
            //Check Entity
            var entity = _manager
                .Book
                .GetOneBookById(id, trackChanges);

            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }

            _manager.Book.DeleteOneBook(entity);
            _manager.Save();


        }

        public IEnumerable<BookDto> GetAllBooks(bool trackChanges)
        {
            var books = _manager.Book.GetAllBooks(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public Book GetOneBookById(int id, bool trackChanges)
        {
            var book = _manager.Book.GetOneBookById(id, trackChanges);

            if (book is null)
                throw new BookNotFoundException(id);
            return book;
        }

        public void UpdateOneBook(int id, BookDtoForUpdate bookDto, bool trackChanges)
        {
            //Check Entity
            var entity = _manager
                .Book
                .GetOneBookById(id, trackChanges);

            if (entity is null)
            {
                throw new BookNotFoundException(id);
            }

            //Mapping
            entity = _mapper.Map<Book>(bookDto);

            _manager.Book.UpdateOneBook(entity);
            _manager.Save();

        }
    }
}
