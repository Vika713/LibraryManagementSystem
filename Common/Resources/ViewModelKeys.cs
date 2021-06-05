namespace Common.Resources
{
    public static class ViewModelKeys
    {
        //Account
        public const string CurrentStatus = nameof(CurrentStatus);
        public const string AccountStatus = nameof(AccountStatus);

        //BookItem
        public const string BookBarcode = nameof(BookBarcode);
        public const string Barcode = nameof(Barcode);
        public const string PublicationDates = nameof(PublicationDates);
        public const string PublicationDate = nameof(PublicationDate);
        public const string DateOfPurchase = nameof(DateOfPurchase);
        public const string BorrowingDate = nameof(BorrowingDate);
        public const string DueDate = nameof(DueDate);

        //Book
        public const string ISBN = nameof(ISBN);
        public const string Authors = nameof(Authors);
        public const string NumberOfPages = nameof(NumberOfPages);
        public const string Title = nameof(Title);
        public const string Author = nameof(Author);
        public const string AuthorName = nameof(AuthorName);
        public const string Subject = nameof(Subject);

        //Cards
        public const string CardBarcode = nameof(CardBarcode);
        public const string CardNumber = nameof(CardNumber);
        public const string CardIssueDate = nameof(CardIssueDate);

        //Lending
        public const string PaidAmount = nameof(PaidAmount);

        //Librarian
        public const string LibrarianId = nameof(LibrarianId);

        //Member
        public const string MemberId = nameof(MemberId);
        public const string DateOfMembership = nameof(DateOfMembership);
        public const string BorrowedBooks = nameof(BorrowedBooks);
        public const string ReservedBooks = nameof(ReservedBooks);

        //Person
        public const string PersonalCode = nameof(PersonalCode);
        public const string Email = nameof(Email);
        public const string StreetAddress = nameof(StreetAddress);
        public const string ZipCode = nameof(ZipCode);

        //Rack
        public const string RackNumber = nameof(RackNumber);
        public const string LocationIdentifier = nameof(LocationIdentifier);
        public const string BookItemsOnRack = nameof(BookItemsOnRack);
    }
}
