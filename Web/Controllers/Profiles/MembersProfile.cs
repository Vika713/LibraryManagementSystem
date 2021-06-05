using AutoMapper;
using Business.Members.DTOs;
using Web.ViewModels.Members;

namespace Web.Controllers.Profiles
{
    public class MembersProfile : Profile
    {
        public MembersProfile()
        {
            CreateMap<MemberDetailsDTO, MemberDetailsViewModel>();
            CreateMap<MembersBorrowDTO, MembersBorrowViewModel>();
            CreateMap<MembersReserveDTO, MembersReserveViewModel>();
            CreateMap<MemberStatusChangeDTO, MemberStatusChangeViewModel>();
            CreateMap<ReservedBookItemDTO, ReservedBookItemViewModel>();
            CreateMap<BorrowedBookItemDTO, BorrowedBookItemViewModel>();
        }
    }
}
