using Business.Cards.DTOs;
using Data.Repositories.Cards;
using Data.Repositories.Members;
using Domain.Models;
using System;

namespace Business.Cards
{
    public class CardQueries : ICardQueriesService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMemberRepository _memberRepository;

        public CardQueries(ICardRepository cardRepository, IMemberRepository memberRepository)
        {
            _cardRepository = cardRepository;
            _memberRepository = memberRepository;
        }

        public CardCreateDTO GetCreateDTO(Guid memberId)
        {
            Member member = _memberRepository.Get(memberId);

            CardCreateDTO createDTO = new CardCreateDTO()
            {
                MemberId = memberId,
                MemberCode = member.Code,
                PersonId = member.PersonId
            };

            return createDTO;
        }

        public CardBlockDTO GetBlockDTO(Guid memberId)
        {
            Card card = _cardRepository.GetActiveCard(memberId);
            Member member = _memberRepository.Get(memberId);

            CardBlockDTO blockDTO = new CardBlockDTO
            {
                Id = card.Id,
                PersonId = member.PersonId,
                MemberId = memberId,
                MemberCode = member.Code,
                Number = card.Number
            };

            return blockDTO;
        }
    }
}
