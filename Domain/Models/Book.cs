using Common.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        public string ISBN { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string Title { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string Subject { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string Publisher { get; protected set; }

        [MaxLength(Consts.MaxDbCharCount)]
        public string Language { get; protected set; }

        [Range(0, Consts.MaxIntNumber)]
        public int NumberOfPages { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public List<BookAuthor> BookAuthors { get; protected set; }

        public List<BookItem> BookItems { get; protected set; }

        public Book()
        {
        }

        public Book(string ISBN, string title, string subject, string publisher, string language, int numberOfPages)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            this.ISBN = ISBN;
            Title = title;
            Subject = subject;
            Publisher = publisher;
            Language = language;
            NumberOfPages = numberOfPages;
            BookAuthors = new List<BookAuthor>();

            new BookValidator().ValidateAndThrow(this);
        }

        public void Edit(string ISBN, string title, string subject, string publisher, string language, int numberOfPages)
        {
            this.ISBN = ISBN;
            Title = title;
            Subject = subject;
            Publisher = publisher;
            Language = language;
            NumberOfPages = numberOfPages;

            new BookValidator().ValidateAndThrow(this);
        }

        public void AddAuthors(List<Author> authors)
        {
            foreach (Author author in authors)
            {
                BookAuthors.Add(new BookAuthor(author, this));
            }
        }

        public void RemoveAuthors(List<BookAuthor> bookAuthors)
        {
            BookAuthors = BookAuthors.Except(bookAuthors).ToList();
        }
    }

    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b.ISBN)
                .Must(x => x.Length == Consts.ISBNLength1 || x.Length == Consts.ISBNLength2);
        }
    }
}
