using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using eBooks.Domain.Entities.Identity;
using eBooks.ViewModel;

namespace eBooks.ServiceLayer.Services.Identity;

public class CustomUserValidator : UserValidator<User>
{
    private readonly ISet<string> _emailsBanList;

    public CustomUserValidator(
        IdentityErrorDescriber errors
    ) : base(errors) { }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);
        var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        ValidateEmail(user, errors);
        ValidateUserName(user, errors);

        return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }

    private void ValidateEmail(User user, List<IdentityError> errors)
    {
        var userEmail = user?.Email;
        if (string.IsNullOrWhiteSpace(userEmail))
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                errors.Add(new IdentityError
                {
                    Code = "EmailIsNotSet",
                    Description = "Please set an email."
                });
            }

            return;
        }

        if (_emailsBanList.Any(email => userEmail.EndsWith(email, StringComparison.OrdinalIgnoreCase)))
        {
            errors.Add(new IdentityError
            {
                Code = "BadEmailDomainError",
                Description = "Please enter a valid email provider."
            });
        }
    }

    private static void ValidateUserName(User user, List<IdentityError> errors)
    {
        var userName = user?.UserName;
        if (string.IsNullOrWhiteSpace(userName))
        {
            errors.Add(new IdentityError
            {
                Code = "UserIsNotSet",
                Description = "Please complete the user informations."
            });

            return;
        }
    }
}