namespace EnglishTutorAI.Application.Interfaces;

public interface IDeleteDocumentService
{
    Task Delete(Guid documentId);
}