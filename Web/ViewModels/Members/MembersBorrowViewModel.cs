using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Members
{
    public class MembersBorrowViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string Code { get; set; }

        public List<BorrowedBookItemViewModel> BorrowedBookItems { get; set; }
    }
}
