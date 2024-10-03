using System.Text;
using System.Text.RegularExpressions;
using EnglishTutorAI.Application.Constants;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EnglishTutorAI.Api.Services.Formatters;

public class TrimStringInputFormatter : TextInputFormatter
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
        var jsonObject = JObject.Parse(json);

        foreach (var prop in jsonObject.Properties().Where(p => p.Value.Type == JTokenType.String))
        {
            jsonObject[prop.Name] = Regex.Replace(prop.Value.ToString().Trim(), @"\s+", " ");
        }

        var deserialized = JsonConvert.DeserializeObject(jsonObject.ToString(), context.ModelType);
        return await InputFormatterResult.SuccessAsync(deserialized);
    }
}