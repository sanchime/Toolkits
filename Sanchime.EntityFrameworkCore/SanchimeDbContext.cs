using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Sanchime.Common;
using Sanchime.Common.Models;
using Sanchime.Common.Snowflakes;
using Sanchime.DependencyInjection;
using Sanchime.EventFlows;
using Sanchime.EventFlows.Entities;
using Sanchime.EventFlows.Seedworks;

namespace Sanchime.EntityFrameworkCore;

public abstract class SanchimeDbContext : DbContext, IUnitOfWork
{
    protected SanchimeDbContext(DbContextOptions options, IServiceProvider provider) : base(options)
    {
        provider.Autowired(this);
    }

    [Autowired]
    public ISnowflake Snowflake { get; internal set; } = null!;

    [Autowired]
    public IEventFlowMediator Mediator { get; internal set; } = null!;

    [Autowired]
    public ICurrentUserContext UserContext { get; internal set; } = null!;


    private IDbContextTransaction? _currentTransaction;

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction!;

    public bool HasActiveTransaction => _currentTransaction is not null;

    protected virtual ValueTask VisitEntry(EntityEntry entry)
    {
        return ValueTask.CompletedTask;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries();

        if (entries != null)
        {
            foreach (var entry in entries)
            {
                await VisitEntry(entry);
            }
            entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added && entry.Entity is Entity entity)
                {
                    entity.Id = Snowflake.NewLong();
                }

                if (entry.Entity is IDeleteable deleteable)
                {
                    if (entry.State is EntityState.Added)
                    {
                        deleteable.IsDeleted = false;
                    }
                    else if (entry.State is EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        deleteable.IsDeleted = true;
                    }
                }

                if (entry.Entity is ITraceableEntity traceable)
                {
                    if (entry.State is EntityState.Added)
                    {
                        traceable.CreatedDate = DateTimeOffset.UtcNow;
                        traceable.ModifiedDate = DateTimeOffset.UtcNow;
                        traceable.CreatedUser = UserContext.UserId;
                        traceable.ModifiedUser = UserContext.UserId;
                        traceable.CreatedUserName = UserContext.UserName;
                        traceable.ModifiedUserName = UserContext.UserName;
                    }
                    else if (entry.State is EntityState.Modified)
                    {
                        traceable.ModifiedDate = DateTimeOffset.UtcNow;
                        traceable.ModifiedUser = UserContext.UserId;
                        traceable.ModifiedUserName = UserContext.UserName;
                    }
                } 
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await Mediator.DispatchDomainEventsAsync(this);
        await this.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null!;

        _currentTransaction = await Database.BeginTransactionAsync();

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        ArgumentNullException.ThrowIfNull(transaction);

        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.RollbackAsync();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
