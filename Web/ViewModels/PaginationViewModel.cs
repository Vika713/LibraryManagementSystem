using Common.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Web.ViewModels.Pagination
{
    public class PaginationViewModel
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = Consts.DefaultPageSize;
        public List<SelectListItem> PageSizes { get; set; }

        public PaginationViewModel()
        {
        }

        public void SetPaginationParameters(int itemCount, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(itemCount / (double)pageSize);
            SetPageSizes();
        }

        private void SetPageSizes()
        {
            PageSizes = new List<SelectListItem>()
                {
                    new SelectListItem() { Value="", Text="" },
                    new SelectListItem() { Value=Consts.PageSize1.ToString(), Text= Consts.PageSize1.ToString() },
                    new SelectListItem() { Value=Consts.PageSize2.ToString(), Text= Consts.PageSize2.ToString() },
                    new SelectListItem() { Value=Consts.PageSize3.ToString(), Text= Consts.PageSize3.ToString() }
                };
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
