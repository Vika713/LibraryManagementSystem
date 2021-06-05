using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories.BookItems
{
    public interface IBookItemRepository : IRepository<BookItem>
    {
        BookItem Get(Guid id);
        BookItem GetByBarcode(string barcode);
        IEnumerable<BookItem> GetFilteredAndPaginated(
            int skipNumber,
            int takeNumber,
            string ISBN,
            string barcode);
        IEnumerable<BookItem> GetPaginatedByBookId(Guid bookId, int skipNumber, int takeNumber);
        IEnumerable<BookItem> GetPaginatedByRackId(Guid rackId, int skipNumber, int takeNumber);
        IEnumerable<BookItem> GetByRackIds(IEnumerable<Guid> rackIds);
        IEnumerable<BookItem> GetByRackId(Guid rackId);
        IEnumerable<BookItem> GetByBorrowedMemberId(Guid memberId);
        IEnumerable<BookItem> GetByReservedMemberId(Guid memberId);
        int GetCount(string ISBN, string barcode);
        int GetCountByBookId(Guid bookId);
        int GetCountByRackId(Guid rackId);
        bool ExistsByBarcode(string barcode);
        bool OtherBookItemsExists(Guid bookId);
    }
}
