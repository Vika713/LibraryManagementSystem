using AutoMapper;
using Business.Racks.DTOs;
using Web.ViewModels.Racks;

namespace Web.Controllers.Profiles
{
    public class RacksProfile : Profile
    {
        public RacksProfile()
        {
            CreateMap<RacksIndexItemDTO, RackItemViewModel>();
            CreateMap<RackCreateViewModel, RackCreateDTO>();
            CreateMap<RackEditDTO, RackEditViewModel>();
            CreateMap<RackEditViewModel, RackEditDTO>();
            CreateMap<RackBookItemsDTO, RackBookItemsViewModel>();
            CreateMap<RackBookItemsViewModel, RackBookItemsDTO>();
            CreateMap<RackDeleteDTO, RackDeleteViewModel>();
        }
    }
}
