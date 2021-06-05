using Business.Racks.DTOs;
using Data.Repositories.BookItems;
using Data.Repositories.Racks;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Racks
{
    public class RackUpdate : IRackUpdateService
    {
        private readonly IRackRepository _rackRepository;
        private readonly IBookItemRepository _bookItemRepository;

        public RackUpdate(IRackRepository rackRepository, IBookItemRepository bookItemRepository)
        {
            _rackRepository = rackRepository;
            _bookItemRepository = bookItemRepository;
        }

        public void Create(RackCreateDTO createDTO)
        {
            Rack rack = new Rack(createDTO.RackNumber, createDTO.LocationIdentifier);

            _rackRepository.Add(rack);
            _rackRepository.SaveChanges();
        }

        public void Edit(RackEditDTO editDTO)
        {
            Rack rack = _rackRepository.Get(editDTO.RackId);

            List<BookItem> bookItemsToRemove = new List<BookItem>();

            if (editDTO.BookItemsOnRack != null && editDTO.BookItemsOnRack.Any())
            {
                for (int i = 0; i < editDTO.BookItemsOnRack.Count(); i++)
                {
                    if (editDTO.BookItemsOnRack[i].Selected == false)
                        bookItemsToRemove.Add(_bookItemRepository.Get(editDTO.BookItemsOnRack[i].BookItemId));
                }

                rack.RemoveBookItems(bookItemsToRemove);
            }

            _rackRepository.Update(rack);
            _rackRepository.SaveChanges();
        }

        public void Delete(Guid rackId)
        {
            Rack rack = _rackRepository.Get(rackId);
            IEnumerable<BookItem> bookItems = _bookItemRepository.GetByRackId(rack.Id);

            if (bookItems == null || !bookItems.Any())
            {
                _rackRepository.Remove(rack);
                _rackRepository.SaveChanges();
            }
        }
    }
}
