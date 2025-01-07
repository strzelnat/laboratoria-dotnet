using System;
using System.Collections.Generic;
using Gravity.Models.Books;
using Microsoft.EntityFrameworkCore;

namespace Gravity.Model.Books
{
    public partial class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<AddressStatus> AddressStatuses { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookLanguage> BookLanguages { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<CustOrder> CustOrders { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public virtual DbSet<OrderHistory> OrderHistories { get; set; }
        public virtual DbSet<OrderLine> OrderLines { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja encji Address
            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.HasKey(e => e.AddressId)
                      .HasName("PK_address");

                entity.Property(e => e.AddressId)
                      .ValueGeneratedNever()
                      .HasColumnName("address_id");

                entity.Property(e => e.City)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("city");

                entity.Property(e => e.CountryId)
                      .HasColumnName("country_id");

                entity.Property(e => e.StreetName)
                      .IsRequired()
                      .HasMaxLength(200)
                      .HasColumnName("street_name");

                entity.Property(e => e.StreetNumber)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnName("street_number");

                entity.HasOne(d => d.Country)
                      .WithMany(p => p.Addresses)
                      .HasForeignKey(d => d.CountryId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_address_country");
            });

            // Konfiguracja encji AddressStatus
            modelBuilder.Entity<AddressStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                      .HasName("PK_address_status");

                entity.ToTable("address_status");

                entity.Property(e => e.StatusId)
                      .ValueGeneratedNever()
                      .HasColumnName("status_id");

                entity.Property(e => e.AddressStatus1)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnName("address_status");

                entity.HasMany(s => s.CustomerAddresses)
                      .WithOne(ca => ca.Status)
                      .HasForeignKey(ca => ca.StatusId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_customer_address_address_status");
            });

            // Konfiguracja encji Author
            modelBuilder.Entity<Author>(entity =>
            {
                entity.ToTable("author");

                entity.HasKey(e => e.AuthorId)
                      .HasName("PK_author");

                entity.Property(e => e.AuthorId)
                      .ValueGeneratedOnAdd() // Zmiana z ValueGeneratedNever() na ValueGeneratedOnAdd()
                      .HasColumnName("author_id")
                      .HasAnnotation("Sqlite:Autoincrement", true); // Dodana adnotacja

                entity.Property(e => e.AuthorName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("author_name");
            });

            // Konfiguracja encji Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.ToTable("book");

                entity.HasKey(e => e.BookId)
                      .HasName("PK_book");

                entity.Property(e => e.BookId)
                      .ValueGeneratedOnAdd()
                      .HasColumnName("book_id")
                      .HasAnnotation("Sqlite:Autoincrement", true); // Dodana adnotacja

                entity.Property(e => e.Isbn13)
                      .IsRequired()
                      .HasMaxLength(13)
                      .HasColumnName("isbn13");

                entity.Property(e => e.LanguageId)
                      .HasColumnName("language_id");

                entity.Property(e => e.NumPages)
                      .IsRequired()
                      .HasColumnName("num_pages");

                entity.Property(e => e.PublicationDate)
                      .HasColumnType("DATE")
                      .HasColumnName("publication_date");

                entity.Property(e => e.PublisherId)
                      .HasColumnName("publisher_id");

                entity.Property(e => e.Title)
                      .IsRequired()
                      .HasMaxLength(200)
                      .HasColumnName("title");

                entity.HasOne(d => d.Language)
                      .WithMany(p => p.Books)
                      .HasForeignKey(d => d.LanguageId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_book_book_language");

                entity.HasOne(d => d.Publisher)
                      .WithMany(p => p.Books)
                      .HasForeignKey(d => d.PublisherId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_book_publisher");

                // Konfiguracja relacji wiele-do-wielu z Author
                entity.HasMany(b => b.Authors)
                      .WithMany(a => a.Books)
                      .UsingEntity<Dictionary<string, object>>(
                          "book_author",
                          j => j
                              .HasOne<Author>()
                              .WithMany()
                              .HasForeignKey("author_id")
                              .OnDelete(DeleteBehavior.Cascade)
                              .HasConstraintName("FK_book_author_author"),
                          j => j
                              .HasOne<Book>()
                              .WithMany()
                              .HasForeignKey("book_id")
                              .OnDelete(DeleteBehavior.Cascade)
                              .HasConstraintName("FK_book_author_book"),
                          j =>
                          {
                              j.HasKey("book_id", "author_id");
                              j.ToTable("book_author");
                              j.IndexerProperty<int>("book_id").HasColumnName("book_id");
                              j.IndexerProperty<int>("author_id").HasColumnName("author_id");
                          });
            });

            // Konfiguracja encji BookLanguage
            modelBuilder.Entity<BookLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                      .HasName("PK_book_language");

                entity.ToTable("book_language");

                entity.Property(e => e.LanguageId)
                      .ValueGeneratedNever()
                      .HasColumnName("language_id");

                entity.Property(e => e.LanguageCode)
                      .IsRequired()
                      .HasMaxLength(10)
                      .HasColumnName("language_code");

                entity.Property(e => e.LanguageName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("language_name");

                // Nawigacja do Book
                entity.HasMany(l => l.Books)
                      .WithOne(b => b.Language)
                      .HasForeignKey(b => b.LanguageId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_book_language_book");
            });

            // Konfiguracja encji Country
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasKey(e => e.CountryId)
                      .HasName("PK_country");

                entity.Property(e => e.CountryId)
                      .ValueGeneratedNever()
                      .HasColumnName("country_id");

                entity.Property(e => e.CountryName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("country_name");
            });

