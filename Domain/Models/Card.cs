using Common.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        [StringLength(Consts.CardNumberLength)]
        public string Number { get; protected set; }

        [Required]
        [MaxLength(Consts.MaxBarcodeLength)]
        public string Barcode { get; protected set; }

        [Required]
        public DateTime IssuedAt { get; protected set; }

        [Required]
        public bool IsActive { get; protected set; }

        [Required]
        public Guid MemberId { get; protected set; }

        public Member Member { get; protected set; }

        public Card()
        {
        }

        public Card(Guid memberId, string number, string barcode, DateTime issuedAt, bool isActive)
        {
            Id = Guid.NewGuid();
            MemberId = memberId;
            Number = number;
            Barcode = barcode;
            IssuedAt = issuedAt;
            IsActive = isActive;
        }

        public void MakeInactive()
        {
            IsActive = false;
        }
    }
}
