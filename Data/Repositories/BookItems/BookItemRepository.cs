using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.BookItems
{
    public class BookItemRepository : Repository<BookItem>, IBookItemRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public BookItemRepository(LibraryContext context) : base(context) { }

        public BookItem Get(Guid id)
        {
            return Query().Single(db => db.Id == id);
        }

        public BookItem GetByBarcode(string barcode)
        {
            return Query().SingleOrDefault(db => db.Barcode == barcode);
        }

        public IEnumerable<BookItem> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string ISBN,
            string barcode)
        {
            return GetFiltered(ISBN, barcode).Skip(skipNumber).Take(takeNumber);
        }

        public IEnumerable<BookItem> GetPaginatedByBookId(Guid bookId, int skipNumber, int takeNumber)
        {
            return GetByBookId(bookId).Skip(skipNumber).Take(takeNumber);
        }

        public IEnumerable<BookItem> GetPaginatedByRackId(Guid rackId, int skipNumber, int takeNumber)
        {

            return GetByRackId(rackId).Skip(skipNumber).Take(takeNumber);
        }

        public IEnumerable<BookItem> GetByRackIds(IEnumerable<Guid> rackIds)
        {
            IEnumerable<BookItem> bookItems = Query()
                .Where(bi => rackIds.Contains(bi.RackId ?? Guid.Empty));

            return bookItems;
        }

        public IEnumerable<BookItem> GetByRackId(Guid rackId)
        {
            return Query()
                .Where(db => db.RackId == rackId);
        }

        public IEnumerable<BookItem> GetByBorrowedMemberId(Guid memberId)
        {
            return Query().Where(bi => bi.BorrowedMemberId == memberId);
        }

        public IEnumerable<BookItem> GetByReservedMemberId(Guid memberId)
        {
            return Query().Where(bi => bi.ReservedMemberId == memberId);
        }

        public int GetCount(string ISBN, string barcode)
        {
            return GetFiltered(ISBN, barcode).Count();
        }

        public int GetCountByBookId(Guid bookId)
        {
            return GetByBookId(bookId).Count();
        }

        public int GetCountByRackId(Guid rackId)
        {
            return GetByRackId(rackId).Count();
        }

        public bool OtherBookItemsExists(Guid bookId)
        {
            return GetByBookId(bookId).Any();
        }

        public bool ExistsByBarcode(string barcode)
        {
            return GetByBarcode(barcode) != null;
        }

        private IQueryable<BookItem> Query()
        {
            return LibraryContext.BookItems
                .OrderByDescending(b => b.CreatedAt);
        }

        private IEnumerable<BookItem> GetFiltered(string ISBN, string barcode)
        {
            return Query()
                .Where(bi =>
                   (ISBN == null || bi.Book.ISBN.Contains(ISBN)) &&
                   (barcode == null || bi.Barcode.ToLower().Contains(barcode.ToLower())));
        }

        private IEnumerable<BookItem> GetByBookId(Guid bookId)
        {
            return Query().Where(db => db.BookId == bookId);
        }
    }
}
