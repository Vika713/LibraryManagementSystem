using Common.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.ViewModels.Pagination;

namespace Web.ViewModels.Racks
{
    public class RackEditViewModel : PaginationViewModel
    {
        public Guid RackId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.BookItemsOnRack)]
        public List<RackBookItemsViewModel> BookItemsOnRack { get; set; }
    }
}
