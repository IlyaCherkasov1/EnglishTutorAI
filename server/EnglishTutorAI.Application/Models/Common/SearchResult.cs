namespace EnglishTutorAI.Application.Models.Common;

public class SearchResult<TResult>
{
    public required IReadOnlyList<TResult> Items { get; set; }

    public int TotalCount { get; set; }
}