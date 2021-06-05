using Business.Authorization;
using Business.BookItems;
using Business.Books;
using Business.Cards;
using Business.Identity;
using Business.Lending;
using Business.Librarians;
using Business.Members;
using Business.People;
using Business.Racks;
using Data.Areas.Identity;
using Data.Context;
using Data.Repositories.BookItems;
using Data.Repositories.Books;
using Data.Repositories.Cards;
using Data.Repositories.Librarians;
using Data.Repositories.Members;
using Data.Repositories.People;
using Data.Repositories.Racks;
using Domain.Areas.Identity;
using Domain.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using Web.Areas.Identity;
using Web.Areas.Identity.Pages.Account;
using Web.Areas.Identity.Pages.Account.Manage;
using Web.ViewModels.BookItems;
using Web.ViewModels.Books;
using Web.ViewModels.Cards;
using Web.ViewModels.Lending;
using Web.ViewModels.Librarians;
using Web.ViewModels.Members;
using Web.ViewModels.People;
using Web.ViewModels.Racks;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("LibraryDatabase"), b => b.MigrationsAssembly("Data")));

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<ApplicationSignInManager>();

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddScoped<IAuthorizationHandler, PersonAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, LibrarianDetailsAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, MemberAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, CardBlockAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, BookItemAuthorizationHandler>();

            services.AddScoped<ICustomAuthorizationService, CustomAuthorizationService>();

            services.AddDbContext<LibraryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LibraryDatabase"), b => b.MigrationsAssembly("Data")));

            services.AddScoped<ILibraryContext, LibraryContext>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IBookItemRepository, BookItemRepository>();
            services.AddScoped<ILibrarianRepository, LibrarianRepository>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IRackRepository, RackRepository>();
            services.AddScoped<IBookUpdateService, BookUpdate>();
            services.AddScoped<IBookQueriesService, BookQueries>();
            services.AddScoped<IBookItemQueriesService, BookItemQueries>();
            services.AddScoped<IBookItemUpdateService, BookItemUpdate>();
            services.AddScoped<ICardQueriesService, CardQueries>();
            services.AddScoped<ICardUpdateService, CardUpdate>();
            services.AddScoped<ILendingQueriesService, LendingQueries>();
            services.AddScoped<ILendingUpdateService, LendingUpdate>();
            services.AddScoped<ILibrarianQueriesService, LibrarianQueries>();
            services.AddScoped<ILibrarianUpdateService, LibrarianUpdate>();
            services.AddScoped<IMemberQueriesService, MemberQueries>();
            services.AddScoped<IMemberUpdateService, MemberUpdate>();
            services.AddScoped<IPersonQueriesService, PersonQueries>();
            services.AddScoped<IPersonUpdateService, PersonUpdate>();
            services.AddScoped<IRackQueriesService, RackQueries>();
            services.AddScoped<IRackUpdateService, RackUpdate>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddMvc()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonEditViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PeopleIndexViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RackCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RacksIndexViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CardCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ISBNCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookEditViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookDeleteViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AuthorNameValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BooksIndexViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookItemsIndexViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookItemCreateViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookItemDeleteViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookItemEditViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BookItemReserveViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LendingFineViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CheckOutViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ReturnViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RenewViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LibrarianStatusChangeViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MemberStatusChangeViewModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterModel.InputModelValidator>())
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EmailModel.InputModelValidator>());

            services.AddLocalization();
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                new CultureInfo("en-US")
            };
                options.DefaultRequestCulture = new RequestCulture("en-US", "en-US");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Books}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
