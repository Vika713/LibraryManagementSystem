using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Racks
{
    public class RackDeleteViewModel
    {
        public Guid Id { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.RackNumber)]
        public int RackNumber { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.LocationIdentifier)]
        public string LocationIdentifier { get; set; }
    }
}
