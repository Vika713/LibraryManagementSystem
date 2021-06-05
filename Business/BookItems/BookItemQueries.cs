using Business.BookItems.DTOs;
using Common.Enumeration;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.People;
using Data.Repositories.Racks;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.BookItems
{
    public class BookItemQueries : IBookItemQueriesService
    {
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IRackRepository _rackRepository;
        private readonly IPersonRepository _personRepository;

        public BookItemQueries(
            IBookItemRepository bookItemRepository,
            IBookRepository bookRepository,
            IRackRepository rackRepository,
            IPersonRepository personRepository)
        {
            _bookItemRepository = bookItemRepository;
            _bookRepository = bookRepository;
            _rackRepository = rackRepository;
            _personRepository = personRepository;
        }

        public List<BookItemsIndexItemDTO> GetIndexItems(BookItemsFilterDTO filter, int pageIndex, int pageSize)
        {
            IEnumerable<BookItem> bookItems = _bookItemRepository.GetFilteredAndPaginated(
                (pageIndex - 1) * pageSize, pageSize,
                filter?.ISBN, filter?.Barcode);

            List<BookItemsIndexItemDTO> indexItems = GetIndexItemsList(bookItems);

            return indexItems;
        }

        public List<BookItemsIndexItemDTO> GetIndexItemsByBook(Guid bookId, int pageIndex, int pageSize)
        {
            IEnumerable<BookItem> bookItems = _bookItemRepository.GetPaginatedByBookId(
                bookId, (pageIndex - 1) * pageSize, pageSize);

            List<BookItemsIndexItemDTO> indexItems = GetIndexItemsList(bookItems);

            return indexItems;
        }

        public List<BookItemsIndexItemDTO> GetIndexItemsByRack(Guid rackId, int pageIndex, int pageSize)
        {
            IEnumerable<BookItem> bookItems = _bookItemRepository.GetPaginatedByRackId(
                rackId, (pageIndex - 1) * pageSize, pageSize);

            List<BookItemsIndexItemDTO> indexItems = GetIndexItemsList(bookItems);

            return indexItems;
        }

        public BookItemDetailsDTO GetDetailsDTO(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            BookItemDetailsDTO detailsDTO = new BookItemDetailsDTO()
            {
                BookItemId = bookItem.Id,
                BookId = bookItem.BookId,
                Barcode = bookItem.Barcode,
                PublicationDate = bookItem.PublicationDate,
                Price = bookItem.Price,
                DateOfPurchase = bookItem.DateOfPurchase,
                Format = bookItem.Format,
                Status = bookItem.Status,
                BorrowingDate = bookItem.BorrowingDate,
                DueDate = bookItem.DueDate,
                BorrowerId = bookItem.BorrowedMemberId,
                ReserverId = bookItem.ReservedMemberId,
                HasOtherBookItems = _bookItemRepository.OtherBookItemsExists(bookItem.BookId)
            };

            if (bookItem.RackId != null)
            {
                detailsDTO.RackNumber = _rackRepository.GetRackNumber((Guid)bookItem.RackId);
                detailsDTO.LocationIdentifier = _rackRepository.GetLocationIdentifier((Guid)bookItem.RackId);
            }

            return detailsDTO;
        }

        public BookItemCreateDTO GetCreateDTO(Guid bookId)
        {
            BookItemCreateDTO createDTO = new BookItemCreateDTO
            {
                BookId = bookId,
                ISBN = _bookRepository.GetISBN(bookId),
                HasOtherBookItems = _bookItemRepository.OtherBookItemsExists(bookId),
            };

            return createDTO;
        }

        public BookItemCreateDTO UpdateRacksSelectList(BookItemCreateDTO createDTO)
        {
            createDTO.RackNumbers = GetSelectList(createDTO.RackNumber.ToString());
            createDTO.LocationIdentifiers = GetSelectList(createDTO.LocationIdentifier);

            return createDTO;
        }

        public BookItemEditDTO GetEditDTO(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            BookItemEditDTO editDTO = new BookItemEditDTO()
            {
                BookItemId = bookItem.Id,
                BookId = bookItem.BookId,
                Barcode = bookItem.Barcode,
                ISBN = _bookRepository.GetISBN(bookItem.BookId),
                PublicationDate = bookItem.PublicationDate,
                Price = bookItem.Price,
                DateOfPurchase = bookItem.DateOfPurchase,
                Format = bookItem.Format,
                Status = bookItem.Status,
                HasOtherBookItems = _bookItemRepository.OtherBookItemsExists(bookItem.BookId)
            };

            if (bookItem.RackId != null)
            {
                editDTO.RackNumber = _rackRepository.GetRackNumber((Guid)bookItem.RackId);
                editDTO.LocationIdentifier = _rackRepository.GetLocationIdentifier((Guid)bookItem.RackId);
            }

            editDTO = UpdateRacksSelectList(editDTO);

            return editDTO;
        }

        public BookItemEditDTO UpdateRacksSelectList(BookItemEditDTO editDTO)
        {
            editDTO.RackNumbers = GetSelectList(editDTO.RackNumber.ToString());
            editDTO.LocationIdentifiers = GetSelectList(editDTO.LocationIdentifier);

            return editDTO;
        }

        public BookItemDeleteDTO GetDeleteDTO(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            BookItemDeleteDTO deleteDTO = new BookItemDeleteDTO()
            {
                BookItemId = bookItem.Id,
                BookId = bookItem.BookId,
                Barcode = bookItem.Barcode,
                ISBN = _bookRepository.GetISBN(bookItem.BookId),
                PublicationDate = bookItem.PublicationDate,
                Price = bookItem.Price,
                DateOfPurchase = bookItem.DateOfPurchase,
                Format = bookItem.Format,
                Status = bookItem.Status,
                HasOtherBookItems = _bookItemRepository.OtherBookItemsExists(bookItem.BookId)
            };

            if (bookItem.RackId != null)
            {
                deleteDTO.RackNumber = _rackRepository.GetRackNumber((Guid)bookItem.RackId);
                deleteDTO.LocationIdentifier = _rackRepository.GetLocationIdentifier((Guid)bookItem.RackId);
            }

            return deleteDTO;
        }

        public BookItemReserveDTO GetReserveDTO(Guid bookItemId, string currentUserEmail)
        {
            BookItemReserveDTO reserveDTO = GetPartialReserveDTO(bookItemId);
            reserveDTO.MemberId = _personRepository.GetByEmail(currentUserEmail).Member.Id;

            return reserveDTO;
        }

        public BookItemReserveDTO GetReserveDTO(Guid bookItemId)
        {
            BookItemReserveDTO reserveDTO = GetPartialReserveDTO(bookItemId);

            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            reserveDTO.MemberId = (Guid)bookItem.ReservedMemberId;

            return reserveDTO;
        }

        public Dictionary<string, string> GetRackNumberList(string searchString)
        {
            List<string> filteredRackNumbers = new List<string>();

            filteredRackNumbers = _rackRepository.GetRackNumbers(searchString)
                .Select(r => r.ToString()).ToList();

            return GetPopulatedDictionary(filteredRackNumbers);
        }

        public Dictionary<string, string> GetLocationIdentifierList(string searchString)
        {
            List<string> filteredLocationIdentifiers = new List<string>();

            filteredLocationIdentifiers = _rackRepository.GetLocationIdentifiers(searchString)
                .Select(r => r.ToString()).ToList();

            return GetPopulatedDictionary(filteredLocationIdentifiers);
        }

        public int GetItemsCount(BookItemsFilterDTO filter)
        {
            return _bookItemRepository.GetCount(filter?.ISBN, filter?.Barcode);
        }

        public int GetItemsCountByBook(Guid bookId)
        {
            return _bookItemRepository.GetCountByBookId(bookId);
        }

        public int GetItemsCountByRack(Guid rackId)
        {
            return _bookItemRepository.GetCountByRackId(rackId);
        }

        private List<BookItemsIndexItemDTO> GetIndexItemsList(IEnumerable<BookItem> bookItems)
        {
            List<BookItemsIndexItemDTO> indexItems = bookItems
                .Select(db => new BookItemsIndexItemDTO()
                {
                    BookItemId = db.Id,
                    BookId = db.BookId,
                    RackId = db.RackId,
                    Barcode = db.Barcode,
                    PublicationDate = db.PublicationDate,
                    Format = db.Format,
                    Status = db.Status,
                    CanBeReserved = db.ReservedMemberId == null && db.Status != BookStatus.Lost,
                    IsReserved = db.ReservedMemberId != null
                }).ToList();

            IEnumerable<Book> books = _bookRepository.Get(bookItems.Select(bi => bi.BookId));
            IEnumerable<Rack> racks = _rackRepository.Get(
                bookItems.Where(bi => bi.RackId != null).Select(bi => bi.RackId.GetValueOrDefault()));

            foreach (BookItemsIndexItemDTO item in indexItems)
            {
                Book book = books.FirstOrDefault(b => b.Id == item.BookId);
                item.ISBN = book.ISBN;
                item.Title = book.Title;
                item.Subject = book.Subject;
                item.Publisher = book.Publisher;
                item.Language = book.Language;
                item.NumberOfPages = book.NumberOfPages;
                item.AuthorsNames = book.BookAuthors?.Select(ba => ba.Author?.Name).ToList();

                if (item.RackId != null)
                {
                    Rack rack = racks.FirstOrDefault(r => r.Id == item.RackId);
                    item.RackNumber = rack.RackNumber;
                    item.LocationIdentifier = rack.LocationIdentifier;
                }
            }

            return indexItems;
        }

        private List<SelectListItem> GetSelectList(string initialValue)
        {
            List<SelectListItem> list = new List<SelectListItem>
            {
                new SelectListItem()
                {
                    Text = initialValue,
                    Value = initialValue
                }
            };

            return list;
        }

        private BookItemReserveDTO GetPartialReserveDTO(Guid bookItemId)
        {
            BookItem bookItem = _bookItemRepository.Get(bookItemId);

            BookItemReserveDTO reserveDTO = new BookItemReserveDTO()
            {
                BookItemId = bookItemId,
                BookId = bookItem.BookId,
                HasOtherBookItems = _bookItemRepository.OtherBookItemsExists(bookItem.BookId)
            };

            return reserveDTO;
        }

        private Dictionary<string, string> GetPopulatedDictionary(List<string> values)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (string value in values)
            {
                dictionary.Add(value, value);
            }

            return dictionary;
        }
    }
}
