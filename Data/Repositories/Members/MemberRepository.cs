using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Data.Repositories.Members
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public MemberRepository(LibraryContext context) : base(context) { }

        public Member Get(Guid id)
        {
            return Query().Single(m => m.Id == id);
        }

        public Member GetByCardBarcode(string barcode)
        {
            return Query().SingleOrDefault(m => m.Cards.Any(c => c.Barcode == barcode));
        }

        public string GetMemberId(Guid id)
        {
            return Query().Single(m => m.Id == id).Code;
        }

        private IQueryable<Member> Query()
        {
            return LibraryContext.Members
                .Include(m => m.Cards);
        }
    }
}
