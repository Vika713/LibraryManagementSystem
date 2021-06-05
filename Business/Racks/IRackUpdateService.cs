using Business.Racks.DTOs;
using System;

namespace Business.Racks
{
    public interface IRackUpdateService
    {
        void Create(RackCreateDTO createDTO);
        void Edit(RackEditDTO editDTO);
        void Delete(Guid rackId);
    }
}
