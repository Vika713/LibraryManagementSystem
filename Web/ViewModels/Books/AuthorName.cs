using Common.Constants;
using Common.Resources;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Books
{
    public class AuthorName
    {
        [Display(ResourceType = typeof(ViewModelsResource), Name = ViewModelKeys.AuthorName)]
        public string Name { get; set; }
    }

    public class AuthorNameValidator : AbstractValidator<AuthorName>
    {
        public AuthorNameValidator()
        {
            RuleFor(a => a.Name).MaximumLength(Consts.MaxDbCharCount);
        }
    }
}
