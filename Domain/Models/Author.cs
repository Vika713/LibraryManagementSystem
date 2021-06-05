using Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(Consts.MaxDbCharCount)]
        public string Name { get; protected set; }

        public List<BookAuthor> BookAuthors { get; set; }

        public Author()
        {
        }

        public Author(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
