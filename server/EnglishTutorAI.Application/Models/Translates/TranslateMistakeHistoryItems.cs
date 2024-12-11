namespace EnglishTutorAI.Application.Models.Translates;

public class TranslateMistakeHistoryItems
{
    public Guid Id { get; set; }

    public required string TranslatedText { get; set; }

    public required string CorrectedText { get; set; }
}