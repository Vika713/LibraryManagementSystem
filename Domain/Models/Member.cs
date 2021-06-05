using Common.Constants;
using Common.Enumeration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Member
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        [StringLength(Consts.MemberCodeLength)]
        public string Code { get; protected set; }

        [Required]
        public DateTime DateOfMembership { get; protected set; }

        public MemberStatus Status { get; protected set; }

        [Required]
        public Guid PersonId { get; protected set; }

        public List<BookItem> BorrowedBookItems { get; protected set; }

        public List<BookItem> ReservedBookItems { get; protected set; }

        public List<Card> Cards { get; protected set; }

        public Person Person { get; protected set; }

        public Member()
        {
        }

        public Member(string code, DateTime dateOfMembership, Guid personId)
        {
            Id = Guid.NewGuid();
            Code = code;
            DateOfMembership = dateOfMembership;
            PersonId = personId;
            Status = MemberStatus.Active;
        }

        public void ChangeStatus(MemberStatus status)
        {
            Status = status;
        }
    }
}
