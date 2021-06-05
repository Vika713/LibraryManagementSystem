using Business.Cards.DTOs;
using Data.Repositories.Cards;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Cards
{
    public class CardUpdate : ICardUpdateService
    {
        private readonly ICardRepository _cardRepository;

        public CardUpdate(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public void Create(CardCreateDTO createDTO)
        {
            DateTime issuedAt = DateTime.Today;
            bool isActive = true;

            Card card = new Card(createDTO.MemberId, createDTO.Number, createDTO.Barcode, issuedAt, isActive);

            _cardRepository.Add(card);
            _cardRepository.SaveChanges();
        }

        public void MakeAllCardsInactive(Guid memberId)
        {
            IEnumerable<Card> cards = _cardRepository.GetActiveByMember(memberId);

            if (cards != null)
            {
                foreach (Card card in cards)
                {
                    card.MakeInactive();
                }

                _cardRepository.UpdateRange(cards.ToList());
                _cardRepository.SaveChanges();
            }
        }
    }
}
