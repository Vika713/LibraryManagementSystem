using Business.BookItems.DTOs;
using System;

namespace Business.BookItems
{
    public interface IBookItemUpdateService
    {
        void Create(BookItemCreateDTO createDTO);
        void Edit(BookItemEditDTO editDTO);
        void Delete(Guid bookItemId);
        void Reserve(BookItemReserveDTO reserveDTO);
        void CancelReservation(Guid bookItemId);
    }
}
