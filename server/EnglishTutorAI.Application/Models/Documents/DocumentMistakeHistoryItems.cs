namespace EnglishTutorAI.Application.Models.Documents;

public class DocumentMistakeHistoryItems
{
    public Guid Id { get; set; }

    public required string TranslatedText { get; set; }

    public required string CorrectedText { get; set; }
}