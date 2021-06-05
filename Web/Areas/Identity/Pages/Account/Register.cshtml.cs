using Business.Identity;
using Common.Enumeration;
using Common.Resources;
using Data.Repositories.People;
using Domain.Areas.Identity;
using Domain.Models;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public partial class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IIdentityService _identityService;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IIdentityService identityService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _identityService = identityService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public class InputModelValidator : AbstractValidator<InputModel>
        {
            private readonly IPersonRepository _personRepository;
            private readonly IStringLocalizer<ValidationMessagesResource> _localizer;

            public InputModelValidator(IPersonRepository personRepository, IStringLocalizer<ValidationMessagesResource> stringLocalizer)
            {
                _personRepository = personRepository;
                _localizer = stringLocalizer;

                RuleFor(p => p.Email).Cascade(CascadeMode.Stop)
                    .Must(RegisteredEmail).WithMessage(_localizer[LocalizationKeys.EmailNotRegistered])
                    .Must(RegisteredAsMemberOrLibrarian).WithMessage(_localizer[LocalizationKeys.NotRegisteredAsMemberOrLibrarian])
                    .Must(IsActive).WithMessage(_localizer[LocalizationKeys.AccountClosed]);
            }

            private bool RegisteredEmail(InputModel model, string email)
            {
                return _personRepository.ExistsByEmail(email);
            }

            public bool RegisteredAsMemberOrLibrarian(InputModel model, string email)
            {
                Person person = _personRepository.GetByEmail(email);

                return person.Member != null || person.Librarian != null;
            }

            public bool IsActive(InputModel model, string email)
            {
                Person person = _personRepository.GetByEmail(email);

                return (person.Member?.Status != MemberStatus.Closed ||
                        person.Librarian?.Status != LibrarianStatus.Closed);
            }
        }

        public async virtual Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async virtual Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, IsEnabled = true };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    foreach (string role in _identityService.GetRoles(user.Email))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
