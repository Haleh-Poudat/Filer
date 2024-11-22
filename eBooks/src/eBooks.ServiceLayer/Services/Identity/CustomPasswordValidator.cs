using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using eBooks.Domain.Entities.Identity;
using eBooks.ServiceLayer.Contracts.Identity;
using eBooks.ViewModel;

namespace eBooks.ServiceLayer.Services.Identity;

public class CustomPasswordValidator : PasswordValidator<User>
{
    private readonly ISet<string> _passwordsBanList;

    public CustomPasswordValidator(
        IdentityErrorDescriber errors
        ) : base(errors) { }

    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();

        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordIsNotSet",
                Description = "Please complete the password."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        if (string.IsNullOrWhiteSpace(user?.UserName))
        {
            errors.Add(new IdentityError
            {
                Code = "UserNameIsNotSet",
                Description = "Please complete the username."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        // First use the built-in validator
        var result = await base.ValidateAsync(manager, user, password);
        errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();

        // Extending the built-in validator
        if (password.Contains(user.UserName, StringComparison.OrdinalIgnoreCase))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordContainsUserName",
                Description = "The password cannot contain part of the username."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        if (!IsSafePasword(password))
        {
            errors.Add(new IdentityError
            {
                Code = "PasswordIsNotSafe",
                Description = "The entered password is not safe."
            });
            return IdentityResult.Failed(errors.ToArray());
        }

        return !errors.Any() ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
    }

    private static bool AreAllCharsEqual(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return false;
        }

        data = data.ToLowerInvariant();
        var firstElement = data.ElementAt(0);
        var euqalCharsLen = data.ToCharArray().Count(x => x == firstElement);
        if (euqalCharsLen == data.Length)
        {
            return true;
        }

        return false;
    }

    private bool IsSafePasword(string data)
    {
        if (string.IsNullOrWhiteSpace(data))
        {
            return false;
        }

        if (data.Length < 5)
        {
            return false;
        }

        if (_passwordsBanList.Contains(data.ToLowerInvariant()))
        {
            return false;
        }

        if (AreAllCharsEqual(data))
        {
            return false;
        }

        return true;
    }
}