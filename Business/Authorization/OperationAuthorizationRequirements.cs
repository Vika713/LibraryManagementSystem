using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Business.Authorization
{
    public static class OperationAuthorizationRequirements
    {
        public static OperationAuthorizationRequirement PersonEdit =
          new OperationAuthorizationRequirement { Name = OperationNames.PersonEditName };
        public static OperationAuthorizationRequirement PersonDetails =
          new OperationAuthorizationRequirement { Name = OperationNames.PersonDetailsName };
        public static OperationAuthorizationRequirement LibrarianDetails =
          new OperationAuthorizationRequirement { Name = OperationNames.LibrarianDetailsName };
        public static OperationAuthorizationRequirement MemberDetails =
          new OperationAuthorizationRequirement { Name = OperationNames.MemberDetailsName };
        public static OperationAuthorizationRequirement CardBlock =
          new OperationAuthorizationRequirement { Name = OperationNames.CardBlockName };
        public static OperationAuthorizationRequirement MemberBookItems =
          new OperationAuthorizationRequirement { Name = OperationNames.MemberBookItemsName };
        public static OperationAuthorizationRequirement CancelReservation =
          new OperationAuthorizationRequirement { Name = OperationNames.CancelReservationName };
    }
}
