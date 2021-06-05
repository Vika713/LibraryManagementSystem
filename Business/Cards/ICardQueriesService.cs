using Business.Cards.DTOs;
using System;

namespace Business.Cards
{
    public interface ICardQueriesService
    {
        CardCreateDTO GetCreateDTO(Guid memberId);
        CardBlockDTO GetBlockDTO(Guid memberId);
    }
}
