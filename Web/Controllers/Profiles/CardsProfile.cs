using AutoMapper;
using Business.Cards.DTOs;
using Web.ViewModels.Cards;

namespace Web.Controllers.Profiles
{
    public class CardsProfile : Profile
    {
        public CardsProfile()
        {
            CreateMap<CardBlockDTO, CardBlockViewModel>();
            CreateMap<CardCreateViewModel, CardCreateDTO>();
            CreateMap<CardCreateDTO, CardCreateViewModel>();
        }
    }
}
