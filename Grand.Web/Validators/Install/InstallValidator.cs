using FluentValidation;
using Grand.Framework.Validators;
using Grand.Services.Installation;
using Grand.Web.Models.Install;
using System.Collections.Generic;

namespace Grand.Web.Validators.Install
{
    public class InstallValidator : BaseGrandValidator<InstallModel>
    {
        public InstallValidator(
            IEnumerable<IValidatorConsumer<InstallModel>> validators,
            IInstallationLocalizationService locService)
            : base(validators)
        {
            RuleFor(x => x.AdminEmail).NotEmpty().When(x => !x.ConnectToExistingDb).WithMessage(locService.GetResource("AdminEmailRequired"));
            RuleFor(x => x.AdminEmail).EmailAddress().When(x => !x.ConnectToExistingDb);
            RuleFor(x => x.AdminPassword).NotEmpty().When(x => !x.ConnectToExistingDb).WithMessage(locService.GetResource("AdminPasswordRequired"));
            RuleFor(x => x.ConfirmPassword).NotEmpty().When(x => !x.ConnectToExistingDb).WithMessage(locService.GetResource("ConfirmPasswordRequired"));
            RuleFor(x => x.AdminPassword).Equal(x => x.ConfirmPassword).When(x => !x.ConnectToExistingDb).WithMessage(locService.GetResource("PasswordsDoNotMatch"));            
        }
    }
}