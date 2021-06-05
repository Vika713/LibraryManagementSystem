using Business.Members.DTOs;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Members;
using Data.Repositories.People;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Members
{
    public class MemberQueries : IMemberQueriesService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IBookItemRepository _bookItemRepository;
        private readonly IBookRepository _bookRepository;

        public MemberQueries(
            IMemberRepository repository,
            IPersonRepository personRepository,
            IBookItemRepository bookItemRepository,
            IBookRepository bookRepository)
        {
            _memberRepository = repository;
            _personRepository = personRepository;
            _bookItemRepository = bookItemRepository;
            _bookRepository = bookRepository;
        }

        public MemberDetailsDTO GetDetailsDTO(Guid memberId)
        {
            Member member = _memberRepository.Get(memberId);

            MemberDetailsDTO detailsDTO = new MemberDetailsDTO()
            {
                Id = member.Id,
                PersonId = member.PersonId,
                Code = member.Code,
                AccountStatus = member.Status,
                DateOfMembership = member.DateOfMembership,
                CardNumber = member.Cards?
                        .Where(c => c.IsActive == true).Select(c => c.Number).FirstOrDefault(),
                CardBarcode = member.Cards?
                        .Where(c => c.IsActive == true).Select(c => c.Barcode).FirstOrDefault(),
                CardIssueDate = member.Cards?
                        .Where(c => c.IsActive == true).Select(c => c.IssuedAt).FirstOrDefault()
            };

            detailsDTO.BorrowedBooks = _bookItemRepository.GetByBorrowedMemberId(detailsDTO.Id).Count();
            detailsDTO.ReservedBooks = _bookItemRepository.GetByReservedMemberId(detailsDTO.Id).Count();

            return detailsDTO;
        }

        public MemberStatusChangeDTO GetStatusChangeDTO(Guid memberId)
        {
            Member member = _memberRepository.Get(memberId);

            MemberStatusChangeDTO statusChangeDTO = new MemberStatusChangeDTO()
            {
                Id = member.Id,
                Code = member.Code,
                CurrentStatus = member.Status
            };

            return statusChangeDTO;
        }

        public MembersBorrowDTO GetBorrowedBookItemsDTO(Guid memberId)
        {
            MembersBorrowDTO borrowDTO = new MembersBorrowDTO
            {
                BorrowedBookItems = _bookItemRepository.GetByBorrowedMemberId(memberId)
                .Select(db => new BorrowedBookItemDTO()
                {
                    Barcode = db.Barcode,
                    BookId = db.BookId,
                    Format = db.Format,
                    BorrowingDate = db.BorrowingDate,
                    DueDate = db.DueDate
                })
                .ToList(),
                Id = memberId,
                Code = _memberRepository.GetMemberId(memberId)
            };

            IEnumerable<Book> books = _bookRepository.Get(borrowDTO.BorrowedBookItems.Select(bi => bi.BookId));

            foreach (BorrowedBookItemDTO bookItem in borrowDTO.BorrowedBookItems)
            {
                Book book = books.FirstOrDefault(b => b.Id == bookItem.BookId);

                bookItem.ISBN = book.ISBN;
                bookItem.Title = book.Title;
                bookItem.AuthorsNames = book.BookAuthors?.Select(ba => ba.Author?.Name).ToList();
            }

            return borrowDTO;
        }

        public MembersReserveDTO GetReservedBookItemsDTO(Guid memberId)
        {
            MembersReserveDTO reserveDTO = new MembersReserveDTO
            {
                ReservedBookItems = _bookItemRepository.GetByReservedMemberId(memberId)
                .Select(db => new ReservedBookItemDTO()
                {
                    Barcode = db.Barcode,
                    BookId = db.BookId,
                    BookItemId = db.Id,
                    Format = db.Format
                })
                .ToList(),
                Id = memberId,
                Code = _memberRepository.GetMemberId(memberId)
            };

            IEnumerable<Book> books = _bookRepository.Get(reserveDTO.ReservedBookItems.Select(r => r.BookId));

            foreach (ReservedBookItemDTO bookItem in reserveDTO.ReservedBookItems)
            {
                Book book = books.FirstOrDefault(b => b.Id == bookItem.BookId);

                bookItem.ISBN = book.ISBN;
                bookItem.Title = book.Title;
                bookItem.AuthorsNames = book.BookAuthors?.Select(ba => ba.Author.Name).ToList();
            }

            return reserveDTO;
        }

        public string GetPersonEmail(Guid memberId)
        {
            return _personRepository.GetByMemberId(memberId).Email;
        }
    }
}
