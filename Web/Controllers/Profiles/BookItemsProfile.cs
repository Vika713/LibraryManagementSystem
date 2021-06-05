using AutoMapper;
using Business.BookItems.DTOs;
using Web.ViewModels.BookItems;

namespace Web.Controllers.Profiles
{
    public class BookItemsProfile : Profile
    {
        public BookItemsProfile()
        {
            CreateMap<BookItemsIndexItemDTO, BookItemItemViewModel>();
            CreateMap<BookItemDetailsDTO, BookItemDetailsViewModel>();
            CreateMap<BookItemCreateViewModel, BookItemCreateDTO>();
            CreateMap<BookItemCreateDTO, BookItemCreateViewModel>();
            CreateMap<BookItemEditDTO, BookItemEditViewModel>();
            CreateMap<BookItemEditViewModel, BookItemEditDTO>();
            CreateMap<BookItemDeleteDTO, BookItemDeleteViewModel>();
            CreateMap<BookItemReserveDTO, BookItemReserveViewModel>();
            CreateMap<BookItemReserveViewModel, BookItemReserveDTO>();
            CreateMap<BookItemReserveDTO, BookItemReservationCancelViewModel>();
        }
    }
}
