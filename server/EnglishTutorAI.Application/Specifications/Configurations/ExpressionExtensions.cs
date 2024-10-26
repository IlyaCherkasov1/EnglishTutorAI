using System.Linq.Expressions;

namespace EnglishTutorAI.Application.Specifications.Configurations;

public static class ExpressionExtensions
{
    public static Expression<Func<T, bool>> AndAlso<T>(
        this Expression<Func<T, bool>> leftExpression,
        Expression<Func<T, bool>> rightExpression)
    {
        var parameter = Expression.Parameter(typeof(T));

        var combined = Expression.AndAlso(
            Expression.Invoke(leftExpression, parameter),
            Expression.Invoke(rightExpression, parameter));

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }
}