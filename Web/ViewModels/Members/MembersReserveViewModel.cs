using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Members
{
    public class MembersReserveViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string Code { get; set; }

        public List<ReservedBookItemViewModel> ReservedBookItems { get; set; }
    }
}
