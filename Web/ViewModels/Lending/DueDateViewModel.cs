using System;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Lending
{
    public class DueDateViewModel
    {
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public DueDateViewModel(DateTime date)
        {
            DueDate = date;
        }
    }
}
