namespace EnglishTutorAI.Domain.Entities;

public class Sentence : Entity
{
    public string OriginalSentence { get; set; }

    public string CorrectedSentence { get; set; }
}