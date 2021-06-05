using Business.Books.DTOs;
using System;
using System.Collections.Generic;

namespace Business.Books
{
    public interface IBookQueriesService
    {
        List<BooksIndexItemDTO> GetIndexItems(BooksFilterDTO filter, int pageIndex, int pageSize);
        BookCreateDTO GetCreateDTO(string ISBN);
        BookEditDTO GetEditDTO(Guid bookId);
        BookDeleteDTO GetDeleteDTO(Guid bookId);
        int GetItemsCount(BooksFilterDTO filter);
        Guid GetBookId(string ISBN);
        bool BookExists(string ISBN);
    }
}
