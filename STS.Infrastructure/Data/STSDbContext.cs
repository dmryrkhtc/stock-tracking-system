using Microsoft.EntityFrameworkCore;
using STS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Infrastructure.Data
{
    public class STSDbContext : DbContext
    {
        public STSDbContext() { } //patameterless constructor
        // onconfiguring eklemek yerine bunu kullandım program.cs üzerinden ayarlanacak
        public STSDbContext(DbContextOptions<STSDbContext> options) : base(options) { }


        //ef core veritabaninda entityler icin tablo olusturur
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=DMRYRKHTC;Database=STSDB;Trusted_Connection=True;TrustServerCertificate=True;"
                );
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //company
            modelBuilder.Entity<Company>(e =>
            {


                e.Property(x => x.Name).IsRequired().HasMaxLength(150);

                e.Property(x => x.TaxNo).IsRequired().HasMaxLength(50);

                e.Property(x => x.Address).IsRequired().HasMaxLength(250);

                e.Property(x => x.TelNo).IsRequired().HasMaxLength(20);

                e.Property(x => x.Email).HasMaxLength(100);


                //vergi numaramizin benzersiz olmasini istedik
                e.HasIndex(x => x.TaxNo).IsUnique();

                //company 1-N Users
                e.HasMany(c => c.Users)
                .WithOne(u => u.Company)
                .HasForeignKey(u => u.CompanyId)
                .OnDelete(DeleteBehavior.Restrict); // firma silinince kullanici/urunleri zorla silinmesin

                // company 1-N products
                e.HasMany(c => c.Products)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);



            });


            //User
            modelBuilder.Entity<User>(e =>
            {


                e.Property(x => x.Name).IsRequired().HasMaxLength(50);

                e.Property(x => x.LastName).IsRequired().HasMaxLength(50);

                e.Property(x => x.Email).HasMaxLength(100);

              // ayni firmada email tek olsun
              //companyid=fk
                e.HasIndex(x => new { x.Email, x.CompanyId }).IsUnique();
            });



            //Product
            modelBuilder.Entity<Product>(e =>
            {

                e.Property(x => x.Name).IsRequired().HasMaxLength(120);
                //unit enum ek konfigurasyon gerektirmez int olarak saklanir
                // string saklamak istersem e.Property(p=>p.Unit).HasConversion<string>();)
                e.HasIndex(x => x.Barcode).IsUnique();

            });



            //Stock
            modelBuilder.Entity<Stock>(e =>
            {
                e.HasOne(s => s.Product)
                .WithMany(p => p.Stocks)//bir urun iki depodada depolanabilir
                .HasForeignKey(s => s.ProductId)
                .OnDelete(DeleteBehavior.Cascade); //ana nesne silinirse bagimli nesneklerin hepsi silinir

                e.Property(s => s.Quantity).IsRequired();
                //store int olarak saklandigi icin bir sey yapmadik


                //ayni urun+ayni magaza
                e.HasIndex(s => new { s.ProductId, s.Store }).IsUnique();
            });


            modelBuilder.Entity<StockMovement>(e =>
            {
                e.HasOne(m => m.Product)
                .WithMany(p => p.StockMovements)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Cascade); //ana nesne silinirse bagimli nesneklerin hepsi silinir

                e.Property(m => m.Quantity).IsRequired();

                //tarih gonderilmezse server zamani yazin
                e.Property(m => m.Date).HasDefaultValueSql("GETUTCDATE()");

           
            });

            base.OnModelCreating(modelBuilder);
        }


    }






}
