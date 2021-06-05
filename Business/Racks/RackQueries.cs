using Business.Racks.DTOs;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Racks;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Racks
{
    public class RackQueries : IRackQueriesService
    {
        private readonly IRackRepository _rackRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IBookRepository _bookRepository;

        public RackQueries(IRackRepository rackRepository, IBookItemRepository bookItemRepository, IBookRepository bookRepository)
        {
            _rackRepository = rackRepository;
            _bookItemRepository = bookItemRepository;
            _bookRepository = bookRepository;
        }

        public List<RacksIndexItemDTO> GetIndexItems(RacksFilterDTO filter, int pageIndex, int itemsCount)
        {
            IEnumerable<Rack> paginatedRacks = _rackRepository.GetFilteredAndPaginated(
                (pageIndex - 1) * itemsCount, itemsCount,
                filter?.Number, filter?.LocationId);

            List<RacksIndexItemDTO> racksDTO = GetIndexItemsList(paginatedRacks);

            return racksDTO;
        }

        public RackEditDTO GetEditDTO(Guid rackId, int pageIndex, int itemsCount)
        {
            Rack rack = _rackRepository.Get(rackId);

            IEnumerable<BookItem> bookItemsOnRack = _bookItemRepository
                .GetPaginatedByRackId(rack.Id, (pageIndex - 1) * itemsCount, itemsCount);

            RackEditDTO editDTO = new RackEditDTO()
            {
                RackId = rack.Id,
                RackNumber = rack.RackNumber,
                LocationIdentifier = rack.LocationIdentifier,
                BookItemsOnRack = GetRackBookItems(bookItemsOnRack.ToList(), true),
            };

            return editDTO;
        }

        public RackDeleteDTO GetDeleteDTO(Guid rackId)
        {
            Rack rack = _rackRepository.Get(rackId);

            RackDeleteDTO deleteDTO = new RackDeleteDTO()
            {
                Id = rack.Id,
                RackNumber = rack.RackNumber,
                LocationIdentifier = rack.LocationIdentifier
            };

            return deleteDTO;
        }

        public int GetItemsCount(RacksFilterDTO filter)
        {
            return _rackRepository.GetCount(filter?.Number, filter?.LocationId);
        }

        public int GetBookItemsCountByRack(Guid rackId)
        {
            return _bookItemRepository.GetCountByRackId(rackId);
        }

        private List<RacksIndexItemDTO> GetIndexItemsList(IEnumerable<Rack> racks)
        {
            List<RacksIndexItemDTO> indexItems = racks
                .Select(db => new RacksIndexItemDTO()
                {
                    Id = db.Id,
                    RackNumber = db.RackNumber,
                    LocationIdentifier = db.LocationIdentifier,
                })
                .ToList();

            IEnumerable<BookItem> bookItems = _bookItemRepository.GetByRackIds(indexItems.Select(r => r.Id));

            foreach (RacksIndexItemDTO item in indexItems)
            {
                item.HasBookItems = bookItems.Where(bi => bi.RackId == item.Id).Any();
            }

            return indexItems;
        }

        private List<RackBookItemsDTO> GetRackBookItems(List<BookItem> bookItems, bool selected)
        {
            List<RackBookItemsDTO> rackBookItems = bookItems.Select(bi => new RackBookItemsDTO()
            {
                BookItemId = bi.Id,
                Barcode = bi.Barcode,
                BookId = bi.BookId,
                PublicationDate = bi.PublicationDate,
                Format = bi.Format,
                Price = bi.Price,
                DateOfPurchase = bi.DateOfPurchase,
                Selected = selected
            }).ToList();

            IEnumerable<Book> books = _bookRepository.Get(
                bookItems.Select(bi => bi.BookId));

            foreach (RackBookItemsDTO rackBookItem in rackBookItems)
            {
                Book book = books.FirstOrDefault(b => b.Id == rackBookItem.BookId);
                rackBookItem.ISBN = book.ISBN;
                rackBookItem.Title = book.Title;
                rackBookItem.Subject = book.Subject;
                rackBookItem.Publisher = book.Publisher;
                rackBookItem.Language = book.Language;
                rackBookItem.NumberOfPages = book.NumberOfPages;
                rackBookItem.AuthorsNames = book.BookAuthors?.Select(ba => ba.Author.Name).ToList();
            }

            return rackBookItems;
        }
    }
}
