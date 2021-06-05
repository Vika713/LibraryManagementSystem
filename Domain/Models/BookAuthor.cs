using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BookAuthor
    {
        [Required]
        public Guid AuthorId { get; protected set; }
        public Author Author { get; protected set; }

        [Required]
        public Guid BookId { get; protected set; }
        public Book Book { get; protected set; }

        public BookAuthor()
        {
        }

        public BookAuthor(Author author, Book book)
        {
            Author = author;
            AuthorId = author.Id;
            Book = book;
            BookId = book.Id;
        }
    }
}
