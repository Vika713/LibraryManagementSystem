using Data.Context;
using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories.Cards
{
    public class CardRepository : Repository<Card>, ICardRepository
    {
        public LibraryContext LibraryContext
        {
            get { return DatabaseContext as LibraryContext; }
        }

        public CardRepository(LibraryContext context) : base(context) { }

        public Card GetActiveCard(Guid memberId)
        {
            return Query()
                .SingleOrDefault(c => c.MemberId == memberId && c.IsActive == true);
        }

        public Card GetByNumber(string number)
        {
            return Query().SingleOrDefault(c => c.Number == number);
        }

        public Card GetByBarcode(string barcode)
        {
            return Query().SingleOrDefault(c => c.Barcode == barcode);
        }

        public IEnumerable<Card> GetActiveByMember(Guid memberId)
        {
            return Query().Where(c => c.IsActive && c.MemberId == memberId);
        }

        public bool ExistsByNumber(string number)
        {
            return GetByNumber(number) != null;
        }

        public bool ExistsByBarcode(string barcode)
        {
            return GetByBarcode(barcode) != null;
        }

        private IQueryable<Card> Query()
        {
            return LibraryContext.Cards;
        }
    }
}
