using Business.BookItems.DTOs;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using Domain.Models;
using System;

namespace Business.BookItems
{
    public class BookItemUpdate : IBookItemUpdateService
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IRackRepository _rackRepository;

        public BookItemUpdate(IBookItemRepository bookItemRepository, IRackRepository rackRepository)
        {
            _bookItemRepository = bookItemRepository;
            _rackRepository = rackRepository;
        }

        public void Create(BookItemCreateDTO createDTO)
        {
            Guid? rackId = null;

            if (createDTO.RackNumber != null && createDTO.LocationIdentifier != null)
            {
                rackId = _rackRepository.GetIdByNumberLocationIdentifier(
                                createDTO.RackNumber, createDTO.LocationIdentifier);
            }

            BookItem bookItem = new BookItem(
                createDTO.BookId, createDTO.Barcode,
                createDTO.PublicationDate,
                createDTO.Price, createDTO.Format,
                createDTO.DateOfPurchase, rackId);

            _bookItemRepository.Add(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void Edit(BookItemEditDTO editDTO)
        {
            BookItem bookItem = _bookItemRepository.Get(editDTO.BookItemId);

            bookItem.Edit(editDTO.Barcode, editDTO.PublicationDate, editDTO.Price, editDTO.Format, editDTO.DateOfPurchase);

            if (editDTO.Format == BookFormat.Audiobook || editDTO.Format == BookFormat.Ebook)
            {
                bookItem.SetRack(null);
            }
            else
            {
                if (editDTO.RackNumber != null && editDTO.LocationIdentifier != null)
                {
                    Guid rackId = _rackRepository
                        .GetIdByNumberLocationIdentifier(editDTO.RackNumber, editDTO.LocationIdentifier);
                    bookItem.SetRack(rackId);
                }
                else
                    bookItem.SetRack(null);
            }

            BookStatus currentStatus = _bookItemRepository.Get(editDTO.BookItemId).Status;

            if (currentStatus != editDTO.Status)
            {
                if (editDTO.Status == BookStatus.Lost || editDTO.Status == BookStatus.Available)
                {
                    bookItem.RemoveMembers();
                    bookItem.SetStatus(editDTO.Status);
                }
            }

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void Delete(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            _bookItemRepository.Remove(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void Reserve(BookItemReserveDTO reserveDTO)
        {
            BookItem bookItem = _bookItemRepository.Get(reserveDTO.BookItemId);

            bookItem.Reserve(reserveDTO.MemberId);

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }

        public void CancelReservation(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            bookItem.RemoveReservation();

            _bookItemRepository.Update(bookItem);
            _bookItemRepository.SaveChanges();
        }
    }
}
