using Microsoft.EntityFrameworkCore;
using servicedesk.Services.Tickets.Domain;

namespace servicedesk.Services.Tickets.Dal
{
    public class TicketDbContext : DbContext
    {
        public TicketDbContext(DbContextOptions<TicketDbContext> options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Address>().ToTable("WH_ADDRESSES");
            modelBuilder.Entity<Address>().HasKey(r => r.Id);
            modelBuilder.Entity<Address>().HasKey(r => r.Id);

            modelBuilder.Entity<Client>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Client>().ToTable("WH_CLIENTS");
            modelBuilder.Entity<Client>().HasKey(r => r.Id);

            modelBuilder.Entity<Ticket>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ticket>().ToTable("WH_TICKETS");
            modelBuilder.Entity<Ticket>().HasKey(r => r.Id);

            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().ToTable("WH_USERS");
            modelBuilder.Entity<User>().HasKey(r => r.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
