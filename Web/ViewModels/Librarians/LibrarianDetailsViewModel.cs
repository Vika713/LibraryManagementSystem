using Common.Enumeration;
using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Librarians
{
    public class LibrarianDetailsViewModel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PersonalCode)]
        public string PersonalCode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LibrarianId)]
        public string Code { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.AccountStatus)]
        public MemberStatus? AccountStatus { get; set; }
    }
}
