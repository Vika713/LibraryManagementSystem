using Common.Resources;
using Data.Repositories.Books;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Books
{
    public class BookDeleteViewModel
    {
        public Guid BookId { get; set; }

        public string ISBN { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Publisher { get; set; }

        public string Language { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.NumberOfPages)]
        public int NumberOfPages { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.Authors)]
        public List<AuthorName> AuthorsNames { get; set; }
    }

    public class BookDeleteViewModelValidator : AbstractValidator<BookDeleteViewModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public BookDeleteViewModelValidator(IBookRepository bookRepository, IStringLocalizer<ValidationMessagesResource> localizer)
        {
            _bookRepository = bookRepository;
            _localizer = localizer;

            RuleFor(b => b.BookId).Must(NoBookItems).WithMessage(_localizer[LocalizationKeys.BookHasBookItems]);
        }

        private bool NoBookItems(BookDeleteViewModel model, Guid bookId)
        {
            return !_bookRepository.HasBookItems(bookId);
        }
    }
}
