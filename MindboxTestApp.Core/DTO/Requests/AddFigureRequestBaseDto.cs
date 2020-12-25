using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MindboxTestApp.Core.Converters.JSON;
using MindboxTestApp.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MindboxTestApp.Core.DTO.Requests
{
    [JsonConverter(typeof(FigureJsonConverter))]
    public class AddFigureRequestBaseDto : IValidatableObject
    {
        protected AddFigureRequestBaseDto()
        {
        }

        [EnumDataType(typeof(FigureType))]
        [JsonConverter(typeof(StringEnumConverter))]
        public FigureType? Type { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Type.HasValue)
                yield return new ValidationResult("Figure type must be specified", new[] {nameof(Type)});
        }
    }
}