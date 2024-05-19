using EntityFrameworkCore.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TANER\\SQLEXPRESS;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }

    public DbSet<Product> Products { get; set; }
}


//Öncelikle DbContext'den inherit edilmiş bir class oluşturuyoruz
//Class içerisine connection bilgimizi yazıyoruz
//Database'deki tablelarımızı DbSet classı ile Context classımıza bağlıyoruz