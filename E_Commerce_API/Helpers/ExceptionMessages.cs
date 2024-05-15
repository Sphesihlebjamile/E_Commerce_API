using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce_API.Helpers
{
    public static class ExceptionMessages
    {
        public static string InvalidParameterData
            = "Input data is not in the correct format";
        
        public static string Product_Edit_IdsDontMatch
            = "The provided Id's do not match";
    }
}