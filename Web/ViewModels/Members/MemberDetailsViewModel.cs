using Common.Enumeration;
using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Members
{
    public class MemberDetailsViewModel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string Code { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.AccountStatus)]
        public MemberStatus? AccountStatus { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.DateOfMembership)]
        [DataType(DataType.Date)]
        public DateTime DateOfMembership { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardNumber)]
        public string CardNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardBarcode)]
        public string CardBarcode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardIssueDate)]
        [DataType(DataType.Date)]
        public DateTime? CardIssueDate { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BorrowedBooks)]
        public int BorrowedBooks { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.ReservedBooks)]
        public int ReservedBooks { get; set; }
    }
}
