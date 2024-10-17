namespace EnglishTutorAI.Application.Interfaces;

public interface ISingleEntryCache
{
    string? Get(string key);

    void Set(string key, string value);
}