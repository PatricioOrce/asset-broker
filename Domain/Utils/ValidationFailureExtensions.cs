using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Utils
{
    public static class ValidationFailureExtensions
    {
        public static string ConvertToFormattedText(this List<ValidationFailure> failures)
        {
            if (failures == null || failures.Count == 0)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Validation errors:");

            foreach (var failure in failures)
            {
                sb.AppendLine($"- {failure.PropertyName}: {failure.ErrorMessage}");
            }

            return sb.ToString();
        }
    }
}
