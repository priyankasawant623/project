using DataWarehouse.API.Models.RecordModels;
using Microsoft.EntityFrameworkCore;

namespace DataWarehouse.API;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets for your entities
    public DbSet<Order> Orders { get; set; }
}
