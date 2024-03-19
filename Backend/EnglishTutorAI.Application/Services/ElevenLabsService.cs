using ElevenLabs;
using ElevenLabs.Models;
using EnglishTutorAI.Application.Configurations;
using EnglishTutorAI.Application.Interfaces;
using Microsoft.Extensions.Options;

namespace EnglishTutorAI.Application.Services;

public class ElevenLabsService : IElevenLabsService
{
    private readonly ElevenLabsConfig _elevenLabsConfig;

    public ElevenLabsService(IOptionsMonitor<ElevenLabsConfig> elevenLabsConfig)
    {
        _elevenLabsConfig = elevenLabsConfig.CurrentValue;
    }

    public async Task GenerateSpeechAsync(string text)
    {
        var elevenLabsApi = new ElevenLabsClient(_elevenLabsConfig.Key);
        var voice = await elevenLabsApi.VoicesEndpoint.GetVoiceAsync(_elevenLabsConfig.VoiceId);
        await using var outputFileStream = File.OpenWrite("myfile.mp3");
        await elevenLabsApi.TextToSpeechEndpoint.TextToSpeechAsync(text, voice, model: Model.MultiLingualV2,
            partialClipCallback: async (partialClip) => { await outputFileStream.WriteAsync(partialClip.ClipData); });
    }
}