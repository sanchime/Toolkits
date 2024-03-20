using Sanchime.DynamicQueryable;
using System.Linq.Expressions;
using System.Reflection;

namespace Sanchime.DynamicQueryable;

public static class DynamicExpressionExtensions
{
    private readonly static MethodInfo StringContainsMethod = typeof(string).GetMethod(nameof(String.Contains), [typeof(string)])!;

    private readonly static MethodInfo EnumParseMethod = typeof(Enum).GetMethod(nameof(Enum.Parse), [typeof(Type), typeof(string)])!;

    /// <summary>
    /// 动态查询
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="filterGroups"></param>
    /// <returns></returns>
    public static IQueryable<T> Where<T>(this IQueryable<T> source, IList<DynamicFilterGroup> filterGroups)
    {
        var predicate = filterGroups.GetExpression<T>();

        return predicate != null ? source.Where(predicate) : source;
    }

    public static Expression<Func<T, bool>>? GetExpression<T>(this IList<DynamicFilterGroup> filterGroups)
    {
        var param = Expression.Parameter(typeof(T), "x");
        if (filterGroups?.Any() is not true)
        {
            return null;
        }

        Expression? expression = null;
        for (var i = 0; i < filterGroups.Count; i++)
        {
            var filterGroup = filterGroups[i];
            var filters = filterGroup.Children;
            if (filters.Count == 0)
            {
                return null;
            }
            Expression expressionItem = param.GetExpression(filters[0]);
            for (var filterIndex = 1; filterIndex < filters.Count; filterIndex++)
            {

                if (filters[filterIndex - 1].Condition is DynamicConditionKind.And)
                {
                    expressionItem = Expression.AndAlso(expressionItem, param.GetExpression(filters[filterIndex]));
                }
                else
                {
                    expressionItem = Expression.OrElse(expressionItem, param.GetExpression(filters[filterIndex]));
                }
            }
            if (i == 0)
            {
                expression = expressionItem;
            }
            else if (expression is not null)
            {
                if (filterGroups[i - 1].Condition is DynamicConditionKind.And)
                {
                    expression = Expression.AndAlso(expression, expressionItem);
                }
                else
                {
                    expression = Expression.OrElse(expression, expressionItem);
                }
            }
        }
        return expression is null ? null : Expression.Lambda<Func<T, bool>>(expression, param);
    }


    private static Expression GetExpression(this ParameterExpression param, DynamicFilter filter)
    {
        var member = Expression.Property(param, filter.PropertyName);
        Expression ConstantExpressionChoose(Type baseType, Type? nullableType) => (baseType, nullableType) switch
        {
            ({ IsEnum: true }, null) => Expression.Convert(Expression.Call(EnumParseMethod, Expression.Constant(baseType), Expression.Constant(filter.Value)), baseType),
            (_, { IsEnum: true }) => Expression.Convert(Expression.Call(EnumParseMethod!, Expression.Constant(nullableType), Expression.Constant(filter.Value)), baseType),
            (_, _) when baseType == typeof(string) => Expression.Constant(string.IsNullOrEmpty(filter.Value?.ToString()) ? null : filter.Value),
            (_, not null) _ => Expression.Convert(Expression.Call(nullableType.GetMethod("Parse", [typeof(string)])!, Expression.Constant(filter.Value)), baseType),
            (_, null) => Expression.Call(baseType.GetMethod("Parse", [typeof(string)])!, Expression.Constant(filter.Value))
        };


        var constant = ConstantExpressionChoose(member.Type, Nullable.GetUnderlyingType(member.Type));
        Expression DynamicOperationChoose(DynamicOperationMode operation) => operation switch
        {
            DynamicOperationMode.Equal => Expression.Equal(member, constant),
            DynamicOperationMode.NotEqual => Expression.NotEqual(member, constant),
            DynamicOperationMode.GreaterThanOrEqual => Expression.GreaterThanOrEqual(member, constant),
            DynamicOperationMode.LessThanOrEqual => Expression.LessThanOrEqual(member, constant),
            DynamicOperationMode.GreaterThan => Expression.GreaterThan(member, constant),
            DynamicOperationMode.LessThan => Expression.LessThan(member, constant),
            DynamicOperationMode.Contains => Expression.Call(member, StringContainsMethod, constant),
            _ => throw new NotSupportedException($"未实现{operation}运算")
        };

        return DynamicOperationChoose(filter.Operation);
    }
}
