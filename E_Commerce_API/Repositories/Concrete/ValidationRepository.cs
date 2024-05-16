using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce_API.Repositories.Abstract;

namespace E_Commerce_API.Repositories.Concrete
{
    public class ValidationRepository
        : IValidationRepsitory
    {
        public bool IsPasswordValid(string password)
        {
            if(password.Length < 8)
                return false;

            List<string> symbols = [
                "~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_",
                "<", "/", ".", ",", "", ":", ";", "}", "{", "]", "[", "=", "+",
                ">", "?", "|", "\\",
            ];
            List<string> numbers = ["0","1","2","3","4","5","6","7","8","9"];
            List<string> lowerAlphabet = [
                "a","b","c","d","e","f","g","h",
                "i","j","k","l","m","n","o","p",
                "q","r","s","t","u","v","w","x","y","z"
            ];
            List<string> upperAlphabet = lowerAlphabet.Select(letter => letter.ToUpper()).ToList();

            int numOfSymbols = password.Where(
                        character => symbols.Contains(character.ToString())
                    ).Count();
            int numOfNumbers = password.Where(
                        character => numbers.Contains(character.ToString())
                    ).Count();
            int numOfLowerCaseLetters = password.Where(
                        character => lowerAlphabet.Contains(character.ToString())
                    ).Count();
            int numOfUpperCaseLetters = password.Where(
                        character => upperAlphabet.Contains(character.ToString())
                    ).Count();
            
            return numOfSymbols >= 2 &&
                    numOfNumbers >= 2 &&
                    numOfLowerCaseLetters >= 2 &&
                    numOfUpperCaseLetters >= 2;
        }

        public bool IsPasswordValid(string password, string confirmPassword)
        {
            if(!password.Equals(confirmPassword))
                return false;
            return IsPasswordValid(password) && IsPasswordValid(confirmPassword);
        }

        public bool IsPhoneNumberValid(string phone)
        {
            if(phone.Length != 10)
                return false;
            
            if(phone[0] != '0')
                return false;

            try{
                foreach(char num in phone){
                    int numb = Convert.ToInt32(num.ToString());
                }
                return true;
            }
            catch{
                return false;
            }
        }
    }
}