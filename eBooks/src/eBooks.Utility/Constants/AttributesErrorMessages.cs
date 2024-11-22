using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBooks.Utility.Constants
{
    public static class AttributesErrorMessages
    {
        public const string EnterName = "Please enter {0}";
        public const string WrongEntry = "{0} is incorrect";
        public const string UserNameInvalid = "The national ID is invalid or duplicate";
        public const string PasswordStructure = "The password must be at least 6 characters long and include both uppercase and lowercase letters and a number";
        public const string ComparePassword = "The passwords do not match";
        public const string ComparePhoneNumber = "The mobile numbers do not match";
        public const string PhoneNumberStructure = "Please use only numbers and enter 11 digits";
        public const string UserNameStructure = "Please use only numbers and enter 10 digits";
        public const string NameStructure = "Please use only Persian letters";
        public const string AllowedEmail = "The email is invalid";
        public const string RemotePhoneNumberMessage = "The mobile number is duplicate. Please use a different number";
        public const string RemoteRoleNameMessage = "The role name is duplicate. Please choose another name";
    }
}