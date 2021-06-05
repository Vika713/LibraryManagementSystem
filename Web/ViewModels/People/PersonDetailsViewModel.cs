using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.People
{
    public class PersonDetailsViewModel
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.PersonalCode)]
        public string PersonalCode { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.StreetAddress)]
        public string StreetAddress { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.ZipCode)]
        public string ZipCode { get; set; }

        public string Country { get; set; }

        public Guid? MemberId { get; set; }

        public Guid? LibrarianId { get; set; }

        public string Email { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string MemberCode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardNumber)]
        public string CardNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LibrarianId)]
        public string LibrarianCode { get; set; }
    }
}
