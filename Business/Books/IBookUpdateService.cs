using Business.Books.DTOs;
using System;

namespace Business.Books
{
    public interface IBookUpdateService
    {
        Guid Create(BookCreateDTO createDTO);
        void Edit(BookEditDTO editDTO);
        void Delete(Guid bookId);
    }
}
