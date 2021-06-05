using AutoMapper;
using Business.Identity.DTOs;
using Web.ViewModels.Identity;

namespace Web.Controllers.Profiles
{
    public class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<ProfileNavigationDTO, ProfileNavigationViewModel>();
        }
    }
}
