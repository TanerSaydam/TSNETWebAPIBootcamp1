using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    //private readonly IConfiguration _configuration;

    //public ApplicationDbContext(IConfiguration configuration)
    //{
    //    _configuration = configuration;
    //}

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        //connection yöntemi 2   => önerilen best practices bu     
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{//connection yöntemi 1
    // // optionsBuilder.UseSqlServer("Data Source=TANER\\SQLEXPRESS;Initial Catalog=eCommerceDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    //    optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServer"));
    //    optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    //}

    //public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<Product>().ToTable("Products");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        //best practices de buradan ayar yapmamız lazım. DataAnnotation önerilmez!

        //modelBuilder.Entity<Product>().Property(p => p.Name).HasColumnType("varchar(50)");
        //modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique(true);
        //modelBuilder.Entity<Product>(build => //bu sadece code first de geçerli
        //{
        //    build.Property(s => s.Name).HasColumnType("varchar(50)");
        //    build.HasIndex(x => x.Name).IsUnique();
        //    build.Property(s => s.Price).HasColumnType("money");
        //});

        //modelBuilder.Entity<Product>(builder =>
        //{
        //    builder.HasCheckConstraint("CK_Product_Quantity_Range", "[Quantity] BETWEEN 1 AND 50000");
        //    builder.HasCheckConstraint("CK_Product_Name_MinLength", "LEN([Name]) >= 3");
        //});

        //modelBuilder.Entity<Product>().HasKey(x => x.MyId);

        //modelBuilder.Entity<Product>().ToTable("Products", entity =>
        //{
        //    entity.HasCheckConstraint("CK_Product_Quantity_Range", "[Quantity] BETWEEN 1 AND 50000");
        //    entity.HasCheckConstraint("CK_Product_Name_MinLength", "LEN([Name]) >= 3");
        //});
    }

}


//Öncelikle DbContext'den inherit edilmiş bir class oluşturuyoruz
//Class içerisine connection bilgimizi yazıyoruz
//Database'deki tablelarımızı DbSet classı ile Context classımıza bağlıyoruz