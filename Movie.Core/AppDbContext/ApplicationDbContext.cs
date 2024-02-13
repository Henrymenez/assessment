using Microsoft.EntityFrameworkCore;
using Movie.Core.Entity;

namespace Movie.Core.AppDbContext;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }


    public DbSet<SearchQuery> SearchQueries { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
       

        base.OnModelCreating(builder);
    }

}
