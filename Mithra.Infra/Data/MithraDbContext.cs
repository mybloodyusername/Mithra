using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mithra.Domain.Entities;

namespace Mithra.Infra.Data;

public class MithraDbContext(DbContextOptions<MithraDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    
    // TODO:public DbSet<ENTITY> Entity => Set<Entity>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // TODO: builder.ApplyConfiguration(new ApplicationUserConfiguration());
        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // TODO: 
        return base.SaveChangesAsync(cancellationToken);
    }
}