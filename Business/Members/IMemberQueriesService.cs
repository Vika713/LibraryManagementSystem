using Business.Members.DTOs;
using System;

namespace Business.Members
{
    public interface IMemberQueriesService
    {
        MemberDetailsDTO GetDetailsDTO(Guid memberId);
        MemberStatusChangeDTO GetStatusChangeDTO(Guid memberId);
        MembersBorrowDTO GetBorrowedBookItemsDTO(Guid memberId);
        MembersReserveDTO GetReservedBookItemsDTO(Guid memberId);
        string GetPersonEmail(Guid memberId);
    }
}
