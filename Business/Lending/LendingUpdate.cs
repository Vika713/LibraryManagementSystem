using Business.Lending.DTOs;
using Common.Constants;
using Data.Repositories.BookItems;
using Data.Repositories.Cards;
using Domain.Models;
using System;

namespace Business.Lending
{
    public class LendingUpdate : ILendingUpdateService
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly ICardRepository _cardRepository;

        public LendingUpdate(ICardRepository cardRepository, IBookItemRepository bookItemRepository)
        {
            _bookItemRepository = bookItemRepository;
            _cardRepository = cardRepository;
        }

        public void Lend(ScanDTO scanDTO)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(scanDTO.BookBarcode);

            Guid memberId = _cardRepository.GetByBarcode(scanDTO.CardBarcode).MemberId;

            bookItem.Lend(memberId);

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void Return(string bookBarcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(bookBarcode);

            bookItem.Return();

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void Renew(string bookBarcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(bookBarcode);

            bookItem.SetDueDate(DateTime.Today.Date.AddDays(Consts.MaxLendingDays));

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }
    }
}
