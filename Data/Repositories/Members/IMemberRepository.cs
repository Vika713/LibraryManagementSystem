using Data.Repositories.Generic;
using Domain.Models;
using System;

namespace Data.Repositories.Members
{
    public interface IMemberRepository : IRepository<Member>
    {
        Member Get(Guid id);
        Member GetByCardBarcode(string barcode);
        string GetMemberId(Guid id);
    }
}
