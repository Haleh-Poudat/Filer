using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.Utility.Constants
{
    public static class MessageText
    {
        public const string CaptchaError = "Your CAPTCHA code was not verified. Please try again.";
        public const string PhoneNumberError = "Please enter your mobile number carefully.";
        public const string OldUserNameError = "Your current national ID is incorrect. Please enter the national ID carefully.";
        public const string PhoneVerificationFailure = "Your mobile number could not be verified. Please verify your mobile number first.";
        public const string ExistUserName = "Your national ID is already registered in the system. You can log in using your mobile number and password.";
        public const string ExistNewPhoneNumber = "Your new mobile number is already registered in the system and cannot replace your current mobile number.";
        public const string PhoneNumberNotRegistered = "Your mobile number is not registered in the system. Please register now with just a few clicks.";
        public const string UserNameNotRegistered = "Your national ID is not registered in the system. Please contact support.";
        public const string ChangePhoneNumber = "Your mobile number has been successfully updated. Please log in again with your new mobile number.";
        public const string UserNameOrPasswordFailure = "The username or password is incorrect.";
        public const string ExpiredCodeError = "The verification code has expired. Please request a new code and try again.";
        public const string IsAuthenticated = "You are already logged in.";
        public const string RequiredIsAuthenticated = "You must log in first.";
        public const string WrongInput = "Please enter the values carefully.";
        public const string AccountVerify = "Your account has been successfully verified.";
        public const string IsLockedOut = "Your account has been locked for 6 minutes due to 6 consecutive failed login attempts.";
        public const string ChangePassword = "A new password has been successfully created. You can now log in to the site with your new password.";
        public const string WrongCode = "The code entered is incorrect. Please try again.";
        public const string TurnResendCode = "Note: The verification code can be resent after the confirmation time expires.";
        public const string OperationSuccess = "The operation was completed successfully.";
        public const string PleaseTryAgain = "The operation failed. Please try again.";
        public const string OperationUnSuccessful = "The operation failed.";
        public const string OperationFailure = "The requested information could not be found. Please contact support to resolve the issue.";
        public const string CannotBeEmpty = "The form must have at least one field filled out.";
    }
}