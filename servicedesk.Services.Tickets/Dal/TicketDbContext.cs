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
            modelBuilder.Entity<Address>().ToTable("WH_Addresses");
            modelBuilder.Entity<Address>().HasKey(r => r.Id);

            modelBuilder.Entity<BusinessUnit>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<BusinessUnit>().ToTable("WH_BusinessUnits");
            modelBuilder.Entity<BusinessUnit>().HasKey(r => r.Id);

            modelBuilder.Entity<Contract>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contract>().ToTable("WH_Contracts");
            modelBuilder.Entity<Contract>().HasKey(r => r.Id);
            modelBuilder.Entity<Contract>().Property(r => r.CreatedAt).ValueGeneratedOnAdd();
            modelBuilder.Entity<Contract>().Property(r => r.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Customer>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Customer>().ToTable("WH_Customers");
            modelBuilder.Entity<Customer>().HasKey(r => r.Id);

            modelBuilder.Entity<Service>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Service>().ToTable("WH_Services");
            modelBuilder.Entity<Service>().HasKey(r => r.Id);

            modelBuilder.Entity<Supplier>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Supplier>().ToTable("WH_Suppliers");
            modelBuilder.Entity<Supplier>().HasKey(r => r.Id);

            modelBuilder.Entity<Ticket>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ticket>().ToTable("WH_Tickets");
            modelBuilder.Entity<Ticket>().HasKey(r => r.Id);
            modelBuilder.Entity<Ticket>().Property(r => r.CreatedAt).ValueGeneratedOnAdd();
            modelBuilder.Entity<Ticket>().Property(r => r.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<TicketPriority>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TicketPriority>().ToTable("WH_TicketPriority");
            modelBuilder.Entity<TicketPriority>().HasKey(r => r.Id);

            modelBuilder.Entity<TicketStatus>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<TicketStatus>().ToTable("WH_TicketStatus");
            modelBuilder.Entity<TicketStatus>().HasKey(r => r.Id);

            modelBuilder.Entity<User>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().ToTable("WH_Users");
            modelBuilder.Entity<User>().HasKey(r => r.Id);
            modelBuilder.Entity<User>().Property(r => r.CreatedAt).ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().Property(r => r.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<Work>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Work>().ToTable("WH_Works");
            modelBuilder.Entity<Work>().HasKey(r => r.Id);
            modelBuilder.Entity<Work>().Property(r => r.CreatedAt).ValueGeneratedOnAdd();
            modelBuilder.Entity<Work>().Property(r => r.UpdatedAt).ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<WorkStatus>().Property(p => p.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<WorkStatus>().ToTable("WH_WorkStatus");
            modelBuilder.Entity<WorkStatus>().HasKey(r => r.Id);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BusinessUnit> BusinessUnits { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketPriority> TicketPriority { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkStatus> WorkStatus { get; set; }
    }
}
