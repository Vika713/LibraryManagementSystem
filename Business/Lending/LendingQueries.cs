using Business.Lending.DTOs;
using Common.Constants;
using Data.Repositories.BookItems;
using Domain.Models;
using System;

namespace Business.Lending
{
    public class LendingQueries : ILendingQueriesService
    {
        private readonly IBookItemRepository _bookItemRepository;

        public LendingQueries(IBookItemRepository bookItemRepository)
        {
            _bookItemRepository = bookItemRepository;
        }

        public LendingFineDTO GetFineDTO(string bookBarcode)
        {
            DateTime dueDate = (DateTime)_bookItemRepository.GetByBarcode(bookBarcode).DueDate;
            DateTime currentDate = DateTime.Today.Date;
            TimeSpan overdue = currentDate - dueDate;

            decimal fine = overdue.Days * Consts.FineRate;

            LendingFineDTO fineDTO = new LendingFineDTO
            {
                BookBarcode = bookBarcode,
                Fine = fine
            };

            return fineDTO;
        }

        public DateTime GetDueDate(string bookBarcode)
        {
            return (DateTime)_bookItemRepository.GetByBarcode(bookBarcode).DueDate;
        }

        public bool IsOverdue(string bookBarcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(bookBarcode);

            DateTime currentDate = DateTime.Today.Date;
            DateTime? dueDate = bookItem.DueDate;

            return currentDate > dueDate;
        }

        public bool BookIsReserved(string bookBarcode)
        {
            BookItem bookItem = _bookItemRepository.GetByBarcode(bookBarcode);

            return bookItem.ReservedMemberId != null;
        }
    }
}
