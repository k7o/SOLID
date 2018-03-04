using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Crosscutting.Contracts
{
    public class ValidationResults
    {
        readonly IEnumerable<ValidationResult> _validationResults;

        public override bool Equals(object obj)
        {
            return _validationResults.Equals(obj);
        }

        public override int GetHashCode()
        {
            return _validationResults.GetHashCode();
        }

        public override string ToString()
        {
            return ErrorMessage ?? _validationResults.ToString();
        }

        public static bool operator ==(ValidationResults validationResults1, ValidationResults validationResults2)
        {
            if (validationResults1._validationResults == validationResults2._validationResults)
            {
                return true;
            }

            if (!validationResults1._validationResults.Any() &&
                !validationResults2._validationResults.Any())
            {
                return true;
            }

            return false;
        }

        public static bool operator !=(ValidationResults validationResults1, ValidationResults validationResults2)
        {
            if (validationResults1._validationResults == validationResults2._validationResults)
            {
                return true;
            }

            if (validationResults1._validationResults.Count() !=
                validationResults2._validationResults.Count())
            {
                return true;
            }

            return false;
        }

        public static ValidationResults Join(ValidationResults first, ValidationResults second)
        {
            return new ValidationResults(first._validationResults.Concat(second._validationResults));
        }

        public static ValidationResults Success
        {
            get
            {
                return new ValidationResults();
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (!_validationResults.Any())
                {
                    return null;
                }

                return _validationResults.Last().ErrorMessage;
            }
        }

        public IEnumerable<string> MemberNames
        {
            get
            {
                if (!_validationResults.Any())
                {
                    return Enumerable.Empty<string>();
                }

                return _validationResults.Last().MemberNames;
            }
        }

        public ValidationResults(string errorMessage)
        {
            _validationResults = new List<ValidationResult>(new[] { new ValidationResult(errorMessage) });
        }

        public ValidationResults(string errorMessage, IEnumerable<string> memberNames)
        {
            _validationResults = new List<ValidationResult>(new[] { new ValidationResult(errorMessage, memberNames) });
        }

        public ValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            Guard.IsNotNull(validationResults, nameof(validationResults));

            _validationResults = validationResults;
        }

        private ValidationResults()
        {
            _validationResults = new List<ValidationResult>();
        }
    }
}
