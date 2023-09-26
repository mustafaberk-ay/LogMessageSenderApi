using LogMessageSenderApi3.Models;
using Microsoft.EntityFrameworkCore;

namespace LogMessageSenderApi3.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<LogMessage> Logs { get; set; }
    }
}