            // Konfiguracja encji CustOrder
            modelBuilder.Entity<CustOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                      .HasName("PK_cust_order");

                entity.ToTable("cust_order");

                entity.Property(e => e.OrderId)
                      .ValueGeneratedNever()
                      .HasColumnName("order_id");

                entity.Property(e => e.CustomerId)
                      .HasColumnName("customer_id");

                entity.Property(e => e.DestAddressId)
                      .HasColumnName("dest_address_id");

                entity.Property(e => e.OrderDate)
                      .HasColumnType("DATETIME")
                      .HasColumnName("order_date");

                entity.Property(e => e.ShippingMethodId)
                      .HasColumnName("shipping_method_id");

                entity.HasOne(d => d.Customer)
                      .WithMany(p => p.CustOrders)
                      .HasForeignKey(d => d.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_cust_order_customer");

                entity.HasOne(d => d.DestAddress)
                      .WithMany(p => p.CustOrders)
                      .HasForeignKey(d => d.DestAddressId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_cust_order_address");

                entity.HasOne(d => d.ShippingMethod)
                      .WithMany(p => p.CustOrders)
                      .HasForeignKey(d => d.ShippingMethodId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_cust_order_shipping_method");
            });

            // Konfiguracja encji Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customer");

                entity.HasKey(e => e.CustomerId)
                      .HasName("PK_customer");

                entity.Property(e => e.CustomerId)
                      .ValueGeneratedNever()
                      .HasColumnName("customer_id");

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("email");

                entity.Property(e => e.FirstName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("last_name");
            });

            // Konfiguracja encji CustomerAddress
            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasKey(e => new { e.CustomerId, e.AddressId })
                      .HasName("PK_customer_address");

                entity.ToTable("customer_address");

                entity.Property(e => e.CustomerId)
                      .HasColumnName("customer_id");

                entity.Property(e => e.AddressId)
                      .HasColumnName("address_id");

                entity.Property(e => e.StatusId)
                      .HasColumnName("status_id");

                entity.HasOne(d => d.Address)
                      .WithMany(p => p.CustomerAddresses)
                      .HasForeignKey(d => d.AddressId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_customer_address_address");

                entity.HasOne(d => d.Customer)
                      .WithMany(p => p.CustomerAddresses)
                      .HasForeignKey(d => d.CustomerId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_customer_address_customer");

                entity.HasOne(d => d.Status)
                      .WithMany(p => p.CustomerAddresses)
                      .HasForeignKey(d => d.StatusId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_customer_address_address_status");
            });

            // Konfiguracja encji OrderHistory
            modelBuilder.Entity<OrderHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId)
                      .HasName("PK_order_history");

                entity.ToTable("order_history");

                entity.Property(e => e.HistoryId)
                      .ValueGeneratedNever()
                      .HasColumnName("history_id");

                entity.Property(e => e.OrderId)
                      .HasColumnName("order_id");

