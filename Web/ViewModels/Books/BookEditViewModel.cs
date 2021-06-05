using Common.Constants;
using Common.Resources;
using Data.Repositories.Books;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Books
{
    public class BookEditViewModel
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

    public class BookEditViewModelValidator : AbstractValidator<BookEditViewModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public BookEditViewModelValidator(
            IBookRepository bookRepository,
            IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _bookRepository = bookRepository;
            _localizer = stringLocalizer;

            RuleFor(b => b.Title).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Subject).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Publisher).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.Language).MaximumLength(Consts.MaxDbCharCount);
            RuleFor(b => b.NumberOfPages).LessThanOrEqualTo(Consts.MaxIntNumber);
            RuleFor(i => i.ISBN).Cascade(CascadeMode.Stop)
               .NotEmpty()
               .Must(x => x.Length == Consts.ISBNLength1 || x.Length == Consts.ISBNLength2)
                   .WithMessage(_localizer[LocalizationKeys.FormatNotValid])
               .Must(UniqueISBN).WithMessage(_localizer[LocalizationKeys.ISBNExists]);
        }

        private bool UniqueISBN(BookEditViewModel model, string ISBN)
        {
            Book book = _bookRepository.GetByISBN(ISBN);

            return (book == null || book.Id == model.BookId);
        }
    }
}
