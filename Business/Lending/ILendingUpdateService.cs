using Business.Lending.DTOs;

namespace Business.Lending
{
    public interface ILendingUpdateService
    {
        void Lend(ScanDTO scanDTO);
        void Return(string bookBarcode);
        void Renew(string bookBarcode);
    }
}
