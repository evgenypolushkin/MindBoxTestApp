using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MindboxTestApp.Core.DTO.Requests
{
    /// <summary>
    ///     DTO to create a new circle
    /// </summary>
    public class AddCircleRequestDto : AddFigureRequestBaseDto
    {
        public double? Radius { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext)
                .Concat(ValidateCircle(validationContext));
        }

        private IEnumerable<ValidationResult> ValidateCircle(ValidationContext validationContext)
        {
            if (!Radius.HasValue)
                yield return new ValidationResult("Circle radius must be specified", new[] {nameof(Radius)});
            else
            {
                if (Radius.Value <= 0)
                    yield return new ValidationResult("Circle radius must be a positive value", new[] {nameof(Radius)});
            }
        }
    }
}