using Autofac;
using Business.BookItems;
using Business.Books;
using Business.Cards;
using Business.Lending;
using Business.Librarians;
using Business.Members;
using Business.People;
using Business.Racks;
using Data.Context;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Cards;
using Data.Repositories.Librarians;
using Data.Repositories.Members;
using Data.Repositories.People;
using Data.Repositories.Racks;

namespace Web
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LibraryContext>().As<ILibraryContext>();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>();
            builder.RegisterType<LibrarianRepository>().As<ILibrarianRepository>();
            builder.RegisterType<MemberRepository>().As<IMemberRepository>();
            builder.RegisterType<CardRepository>().As<ICardRepository>();
            builder.RegisterType<BookRepository>().As<IBookRepository>();
            builder.RegisterType<BookItemRepository>().As<IBookItemRepository>();
            builder.RegisterType<RackRepository>().As<IRackRepository>();

            builder.RegisterType<BookUpdate>().As<IBookUpdateService>();
            builder.RegisterType<BookQueries>().As<IBookQueriesService>();
            builder.RegisterType<BookItemQueries>().As<IBookItemQueriesService>();
            builder.RegisterType<BookItemUpdate>().As<IBookItemUpdateService>();
            builder.RegisterType<CardQueries>().As<ICardQueriesService>();
            builder.RegisterType<CardUpdate>().As<ICardUpdateService>();
            builder.RegisterType<LendingQueries>().As<ILendingQueriesService>();
            builder.RegisterType<LendingUpdate>().As<ILendingUpdateService>();
            builder.RegisterType<LibrarianQueries>().As<ILibrarianQueriesService>();
            builder.RegisterType<LibrarianUpdate>().As<ILibrarianUpdateService>();
            builder.RegisterType<MemberQueries>().As<IMemberQueriesService>();
            builder.RegisterType<MemberUpdate>().As<IMemberUpdateService>();
            builder.RegisterType<PersonQueries>().As<IPersonQueriesService>();
            builder.RegisterType<PersonUpdate>().As<IPersonUpdateService>();
            builder.RegisterType<RackQueries>().As<IRackQueriesService>();
            builder.RegisterType<RackUpdate>().As<IRackUpdateService>();
            builder.RegisterType<Business.Identity.IdentityService>().As<Business.Identity.IIdentityService>();

            return builder.Build();
        }
    }
}
