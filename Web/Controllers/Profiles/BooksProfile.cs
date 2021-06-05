using AutoMapper;
using Business.Books.DTOs;
using Web.ViewModels.Books;

namespace Web.Controllers.Profiles
{
    public class BooksProfile : Profile
    {
        public BooksProfile()
        {
            CreateMap<BooksIndexItemDTO, BookItemViewModel>();
            CreateMap<BooksIndexItemDTO.DateTimeItem, BookItemViewModel.DateTimeItem>();
            CreateMap<BookCreateViewModel, BookCreateDTO>();
            CreateMap<BookCreateDTO, BookCreateViewModel>();
            CreateMap<BookEditDTO, BookEditViewModel>();
            CreateMap<AuthorNameDTO, AuthorName>();
            CreateMap<AuthorName, AuthorNameDTO>();
            CreateMap<BookEditViewModel, BookEditDTO>();
            CreateMap<BookDeleteDTO, BookDeleteViewModel>();
        }

    }
}
