using Microsoft.EntityFrameworkCore;
using apiTest.Models;

namespace apiTest.Data
{
    public class IssueDbContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public IssueDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }
    }
}

