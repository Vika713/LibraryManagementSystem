using AutoMapper;
using Business.Librarians.DTOs;
using Web.ViewModels.Librarians;

namespace Web.Controllers.Profiles
{
    public class LibrariansProfile : Profile
    {
        public LibrariansProfile()
        {
            CreateMap<LibrarianDetailsDTO, LibrarianDetailsViewModel>();
            CreateMap<LibrarianStatusChangeDTO, LibrarianStatusChangeViewModel>();
        }
    }
}
