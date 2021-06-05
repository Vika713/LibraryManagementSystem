using Business.Books.DTOs;
using Data.Repositories.Books;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Books
{
    public class BookUpdate : IBookUpdateService
    {
        private readonly IBookRepository _bookRepository;

        public BookUpdate(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Guid Create(BookCreateDTO createDTO)
        {
            Book book = new Book(
                createDTO.ISBN,
                createDTO.Title,
                createDTO.Subject,
                createDTO.Publisher,
                createDTO.Language,
                createDTO.NumberOfPages);

            createDTO.AuthorsNames = createDTO.AuthorsNames
                .Where(an => !string.IsNullOrWhiteSpace(an.Name)).Distinct().ToList();

            if (createDTO.AuthorsNames.Any())
            {
                List<Author> authorsToAdd = new List<Author>();

                IEnumerable<Author> authors = _bookRepository.GetAuthorsByNames(
                    createDTO.AuthorsNames.Select(an => an.Name));

                foreach (AuthorNameDTO authorName in createDTO.AuthorsNames)
                {
                    Author author = authors.FirstOrDefault(a => a.Name == authorName.Name);
                    if (author != null)
                    {
                        authorsToAdd.Add(author);
                    }
                    else
                    {
                        authorsToAdd.Add(new Author(authorName.Name));
                    }
                }

                book.AddAuthors(authorsToAdd);
            }

            _bookRepository.Add(book);
            _bookRepository.SaveChanges();

            return book.Id;
        }

        public void Edit(BookEditDTO editDTO)
        {
            editDTO.AuthorsNames = editDTO.AuthorsNames
                .Where(s => !string.IsNullOrWhiteSpace(s.Name)).ToList();

            IEnumerable<Author> bookAuthors = _bookRepository.GetAuthorsByBookId(editDTO.BookId);

            IEnumerable<Author> authorsToRemove = bookAuthors
                   .Where(ba => editDTO.AuthorsNames.All(an => an.Name != ba.Name));

            Book editedBook = _bookRepository.Get(editDTO.BookId);

            editedBook.Edit(
                editDTO.ISBN,
                editDTO.Title,
                editDTO.Subject,
                editDTO.Publisher,
                editDTO.Language,
                editDTO.NumberOfPages);

            if (authorsToRemove != null && authorsToRemove.Any())
            {
                List<BookAuthor> bookAuthorsToRemove = new List<BookAuthor>();

                foreach (Author author in authorsToRemove)
                {
                    bookAuthorsToRemove.AddRange(
                        editedBook.BookAuthors.Where(ba =>
                            ba.AuthorId == author.Id &&
                            ba.BookId == editDTO.BookId)
                        .ToList());
                }

                editedBook.RemoveAuthors(bookAuthorsToRemove);
            }

            IEnumerable<Author> authors = _bookRepository.GetAuthorsByNames(
                editDTO.AuthorsNames.Select(an => an.Name));

            List<Author> authorsToAdd = new List<Author>();
            List<Author> newAuthorsToAdd = new List<Author>();

            foreach (AuthorNameDTO authorName in editDTO.AuthorsNames)
            {
                Author author = authors.FirstOrDefault(a => a.Name == authorName.Name);
                if (author != null)
                {
                    if (editedBook.BookAuthors.Any(ba => ba.Author.Name == authorName.Name) == false)
                        authorsToAdd.Add(author);
                }
                else
                {
                    newAuthorsToAdd.Add(new Author(authorName.Name));
                }
            }

            editedBook.AddAuthors(authorsToAdd.Union(newAuthorsToAdd).ToList());

            _bookRepository.AddAuthors(newAuthorsToAdd);
            _bookRepository.Update(editedBook);
            _bookRepository.SaveChanges();

            if (authorsToRemove != null && authorsToRemove.Any())
                DeleteAuthors(authorsToRemove.Select(atr => atr.Id));
        }

        public void Delete(Guid bookId)
        {
            Book book = _bookRepository.Get(bookId);

            if (book.BookItems == null || !book.BookItems.Any())
            {
                _bookRepository.Remove(book);
                _bookRepository.SaveChanges();

                if (book.BookAuthors != null || book.BookAuthors.Any())
                {
                    IEnumerable<Guid> authorsToDeleteIds = book.BookAuthors.Select(ba => ba.Author.Id).ToList();
                    DeleteAuthors(authorsToDeleteIds);
                }
            }
        }

        private void DeleteAuthors(IEnumerable<Guid> authorsIds)
        {
            IEnumerable<Author> authorsToCheck = _bookRepository.GetAuthors(authorsIds);

            List<Author> authorsToDelete = authorsToCheck
                .Where(atc => atc.BookAuthors == null || !atc.BookAuthors.Any())
                .ToList();

            if (authorsToDelete.Any())
            {
                _bookRepository.RemoveAuthors(authorsToDelete);
                _bookRepository.SaveChanges();
            }
        }
    }
}
