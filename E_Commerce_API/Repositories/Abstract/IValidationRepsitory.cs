using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Repositories.Abstract
{
    public interface IValidationRepsitory
    {
        bool IsPasswordValid(string password);
        bool IsPasswordValid(string password, string ConfirmPassword);
        bool IsPhoneNumberValid(string phone);
    }
}