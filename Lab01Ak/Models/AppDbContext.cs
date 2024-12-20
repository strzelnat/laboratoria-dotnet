using Microsoft.EntityFrameworkCore;

namespace LaboratoriumASPNET.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { 
        get; 
        set;
    }
    private string DbPath { get; set; }
    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source = {DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Nowak",
                    PhoneNumber = "123123123",
                    BirthDate = new DateTime(1980, 1, 1),
                    Email = "ewa@wsei.edu.pl",
                    Created = DateTime.Now,
                }
            );
    }
}