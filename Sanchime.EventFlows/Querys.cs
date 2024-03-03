namespace Sanchime.EventFlows;

public interface IQuery<TQueriedResult>;

public interface IQueryHandler<in TQuery, TQueriedResult>
    where TQuery : IQuery<TQueriedResult>
{
    Task<TQueriedResult> Handle(TQuery query, CancellationToken cancellation = default);
}

public interface IQueryRequester
{
    Task<TQueriedResult> Request<TQuery, TQueriedResult>(TQuery query, CancellationToken cancellation = default)
        where TQuery : IQuery<TQueriedResult>;
}