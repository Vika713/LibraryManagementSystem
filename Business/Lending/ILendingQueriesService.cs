using Business.Lending.DTOs;
using System;

namespace Business.Lending
{
    public interface ILendingQueriesService
    {
        LendingFineDTO GetFineDTO(string bookBarcode);
        DateTime GetDueDate(string bookBarcode);
        bool IsOverdue(string bookBarcode);
        bool BookIsReserved(string bookBarcode);
    }
}
