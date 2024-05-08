using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DD_QLQuanNet.data
{
    public class QLQuanNetContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TopUp> TopUps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=MSI\SQLEXPRESS;Initial Catalog=DD_QLQuanNet;Integrated Security=True;Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.User_ID);
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Customer_ID);
            });
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithMany(u => u.Customer)
                .HasForeignKey(c => c.User_ID);
            modelBuilder.Entity<Station>(entity =>
            {
                entity.HasKey(e => e.Station_ID);
            });
            modelBuilder.Entity<Station>()
                .HasOne(s => s.User)
                .WithMany(u => u.Station)
                .HasForeignKey(s => s.User_ID);
            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasKey(e => e.Service_ID);
            });
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Invoice_ID);
            });
            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.User)
                .WithMany(u => u.Invoice)
                .HasForeignKey(i => i.User_ID);
            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.Report_ID);
            });
            modelBuilder.Entity<Report>()
                .HasOne(r => r.User)
                .WithMany(u => u.Report)
                .HasForeignKey(r => r.User_ID);
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Order_ID);
            });
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Order)
                .HasForeignKey(o => o.User_ID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Service)
                .WithMany(s => s.Order)
                .HasForeignKey(o => o.Service_ID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Station)
                .WithMany(s => s.Order)
                .HasForeignKey(o => o.Station_ID);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Invoice)
                .WithMany(i => i.Order)
                .HasForeignKey(o => o.Invoice_ID);
            modelBuilder.Entity<TopUp>(entity =>
            {
                entity.HasKey(e => e.TopUp_ID);
            });
            modelBuilder.Entity<TopUp>()
                .HasOne(d => d.User)
                .WithMany(u => u.TopUp)
                .HasForeignKey(d => d.User_ID);
        }

        public class User
        {
            public int User_ID { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string Role { get; set; }
            public string Status { get; set; }
            public decimal Balance { get; set; }

            // Thuộc tính navigation
            public Collection<Customer> Customer { get; set; }

            public Collection<Station> Station { get; set; }
            public Collection<Invoice> Invoice { get; set; }
            public Collection<Report> Report { get; set; }
            public Collection<Order> Order { get; set; }
            public Collection<TopUp> TopUp { get; set; }
        }

        public class Customer
        {
            public int Customer_ID { get; set; }
            public string? Full_Name { get; set; }
            public DateTime? Birthdate { get; set; }
            public string? Gender { get; set; }
            public string? Email { get; set; }
            public string? Address { get; set; }
            public string? Phone { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
        }

        public class Station
        {
            public int Station_ID { get; set; }
            public string Station_Name { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }
            public decimal Price_Per_Hour { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
            public Collection<Order> Order { get; set; }
        }

        public class Service
        {
            public int Service_ID { get; set; }
            public string Service_Name { get; set; }
            public decimal Price { get; set; }
            public int Category { get; set; }
            public string Description { get; set; }
            public string Image { get; set; }
            public Collection<Order> Order { get; set; }
        }

        public class Invoice
        {
            public int Invoice_ID { get; set; }
            public DateTime Date { get; set; }
            public decimal Total { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
            public Collection<Order> Order { get; set; }
        }

        public class Order
        {
            public int Order_ID { get; set; }
            public DateTime Order_Date { get; set; }
            public int Quantity { get; set; }
            public decimal TotalCost { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
            public int Service_ID { get; set; }
            public Service Service { get; set; }
            public int Station_ID { get; set; }
            public Station Station { get; set; }
            public int Invoice_ID { get; set; }
            public Invoice Invoice { get; set; }
        }

        public class Report
        {
            public int Report_ID { get; set; }
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public decimal Total_Recharge { get; set; }
            public decimal Total_Service { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
        }

        public class TopUp
        {
            public int TopUp_ID { get; set; }
            public DateTime TopUp_Date { get; set; }
            public decimal Amount { get; set; }
            public string? Description { get; set; }
            public int User_ID { get; set; }
            public User User { get; set; }
        }
    }
}