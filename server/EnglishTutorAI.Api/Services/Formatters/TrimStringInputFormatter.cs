using System.Text;
using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Constants;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnglishTutorAI.Api.Services.Formatters;

public partial class TrimStringInputFormatter : TextInputFormatter
{
    public TrimStringInputFormatter()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse(ContentTypes.Json));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
    {
        using var reader = new StreamReader(
            context.HttpContext.Request.Body,
            encoding,
            detectEncodingFromByteOrderMarks: true,
            bufferSize: 1024,
            leaveOpen: true);

        var json = await reader.ReadToEndAsync();

        try
        {
            if (json.Trim().StartsWith('{'))
            {
                var jsonObject = JObject.Parse(json);

                foreach (var prop in jsonObject.Properties().Where(p => p.Value.Type == JTokenType.String))
                {
                    jsonObject[prop.Name] = SpaceRegex().Replace(prop.Value.ToString().Trim(), " ");
                }

                var deserializedObject = JsonConvert.DeserializeObject(jsonObject.ToString(), context.ModelType);
                return await InputFormatterResult.SuccessAsync(deserializedObject);
            }

            if (json.Trim().StartsWith("\"") && json.Trim().EndsWith("\""))
            {
                var trimmedString = SpaceRegex().Replace(json.Trim('"').Trim(), " ");
                return await InputFormatterResult.SuccessAsync(trimmedString);
            }

            var deserializedPrimitive = JsonConvert.DeserializeObject(json, context.ModelType);
            return await InputFormatterResult.SuccessAsync(deserializedPrimitive);
        }
        catch (JsonReaderException ex)
        {
            context.ModelState.AddModelError(context.ModelName, "Invalid JSON: " + ex.Message);
            return await InputFormatterResult.FailureAsync();
        }
    }

    [GeneratedRegex(@"\s+")]
    private static partial Regex SpaceRegex();
}