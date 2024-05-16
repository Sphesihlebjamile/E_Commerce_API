
namespace E_Commerce_API.Repositories.Abstract
{
    public interface IValidationRepsitory
    {
        bool IsPasswordValid(string password);
        bool IsPasswordValid(string password, string ConfirmPassword);
        bool IsPhoneNumberValid(string phone);
    }
}