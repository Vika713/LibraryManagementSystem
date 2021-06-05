using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Language = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NumberOfPages = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonalCode = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RackNumber = table.Column<int>(type: "int", nullable: false),
                    LocationIdentifier = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthor", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookAuthor_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthor_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    State = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Librarians",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Librarians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Librarians_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    DateOfMembership = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Members_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: true),
                    Format = table.Column<int>(type: "int", nullable: true),
                    DateOfPurchase = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BorrowingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BorrowedMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ReservedMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookItems_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookItems_Members_BorrowedMemberId",
                        column: x => x.BorrowedMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookItems_Members_ReservedMemberId",
                        column: x => x.ReservedMemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookItems_Racks_RackId",
                        column: x => x.RackId,
                        principalTable: "Racks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Barcode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("d27a2d1a-ed26-48e5-9d8d-9be9067b310a"), "Harari, Yuval Noah" },
                    { new Guid("23933d81-8a3a-4fc6-a121-4a078ba86bfa"), "King, Stephen" },
                    { new Guid("33a9812c-c9b2-44c2-9443-f07e20b74d80"), "Straub, Peter" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CreatedAt", "ISBN", "Language", "NumberOfPages", "Publisher", "Subject", "Title" },
                values: new object[,]
                {
                    { new Guid("da919b37-703d-47d8-b65d-368d9fa1429d"), new DateTime(2021, 6, 5, 17, 51, 28, 638, DateTimeKind.Local).AddTicks(8772), "0062316095", "English", 464, "Harper", "History", "Sapiens: A Brief History of Humankind" },
                    { new Guid("68ffc480-4606-4607-8b46-8366229a8789"), new DateTime(2021, 6, 5, 17, 51, 28, 638, DateTimeKind.Local).AddTicks(9918), "9781451697216", "English", 921, "Pocket Books", "Dark Fantasy", "The Talisman: A Novel" }
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "PersonalCode", "Phone" },
                values: new object[,]
                {
                    { new Guid("e08af54c-c88f-4943-a106-bae2071a60be"), new DateTime(2021, 6, 5, 17, 51, 28, 632, DateTimeKind.Local).AddTicks(8999), "librarian@lms.com", "Sarah Smith", "41234567890", "860000000" },
                    { new Guid("3343c381-65ea-4392-a2ac-5eaef92d99e9"), new DateTime(2021, 6, 5, 17, 51, 28, 635, DateTimeKind.Local).AddTicks(7093), "member@lms.com", "John Adams", "31234567890", "861111111" },
                    { new Guid("93ff99ed-d074-4a44-9ee0-fdc9be300cf5"), new DateTime(2021, 6, 5, 17, 51, 28, 635, DateTimeKind.Local).AddTicks(7123), "admin@lms.com", "Admin", "00000000000", "000000000" }
                });

            migrationBuilder.InsertData(
                table: "Racks",
                columns: new[] { "Id", "CreatedAt", "LocationIdentifier", "RackNumber" },
                values: new object[] { new Guid("435d6acb-182d-46c6-89df-ff250bdc1d9e"), new DateTime(2021, 6, 5, 17, 51, 28, 638, DateTimeKind.Local).AddTicks(2976), "A", 0 });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "PersonId", "State", "StreetAddress", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("ce64e16b-45bf-46a9-a537-5f6bb48f0146"), "Vilnius", "Lithuania", new Guid("e08af54c-c88f-4943-a106-bae2071a60be"), null, "Gabijos", "LT-00000" },
                    { new Guid("2a60f9c5-3d87-4cfe-a713-019de1fdc9cf"), "Vilnius", "Lithuania", new Guid("3343c381-65ea-4392-a2ac-5eaef92d99e9"), null, "Vilniaus", "LT-00000" },
                    { new Guid("798bf79c-0d1d-4334-a64c-3222966601e9"), "Vilnius", "Lithuania", new Guid("93ff99ed-d074-4a44-9ee0-fdc9be300cf5"), null, "Medeinos", "LT-00000" }
                });

            migrationBuilder.InsertData(
                table: "BookAuthor",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { new Guid("d27a2d1a-ed26-48e5-9d8d-9be9067b310a"), new Guid("da919b37-703d-47d8-b65d-368d9fa1429d") },
                    { new Guid("23933d81-8a3a-4fc6-a121-4a078ba86bfa"), new Guid("68ffc480-4606-4607-8b46-8366229a8789") },
                    { new Guid("33a9812c-c9b2-44c2-9443-f07e20b74d80"), new Guid("68ffc480-4606-4607-8b46-8366229a8789") }
                });

            migrationBuilder.InsertData(
                table: "BookItems",
                columns: new[] { "Id", "Barcode", "BookId", "BorrowedMemberId", "BorrowingDate", "CreatedAt", "DateOfPurchase", "DueDate", "Format", "Price", "PublicationDate", "RackId", "ReservedMemberId", "Status" },
                values: new object[,]
                {
                    { new Guid("c4a7cb00-1fb6-4f46-9546-f69a5f45b824"), "001", new Guid("da919b37-703d-47d8-b65d-368d9fa1429d"), null, null, new DateTime(2021, 6, 5, 17, 51, 28, 639, DateTimeKind.Local).AddTicks(9048), new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 25.699999999999999, new DateTime(2015, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("435d6acb-182d-46c6-89df-ff250bdc1d9e"), null, 0 },
                    { new Guid("53ddda4c-b976-46e8-8b86-902187e741e5"), "002", new Guid("68ffc480-4606-4607-8b46-8366229a8789"), null, null, new DateTime(2021, 6, 5, 17, 51, 28, 640, DateTimeKind.Local).AddTicks(3505), new DateTime(2021, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 44.869999999999997, new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("435d6acb-182d-46c6-89df-ff250bdc1d9e"), null, 0 }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "Code", "PersonId", "Status" },
                values: new object[] { new Guid("6f95fdfd-2529-4e3e-8a9d-d6aec39ae109"), "L210000", new Guid("e08af54c-c88f-4943-a106-bae2071a60be"), 0 });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Code", "DateOfMembership", "PersonId", "Status" },
                values: new object[] { new Guid("3c9ca831-0634-424a-8ad6-81c2b991903c"), "M210000", new DateTime(2021, 6, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3343c381-65ea-4392-a2ac-5eaef92d99e9"), 0 });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Barcode", "IsActive", "IssuedAt", "MemberId", "Number" },
                values: new object[] { new Guid("f9d96da3-816a-45cf-bb76-c6cca1b484a2"), "123456", true, new DateTime(2021, 6, 5, 0, 0, 0, 0, DateTimeKind.Local), new Guid("3c9ca831-0634-424a-8ad6-81c2b991903c"), "12345" });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonId",
                table: "Addresses",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_Name",
                table: "Authors",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_BookId",
                table: "BookAuthor",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_Barcode",
                table: "BookItems",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_BookId",
                table: "BookItems",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_BorrowedMemberId",
                table: "BookItems",
                column: "BorrowedMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_RackId",
                table: "BookItems",
                column: "RackId");

            migrationBuilder.CreateIndex(
                name: "IX_BookItems_ReservedMemberId",
                table: "BookItems",
                column: "ReservedMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Barcode",
                table: "Cards",
                column: "Barcode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_MemberId",
                table: "Cards",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_Number",
                table: "Cards",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Librarians_Code",
                table: "Librarians",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Librarians_PersonId",
                table: "Librarians",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_Code",
                table: "Members",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Members_PersonId",
                table: "Members",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_Email",
                table: "People",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_People_PersonalCode",
                table: "People",
                column: "PersonalCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Racks_RackNumber_LocationIdentifier",
                table: "Racks",
                columns: new[] { "RackNumber", "LocationIdentifier" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "BookItems");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Librarians");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Racks");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
