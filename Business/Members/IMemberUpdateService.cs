using Common.Enumeration;
using System;

namespace Business.Members
{
    public interface IMemberUpdateService
    {
        void ChangeStatus(Guid memberId, MemberStatus newStatus);
    }
}
