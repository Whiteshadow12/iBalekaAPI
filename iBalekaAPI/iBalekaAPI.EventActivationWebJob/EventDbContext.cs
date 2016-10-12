using Microsoft.EntityFrameworkCore;
using iBalekaAPI.EventActivationWebJob.Models;

namespace iBalekaAPI.EventActivationWebJob
{
    public class EventDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=tcp:ibalekadb.database.windows.net,1433;Initial Catalog = iBalekaDB; User Id = iBalekaAdmin@ibalekadb;Password=WeWillWin2026;MultipleActiveResultSets=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public virtual DbSet<Event> Event { get; set; }
    }
}
