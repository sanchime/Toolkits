using Sanchime.DynamicQueryable;
using System.Linq.Expressions;

namespace Sanchime.DynamicQueryable.Builders;

public sealed class DynamicQueryBuilder<TEntity>
{
    private readonly DynamicQuery _query;

    private readonly DynamicFilterGroup _group;

    private readonly TEntity _entity;

    private DynamicQueryBuilder(TEntity entity)
    {
        _entity = entity;
        _group = new DynamicFilterGroup();

        _query = new DynamicQuery() { Filters = [_group] };
    }

    private DynamicQueryBuilder(TEntity entity, DynamicQuery query, DynamicFilterGroup group)
    {
        _query = query;
        _group = group;
        _entity = entity;
    }

    public static DynamicQueryBuilder<TEntity> CreateQuery(TEntity entity) => new(entity);

    public DynamicQuery Build() => _query;

    public DynamicQueryBuilder<TEntity> AddGroup(DynamicConditionKind condition = DynamicConditionKind.And)
    {
        return new DynamicQueryBuilder<TEntity>(_entity, _query, new DynamicFilterGroup() { Condition = condition });
    }

    public DynamicQueryBuilder<TEntity> AddProperty<TProperty>(
        Expression<Func<TEntity, TProperty>> property,
        DynamicOperationMode operation = DynamicOperationMode.Equal,
        Func<TProperty, bool>? predicate = null,
        DynamicConditionKind condition = DynamicConditionKind.And)
    {
        if (property is not MemberExpression member)
        {
            throw new NotSupportedException();
        }

        var propertyValue = property.Compile().Invoke(_entity);
        if (predicate is null || predicate(propertyValue))
        {
            _group.Children.Add(new DynamicFilter
            {
                PropertyName = member.Member.Name,
                Value = propertyValue,
                Operation = operation,
                Condition = condition
            });
        }

        return this;
    }
}
