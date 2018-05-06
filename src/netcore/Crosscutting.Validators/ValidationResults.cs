using Crosscutting.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Crosscutting.Validators
{
    public class ValidationResults
    {
        public IEnumerable<ValidationResult> Results { get; private set; }

        public static ValidationResults Join([IsNotNull] ValidationResults first, [IsNotNull] ValidationResults second)
        {
            Guard.IsNotNull(first, nameof(first));
            Guard.IsNotNull(second, nameof(second));

            return new ValidationResults(first.Results.Concat(second.Results));
        }

        public static ValidationResults Success
        {
            get
            {
                return new ValidationResults();
            }
        }

        public bool Succeeded
        {
            get
            {
                return !Results.Any();
            }
        }

        public string ErrorMessage
        {
            get
            {
                if (!Results.Any())
                {
                    return null;
                }

                return Results.Last().ErrorMessage;
            }
        }

        public IEnumerable<string> MemberNames
        {
            get
            {
                if (!Results.Any())
                {
                    return Enumerable.Empty<string>();
                }

                return Results.Last().MemberNames;
            }
        }

        public ValidationResults(string errorMessage)
        {
            Results = new List<ValidationResult>(new[] { new ValidationResult(errorMessage) });
        }

        public ValidationResults(string errorMessage, IEnumerable<string> memberNames)
        {
            Results = new List<ValidationResult>(new[] { new ValidationResult(errorMessage, memberNames) });
        }

        public ValidationResults(IEnumerable<ValidationResult> validationResults)
        {
            Guard.IsNotNull(validationResults, nameof(validationResults));

            Results = validationResults;
        }

        private ValidationResults()
        {
            Results = new List<ValidationResult>();
        }
    }
}
