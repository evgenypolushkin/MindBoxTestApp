using System;
using System.Linq;
using Mapster;
using MindboxTestApp.Core.DTO.Requests;
using MindboxTestApp.Core.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MindboxTestApp.Core.Converters.JSON
{
    public class FigureJsonConverter : JsonConverter<AddFigureRequestBaseDto>
    {
        public override void WriteJson(JsonWriter writer, AddFigureRequestBaseDto value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }

        public override AddFigureRequestBaseDto ReadJson(JsonReader reader, Type objectType,
            AddFigureRequestBaseDto existingValue,
            bool hasExistingValue, JsonSerializer serializer)
        {
            JToken createDto = JToken.Load(reader);
            const string figureTypeTokenPath = nameof(AddFigureRequestBaseDto.Type);
            JToken figureTypeToken = createDto[figureTypeTokenPath];
            if (figureTypeToken == null)
            {
                throw new JsonSerializationException(
                    $"Type of the figure must be specified. Path: {figureTypeTokenPath}", figureTypeTokenPath, 0, 0,
                    null);
            }

            var figureType = figureTypeToken.ToObject<FigureType>();
            if (!Enum.IsDefined(typeof(FigureType), figureType))
                OnInvalidFigureType(figureTypeTokenPath);

            AddFigureRequestBaseDto specificFigureCreateDto;
            try
            {
                specificFigureCreateDto = figureType.Adapt<AddFigureRequestBaseDto>();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new JsonSerializationException(
                    $"Currently the processing of the figure type {figureType} is not implemented.");
            }

            JsonConvert.PopulateObject(createDto.ToString(), specificFigureCreateDto);
            return specificFigureCreateDto;
        }

        private static void OnInvalidFigureType(string path)
        {
            throw new JsonReaderException(
                $@"The {path} value does not match any of the valid values. Valid values: " +
                $@"{Enum.GetValues(typeof(FigureType))
                    .Cast<FigureType>()
                    .Select(i => $"{(int) i} - ({Enum.GetName(typeof(FigureType), i)})")
                    .Aggregate((s1, s2) => $"{s1}, {s2}")}");
        }

        private static AddFigureRequestBaseDto GetSpecificFigureCreateDto(FigureType figureType)
        {
            switch (figureType)
            {
                case FigureType.Circle: return new AddCircleRequestDto();
                case FigureType.Triangle: return new AddTriangleRequestDto();
                default:
                    throw new JsonSerializationException("Type of the figure specified is not supported");
            }
        }
    }
}