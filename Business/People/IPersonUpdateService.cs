using Business.People.DTOs;
using System;

namespace Business.People
{
    public interface IPersonUpdateService
    {
        Guid Create(PersonCreateDTO createDTO);
        void Edit(PersonEditDTO editDTO);
        void AddAsMember(Guid personId);
        void AddAsLibrarian(Guid personId);
    }
}
