
namespace E_Commerce_API.Helpers
{
    public static class ExceptionMessages
    {
        public static string InvalidParameterData
            = "Input data is not in the correct format";
        
        public static string Product_Edit_IdsDontMatch
            = "The provided Id's do not match";

        public static string User_Email_Invalid
            = "There provided email already exists";

        public static string User_Username_Invalid
            = "There provided username already exists";
        
        public static string User_PasswordsDontMatch
            = "Your passwords do not match or are not valid";
        
        public static string User_Phone_Invalid
            = "Your phone number is invalid";
    }
}