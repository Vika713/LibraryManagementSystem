using Business.Books.DTOs;
using Data.Repositories.Books;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Books
{
    public class BookQueries : IBookQueriesService
    {
        private readonly IBookRepository _bookRepository;

        public BookQueries(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<BooksIndexItemDTO> GetIndexItems(BooksFilterDTO filter, int pageIndex, int pageSize)
        {
            IEnumerable<Book> paginatedBooks = _bookRepository.GetFilteredAndPaginated(
                (pageIndex - 1) * pageSize, pageSize,
                filter?.Title, filter?.Author, filter?.Subject, filter?.PublicationDate);

            List<BooksIndexItemDTO> indexItems = GetIndexItemsList(paginatedBooks);

            return indexItems;
        }

        public BookCreateDTO GetCreateDTO(string ISBN)
        {
            BookCreateDTO createDTO = new BookCreateDTO
            {
                ISBN = ISBN
            };

            return createDTO;
        }

        public BookEditDTO GetEditDTO(Guid bookId)
        {
            Book book = _bookRepository.Get(bookId);

            BookEditDTO editDTO = new BookEditDTO()
            {
                BookId = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Subject = book.Subject,
                Publisher = book.Publisher,
                Language = book.Language,
                NumberOfPages = book.NumberOfPages,
                AuthorsNames = book.BookAuthors?
                        .Select(ba => new AuthorNameDTO()
                        {
                            Name = ba.Author.Name
                        })
                        .ToList(),
            };

            return editDTO;
        }

        public BookDeleteDTO GetDeleteDTO(Guid bookId)
        {
            Book book = _bookRepository.Get(bookId);

            BookDeleteDTO deleteDTO = new BookDeleteDTO()
            {
                BookId = book.Id,
                ISBN = book.ISBN,
                Title = book.Title,
                Subject = book.Subject,
                Publisher = book.Publisher,
                Language = book.Language,
                NumberOfPages = book.NumberOfPages,
                AuthorsNames = book.BookAuthors?
                        .Select(ba => new AuthorNameDTO()
                        {
                            Name = ba.Author.Name
                        })
                        .ToList()
            };

            return deleteDTO;
        }

        public int GetItemsCount(BooksFilterDTO filter)
        {
            return _bookRepository.GetCount(
                        filter?.Title, filter?.Author, filter?.Subject, filter?.PublicationDate);
        }

        public Guid GetBookId(string ISBN)
        {
            return _bookRepository.GetByISBN(ISBN).Id;
        }

        public bool BookExists(string ISBN)
        {
            return _bookRepository.GetByISBN(ISBN) != null;
        }

        private List<BooksIndexItemDTO> GetIndexItemsList(IEnumerable<Book> books)
        {
            List<BooksIndexItemDTO> indexItems = books
                .Select(db => new BooksIndexItemDTO()
                {
                    BookId = db.Id,
                    ISBN = db.ISBN,
                    Title = db.Title,
                    Subject = db.Subject,
                    Publisher = db.Publisher,
                    Language = db.Language,
                    NumberOfPages = db.NumberOfPages,
                    AuthorsNames = db.BookAuthors?.Select(ba => ba.Author.Name).ToList(),
                    PublicationDates = db.BookItems?.Select(bi => bi.PublicationDate)
                        .Select(date => new BooksIndexItemDTO.DateTimeItem()
                        {
                            Value = date
                        }).ToList(),
                    HasBookItems = db.BookItems?.Any()
                }).ToList();

            return indexItems;
        }
    }
}
