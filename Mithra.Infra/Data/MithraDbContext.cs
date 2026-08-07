using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mithra.Domain.Entities;
using Mithra.Infra.Configurations;

namespace Mithra.Infra.Data;

public class MithraDbContext(DbContextOptions<MithraDbContext> options)
    : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    
    // TODO:public DbSet<ENTITY> Entity => Set<Entity>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserConfig());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        // TODO: 
        return base.SaveChangesAsync(cancellationToken);
    }
}