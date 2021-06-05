using Business.Cards.DTOs;
using System;

namespace Business.Cards
{
    public interface ICardUpdateService
    {
        void Create(CardCreateDTO createDTO);
        void MakeAllCardsInactive(Guid memberId);
    }
}
