using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessIdValidator
{
    public class BusinessIdSpecification : ISpecification<string>
    {
        #region Private members
        private List<string> _validationErrors;
        #endregion

        #region ISpecification implementation
        public IEnumerable<string> ReasonsForDissatisfaction
        {
            get
            {
                return _validationErrors;
            }
        }

        public bool IsSatisfiedBy(string entity)
        {
            _validationErrors = new List<string>();

            if (!validateLength(entity))
                _validationErrors.Add(Resources.Resources.NotValidLength);

            if (!validateSeparatorMark(entity))
                _validationErrors.Add(Resources.Resources.MissingSeparatorMark);

            if (!validateThatCharsAreNumbers(entity))
                _validationErrors.Add(Resources.Resources.NotNumbers);

            if (_validationErrors.Count == 0)
            {
                if (!validateCheckSum(entity))
                {
                    _validationErrors.Add(Resources.Resources.CheckSumDoesNotMatch);
                    return false;
                }
                return true;
            }

            return false;
        }
        #endregion

        #region Private methods
        private bool validateLength(string s)
        {
            return (s.Length == 9);
        }

        private bool validateSeparatorMark(string s)
        {
            if (s.Length > 7)
                return (s[7] == '-');

            return false;
        }

        private bool validateThatCharsAreNumbers(string s)
        {
            if (s.Length > 2 && s[s.Length - 2] == '-')
                s = s.Remove(s.Length-2, 1);

            foreach (char c in s.ToCharArray())
            {
                if (!Char.IsNumber(c))
                    return false;
            }
            return true;
        }

        private bool validateCheckSum(string s)
        {
            int[] weightNumbers = new int[7] { 7, 9, 10, 5, 8, 4, 2 };

            int weightedSum = 0;
            for (int i = 0; i < 7; i++)
                weightedSum += (int)Char.GetNumericValue(s[i]) * weightNumbers[i];

            int reminder = weightedSum % 11;
            int calculatedCheckSum;

            if (reminder == 0)
                calculatedCheckSum = 0;
            else if (reminder == 1)
                return false;
            else if (reminder >= 2 && reminder <= 10)
                calculatedCheckSum = 11 - reminder;
            else
                return false;

            return ((int)Char.GetNumericValue(s[8]) == calculatedCheckSum);
        }
        #endregion
    }
}
