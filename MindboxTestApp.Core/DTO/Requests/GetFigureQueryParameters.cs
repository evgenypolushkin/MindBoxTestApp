using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MindboxTestApp.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MindboxTestApp.Core.DTO.Requests
{
    public class GetFigureQueryParameters : IValidatableObject
    {
        [EnumDataType(typeof(FigureType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public FigureType? Type { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Type.HasValue)
            {
                yield return new ValidationResult("The type of the figure must be specified as query parameter",
                    new[] {nameof(Type)});
            }
        }
    }
}