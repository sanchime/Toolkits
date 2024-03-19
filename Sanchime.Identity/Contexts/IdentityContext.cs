using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Sanchime.EntityFrameworkCore;
using Sanchime.Identity.Entities;

namespace Sanchime.Identity.Contexts;

public sealed class IdentityContext(DbContextOptions options, IServiceProvider provider) : SanchimeDbContext(options, provider)
{

    public DbSet<Account> Accounts { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<UserGroup> UserGroups { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    protected override ValueTask VisitEntry(EntityEntry entry)
    {
        if (entry.Entity is Account account && entry.State is EntityState.Added)
        {
            account.User ??= new User { Name = account.LoginName, Account = account };
        }

        return ValueTask.CompletedTask;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<IdentityEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Ignore(x => x.DomainEvents);
            entity.HasIndex(x => x.IsDeleted);
            entity.HasIndex(x => x.IsEnabled);
            entity.HasIndex(x => new { x.CreatedDate, x.CreatedUser, x.CreatedUserName, x.ModifiedDate, x.ModifiedUser, x.ModifiedUserName }).IsDescending();
            
            entity.Property(x => x.IsEnabled)
                .HasDefaultValue(true);

            entity.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
            
            entity.UseTpcMappingStrategy();
        });

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasIndex(x => x.LoginName).IsUnique();

            entity.HasOne(x => x.User).WithOne(x => x.Account).HasForeignKey<User>(x => x.AccountId);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasMany(x => x.Roles)
                .WithMany(x => x.Users);
        });
        modelBuilder.Entity<UserGroup>(entity =>
        {
            entity.HasMany(x => x.Users).WithOne(x => x.UserGroup);

            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false);
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasMany(x => x.Permissions)
                .WithMany(x => x.Roles);

            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false);
        });
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .IsRequired(false);
        });
    }

}
