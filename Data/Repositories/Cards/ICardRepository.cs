using Data.Repositories.Generic;
using Domain.Models;
using System;
using System.Collections.Generic;

namespace Data.Repositories.Cards
{
    public interface ICardRepository : IRepository<Card>
    {
        Card GetActiveCard(Guid id);
        Card GetByNumber(string number);
        Card GetByBarcode(string barcode);
        IEnumerable<Card> GetActiveByMember(Guid memberId);
        bool ExistsByNumber(string number);
        bool ExistsByBarcode(string barcode);
    }
}
