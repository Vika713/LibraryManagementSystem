using Common.Constants;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.BookItems;
using Domain.Models;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace Web.ViewModels.BookItems
{
    public class BookItemReserveViewModel
    {
        public Guid BookItemId { get; set; }

        public Guid BookId { get; set; }

        public Guid MemberId { get; set; }

        public bool HasOtherBookItems { get; set; }
    }

    public class BookItemReserveViewModelValidator : AbstractValidator<BookItemReserveViewModel>
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

        public BookItemReserveViewModelValidator(IBookItemRepository bookItemRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
        {
            _bookItemRepository = bookItemRepository;
            _localizer = stringLocalizer;

            RuleFor(bi => bi.MemberId)
                .Must(MemberCanReserve).WithMessage(_localizer[LocalizationKeys.MemberCannotReserve]);
            RuleFor(bi => bi.BookItemId).Cascade(CascadeMode.Stop)
                .Must(BookCanBeReserved).WithMessage(_localizer[LocalizationKeys.BookCannotBeReserved])
                .Must(MemberCanReserveBookItem).WithMessage(_localizer[LocalizationKeys.MemberCannotReserveThisBook]);
        }

        private bool MemberCanReserve(BookItemReserveViewModel model, Guid id)
        {
            return _bookItemRepository.GetByReservedMemberId(id).Count() < Consts.MaxBooksPerMember;
        }

        private bool BookCanBeReserved(BookItemReserveViewModel model, Guid id)
        {
            BookItem bookItem = _bookItemRepository.Get(id);

            return (bookItem.Status != BookStatus.Lost && bookItem.ReservedMemberId == null);
        }

        private bool MemberCanReserveBookItem(BookItemReserveViewModel model, Guid id)
        {
            return _bookItemRepository.Get(id).BorrowedMemberId != model.MemberId;
        }

    }
}
