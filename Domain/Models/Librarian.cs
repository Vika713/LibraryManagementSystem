using Common.Constants;
using Common.Enumeration;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Librarian
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        [StringLength(Consts.LibrarianCodeLength)]
        public string Code { get; protected set; }

        [Required]
        public Guid PersonId { get; protected set; }

        public LibrarianStatus Status { get; protected set; }

        public Person Person { get; protected set; }

        public Librarian()
        {
        }

        public Librarian(string code, Guid personId)
        {
            Id = Guid.NewGuid();
            Code = code;
            PersonId = personId;
            Status = LibrarianStatus.Active;
        }

        public void ChangeStatus(LibrarianStatus status)
        {
            Status = status;
        }
    }
}
