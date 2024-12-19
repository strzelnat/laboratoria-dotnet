using Lab01Ak.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LaboratoriumASPNET.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<ContactEntity> Contacts { get; set; }
    public DbSet<OrganizationEntity> Organizations { get; set; }

    private string DbPath { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Ścieżka do bazy danych SQLite
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "contacts.db");
        optionsBuilder.UseSqlite($"Data source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Konfiguracja Address jako Owned Entity w OrganizationEntity
        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Address, a =>
            {
                a.Property(p => p.City).HasMaxLength(100); // Dodatkowe właściwości
                a.Property(p => p.Street).HasMaxLength(200);
            });

        
        // Relacja między ContactEntity a OrganizationEntity
        modelBuilder.Entity<ContactEntity>()
            .HasOne(c => c.Organization)
            .WithMany(o => o.Contacts)
            .HasForeignKey(c => c.OrganizationId);

        // Dodanie ról użytkowników
        string ADMIN_ID = Guid.NewGuid().ToString();
        string USER_ID = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = ADMIN_ID, Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = ADMIN_ID },
            new IdentityRole { Id = USER_ID, Name = "user", NormalizedName = "USER", ConcurrencyStamp = USER_ID }
        );

        // Dodanie użytkowników
        var admin = new IdentityUser
        {
            Id = ADMIN_ID,
            UserName = "Adam",
            NormalizedUserName = "ADAM",
            Email = "adam@wsei.edu.pl",
            NormalizedEmail = "ADAM@WSEI.EDU.PL",
            EmailConfirmed = true
        };

        var user = new IdentityUser
        {
            Id = USER_ID,
            UserName = "Kuba",
            NormalizedUserName = "KUBA",
            Email = "kuba@wsei.edu.pl",
            NormalizedEmail = "KUBA@WSEI.EDU.PL",
            EmailConfirmed = true
        };

        PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
        admin.PasswordHash = hasher.HashPassword(admin, "1234!");
        user.PasswordHash = hasher.HashPassword(user, "1234@");

        modelBuilder.Entity<IdentityUser>().HasData(admin, user);

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = ADMIN_ID, UserId = ADMIN_ID },
            new IdentityUserRole<string> { RoleId = USER_ID, UserId = USER_ID }
        );

        // Dodanie danych testowych dla organizacji i kontaktów (z Twoich danych)
        modelBuilder.Entity<OrganizationEntity>().HasData(
            new OrganizationEntity
            {
                Id = 1,
                Regon = "321321321",
                Nip = "123456",
                Name = "WSEI",
            },
            new OrganizationEntity
            {
                Id = 2,
                Regon = "123123123",
                Nip = "432432",
                Name = "Famo",
            }
        );

        modelBuilder.Entity<ContactEntity>().HasData(
            new ContactEntity
            {
                Id = 1,
                FirstName = "Adam",
                LastName = "Nowak",
                PhoneNumber = "123123123",
                BirthDate = new DateTime(1980, 1, 1),
                Email = "ewa@wsei.edu.pl",
                Created = DateTime.Now,
                OrganizationId = 1
            },
            new ContactEntity
            {
                Id = 2,
                FirstName = "Ola",
                LastName = "Nowak",
                PhoneNumber = "123123123",
                BirthDate = new DateTime(2001, 1, 1),
                Email = "ola@wsei.edu.pl",
                Created = DateTime.Now,
                OrganizationId = 2
            }
        );
    }
}