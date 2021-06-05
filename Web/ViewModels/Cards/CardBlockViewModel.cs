using Common.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Cards
{
    public class CardBlockViewModel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public Guid MemberId { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.MemberId)]
        public string MemberCode { get; set; }

        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.CardNumber)]
        public string Number { get; set; }
    }
}
