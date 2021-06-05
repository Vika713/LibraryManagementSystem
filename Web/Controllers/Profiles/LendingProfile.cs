using AutoMapper;
using Business.Lending.DTOs;
using Web.ViewModels.Lending;

namespace Web.Controllers.Profiles
{
    public class LendingProfile : Profile
    {
        public LendingProfile()
        {
            CreateMap<LendingFineDTO, LendingFineViewModel>();
            CreateMap<LendingFineViewModel, LendingFineDTO>();
            CreateMap<CheckOutViewModel, ScanDTO>();
            CreateMap<RenewViewModel, ScanDTO>();
        }
    }
}
