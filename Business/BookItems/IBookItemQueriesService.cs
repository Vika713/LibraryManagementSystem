using Business.BookItems.DTOs;
using System;
using System.Collections.Generic;

namespace Business.BookItems
{
    public interface IBookItemQueriesService
    {
        List<BookItemsIndexItemDTO> GetIndexItems(BookItemsFilterDTO filter, int pageIndex, int pageSize);
        List<BookItemsIndexItemDTO> GetIndexItemsByBook(Guid bookId, int pageIndex, int pageSize);
        List<BookItemsIndexItemDTO> GetIndexItemsByRack(Guid rackId, int pageIndex, int pageSize);
        BookItemDetailsDTO GetDetailsDTO(Guid bookItemId);
        BookItemCreateDTO GetCreateDTO(Guid bookId);
        BookItemCreateDTO UpdateRacksSelectList(BookItemCreateDTO createDTO);
        BookItemEditDTO GetEditDTO(Guid bookItemId);
        BookItemEditDTO UpdateRacksSelectList(BookItemEditDTO editDTO);
        BookItemDeleteDTO GetDeleteDTO(Guid bookItemId);
        BookItemReserveDTO GetReserveDTO(Guid bookItemId, string currentUserEmail);
        BookItemReserveDTO GetReserveDTO(Guid bookItemId);
        Dictionary<string, string> GetRackNumberList(string searchString);
        Dictionary<string, string> GetLocationIdentifierList(string searchString);
        int GetItemsCount(BookItemsFilterDTO filter);
        int GetItemsCountByBook(Guid bookId);
        int GetItemsCountByRack(Guid rackId);
    }
}
