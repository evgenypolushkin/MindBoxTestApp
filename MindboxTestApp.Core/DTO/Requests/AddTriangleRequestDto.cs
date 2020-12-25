using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MindboxTestApp.Core.DTO.Requests
{
    public sealed class AddTriangleRequestDto : AddFigureRequestBaseDto
    {
        public double? ALength { get; set; }

        public double? BLength { get; set; }

        public double? AbAngle { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return base.Validate(validationContext)
                .Concat(ValidateTriangle(validationContext));
        }

        private IEnumerable<ValidationResult> ValidateTriangle(ValidationContext validationContext)
        {
            if (!ALength.HasValue || ALength.Value <= 0)
            {
                yield return new ValidationResult(
                    "The length of the A side must be specified and have a positive value",
                    new[] {nameof(ALength)});
            }

            if (!BLength.HasValue || BLength.Value <= 0)
            {
                yield return new ValidationResult(
                    "The length of the B side must be specified and have a positive value",
                    new[] {nameof(BLength)});
            }

            if (!AbAngle.HasValue || AbAngle.Value <= 0 || AbAngle.Value > 180)
            {
                yield return new ValidationResult(
                    "The angle between sides A and B must be specified and have a positive value less than 180.",
                    new[] {nameof(AbAngle)});
            }
        }
    }
}