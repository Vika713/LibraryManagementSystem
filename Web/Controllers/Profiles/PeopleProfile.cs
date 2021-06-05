using AutoMapper;
using Business.People.DTOs;
using Web.ViewModels.People;

namespace Web.Controllers.Profiles
{
    public class PeopleProfile : Profile
    {
        public PeopleProfile()
        {
            CreateMap<PeopleIndexItemDTO, PersonItemViewModel>();
            CreateMap<PersonDetailsDTO, PersonDetailsViewModel>();
            CreateMap<PersonCreateViewModel, PersonCreateDTO>();
            CreateMap<PersonEditDTO, PersonEditViewModel>();
            CreateMap<PersonEditViewModel, PersonEditDTO>();
        }
    }
}
