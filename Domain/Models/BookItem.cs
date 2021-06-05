using Common.Constants;
using Common.Enumeration;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class BookItem
    {
        [Key]
        public Guid Id { get; protected set; }

        [Required]
        [MaxLength(Consts.MaxBarcodeLength)]
        public string Barcode { get; protected set; }

        [Required]
        public Guid BookId { get; protected set; }

        [DataType(DataType.Date)]
        public DateTime? PublicationDate { get; protected set; }

        [Range(0.0, Consts.MaxDoubleNumber)]
        public double? Price { get; protected set; }

        public BookFormat? Format { get; protected set; }

        public DateTime? DateOfPurchase { get; protected set; }

        public BookStatus Status { get; protected set; }

        public DateTime? BorrowingDate { get; protected set; }

        public DateTime? DueDate { get; protected set; }

        public DateTime CreatedAt { get; protected set; }

        public Guid? BorrowedMemberId { get; protected set; }

        public Guid? ReservedMemberId { get; protected set; }

        public Guid? RackId { get; protected set; }

        public Rack Rack { get; protected set; }

        public Book Book { get; protected set; }

        public Member BorrowedMember { get; protected set; }

        public Member ReservedMember { get; protected set; }

        public BookItem()
        {
        }

        public BookItem(Guid bookId, string barcode, DateTime? publicationDate, double? price, BookFormat format, DateTime? dateOfPurchase, Guid? rackId)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
            BookId = bookId;
            Barcode = barcode;
            PublicationDate = publicationDate;
            Price = price;
            Format = format;
            DateOfPurchase = dateOfPurchase;
            Status = BookStatus.Available;
            RackId = rackId;
        }

        public void Edit(string barcode, DateTime? publicationDate, double? price, BookFormat? format, DateTime? dateOfPurchase)
        {
            Barcode = barcode;
            PublicationDate = publicationDate;
            Price = price;
            Format = format;
            DateOfPurchase = dateOfPurchase;
        }

        public void Lend(Guid memberId)
        {
            Status = BookStatus.Loaned;
            BorrowedMemberId = memberId;
            BorrowingDate = DateTime.Today.Date;
            DueDate = DateTime.Today.Date.AddDays(Consts.MaxLendingDays);
            ReservedMemberId = null;
        }

        public void Return()
        {
            BorrowedMemberId = null;
            DueDate = null;
            BorrowingDate = null;

            Status = ReservedMemberId != null ? BookStatus.Reserved : BookStatus.Available;
        }

        public void Reserve(Guid memberId)
        {
            ReservedMemberId = memberId;

            if (Status == BookStatus.Available)
            {
                Status = BookStatus.Reserved;
            }
        }

        public void RemoveReservation()
        {
            ReservedMemberId = null;

            if (Status == BookStatus.Reserved)
            {
                Status = BookStatus.Available;
            }
        }

        public void RemoveMembers()
        {
            BorrowedMemberId = null;
            ReservedMemberId = null;
            BorrowingDate = null;
            DueDate = null;
        }

        public void SetStatus(BookStatus status)
        {
            Status = status;
        }

        public void SetRack(Guid? rackId)
        {
            RackId = rackId;
        }

        public void SetDueDate(DateTime date)
        {
            DueDate = date;
        }
    }
}
