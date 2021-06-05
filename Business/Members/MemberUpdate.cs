using Business.Cards;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Members;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Members
{
    public class MemberUpdate : IMemberUpdateService
    {
        private readonly ICardUpdateService _cardUpdateService;
        private readonly IMemberRepository _memberRepository;
        private readonly IBookItemRepository _bookItemRepository;

        public MemberUpdate(
            ICardUpdateService cardUpdateService,
            IMemberRepository memberRepository,
            IBookItemRepository bookItemRepository)
        {
            _cardUpdateService = cardUpdateService;
            _memberRepository = memberRepository;
            _bookItemRepository = bookItemRepository;
        }

        public void ChangeStatus(Guid memberId, MemberStatus newStatus)
        {
            if (newStatus == MemberStatus.Blacklisted || newStatus == MemberStatus.Closed)
            {
                _cardUpdateService.MakeAllCardsInactive(memberId);
                RemoveReservations(memberId);
            }

            Member member = _memberRepository.Get(memberId);
            member.ChangeStatus(newStatus);

            _memberRepository.Update(member);
            _memberRepository.SaveChanges();
        }

        private void RemoveReservations(Guid memberId)
        {
            IEnumerable<BookItem> bookItems = _bookItemRepository.GetByReservedMemberId(memberId);

            if (bookItems != null && bookItems.Any())
            {
                foreach (BookItem bookItem in bookItems)
                {
                    bookItem.RemoveReservation();
                }

                _bookItemRepository.UpdateRange(bookItems.ToList());
                _bookItemRepository.SaveChanges();
            }
        }
    }
}
