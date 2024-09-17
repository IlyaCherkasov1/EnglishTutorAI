using System.Linq.Expressions;
using EnglishTutorAI.Application.Models.Common;
using EnglishTutorAI.Domain.Entities;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public interface ISpecification<T>
    where T : Entity
{
    Expression<Func<T, bool>>? Criteria { get; }

    List<Expression<Func<T, object>>> Includes { get; }

    List<(Expression<Func<T, object>> KeySelector, bool IsDescending)> OrderByExpressions { get; }
}