                entity.Property(e => e.StatusDate)
                      .HasColumnType("DATETIME")
                      .HasColumnName("status_date");

                entity.Property(e => e.StatusId)
                      .HasColumnName("status_id");

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.OrderHistories)
                      .HasForeignKey(d => d.OrderId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_order_history_cust_order");

                entity.HasOne(d => d.Status)
                      .WithMany(p => p.OrderHistories)
                      .HasForeignKey(d => d.StatusId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_order_history_order_status");
            });

            // Konfiguracja encji OrderLine
            modelBuilder.Entity<OrderLine>(entity =>
            {
                entity.HasKey(e => e.LineId)
                      .HasName("PK_order_line");

                entity.ToTable("order_line");

                entity.Property(e => e.LineId)
                      .ValueGeneratedNever()
                      .HasColumnName("line_id");

                entity.Property(e => e.BookId)
                      .HasColumnName("book_id");

                entity.Property(e => e.OrderId)
                      .HasColumnName("order_id");

                entity.Property(e => e.Price)
                      .HasColumnType("DECIMAL(18,2)")
                      .HasColumnName("price");

                entity.HasOne(d => d.Book)
                      .WithMany(p => p.OrderLines)
                      .HasForeignKey(d => d.BookId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_order_line_book");

                entity.HasOne(d => d.Order)
                      .WithMany(p => p.OrderLines)
                      .HasForeignKey(d => d.OrderId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_order_line_cust_order");
            });

            // Konfiguracja encji OrderStatus
            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                      .HasName("PK_order_status");

                entity.ToTable("order_status");

                entity.Property(e => e.StatusId)
                      .ValueGeneratedNever()
                      .HasColumnName("status_id");

                entity.Property(e => e.StatusValue)
                      .IsRequired()
                      .HasMaxLength(50)
                      .HasColumnName("status_value");
            });

            // Konfiguracja encji Publisher
            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.ToTable("publisher");

                entity.HasKey(e => e.PublisherId)
                      .HasName("PK_publisher");

                entity.Property(e => e.PublisherId)
                      .ValueGeneratedNever()
                      .HasColumnName("publisher_id");

                entity.Property(e => e.PublisherName)
                      .IsRequired()
                      .HasMaxLength(200)
                      .HasColumnName("publisher_name");

                // Nawigacja do Book
                entity.HasMany(p => p.Books)
                      .WithOne(b => b.Publisher)
                      .HasForeignKey(b => b.PublisherId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_publisher_book");
            });

            // Konfiguracja encji ShippingMethod
            modelBuilder.Entity<ShippingMethod>(entity =>
            {
                entity.HasKey(e => e.MethodId)
                      .HasName("PK_shipping_method");

                entity.ToTable("shipping_method");

                entity.Property(e => e.MethodId)
                      .ValueGeneratedNever()
                      .HasColumnName("method_id");

                entity.Property(e => e.Cost)
                      .HasColumnType("DECIMAL(18,2)")
                      .HasColumnName("cost");

                entity.Property(e => e.MethodName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("method_name");

                // Nawigacja do CustOrder
                entity.HasMany(s => s.CustOrders)
                      .WithOne(o => o.ShippingMethod)
                      .HasForeignKey(o => o.ShippingMethodId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_shipping_method_cust_order");
            });

            // Konfiguracja encji BookLanguage
            modelBuilder.Entity<BookLanguage>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                      .HasName("PK_book_language");

                entity.ToTable("book_language");

                entity.Property(e => e.LanguageId)
                      .ValueGeneratedNever()
                      .HasColumnName("language_id");

                entity.Property(e => e.LanguageCode)
                      .IsRequired()
                      .HasMaxLength(10)
                      .HasColumnName("language_code");

                entity.Property(e => e.LanguageName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("language_name");

                // Nawigacja do Book
                entity.HasMany(l => l.Books)
                      .WithOne(b => b.Language)
                      .HasForeignKey(b => b.LanguageId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_book_language_book");
            });

            // Konfiguracja encji Country
            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasKey(e => e.CountryId)
                      .HasName("PK_country");

                entity.Property(e => e.CountryId)
                      .ValueGeneratedNever()
                      .HasColumnName("country_id");

                entity.Property(e => e.CountryName)
                      .IsRequired()
                      .HasMaxLength(100)
                      .HasColumnName("country_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
