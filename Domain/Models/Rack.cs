using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Domain.Models
{
    public class Rack
    {
        [Key]
        public Guid Id { get; protected set; }

        [Range(0, Consts.MaxIntNumber)]
        public int RackNumber { get; protected set; }

        [Required]
        [MaxLength(Consts.MaxDbCharCount)]
        public string LocationIdentifier { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public List<BookItem> BookItems { get; protected set; }

        public Rack()
        {
        }

        public Rack(int number, string locationIdentifier)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            RackNumber = number;
            LocationIdentifier = locationIdentifier;
        }

        public void RemoveBookItems(List<BookItem> bookItems)
        {
            BookItems = BookItems.Except(bookItems).ToList();
        }
    }
}
