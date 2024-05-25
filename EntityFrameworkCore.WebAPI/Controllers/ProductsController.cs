using EntityFrameworkCore.WebAPI.Context;
using EntityFrameworkCore.WebAPI.DTOs;
using EntityFrameworkCore.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WebAPI.Controllers;
[Route("api/[controller]/[action]")]
[ApiController]
public sealed class ProductsController(ApplicationDbContext _context) : ControllerBase //primary constructor yöntemi
{
    //private readonly ApplicationDbContext _context;
    //public ProductsController(ApplicationDbContext context)
    //{
    //    #region Bu önerilmeyen kullanım
    //    //var builder = new DbContextOptionsBuilder();
    //    //builder.UseSqlServer("Connection String");
    //    //_context = new(builder.Options);
    //    #endregion

    //    #region Bu da önerilen yöntem
    //    _context = context;
    //    #endregion
    //}

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Product> products = _context.Set<Product>().ToList();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult Create(CreateProductDto request)
    {
        Product product = new()
        {
            Name = request.Name,
            Price = request.Price,
            Quantity = request.Quantity,
            CategoryName = request.CategoryName
        }; //auto mapper

        _context.Set<Product>().Add(product);
        _context.SaveChanges();

        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> CreateMethods(CancellationToken cancellationToken)
    {
        List<Product> products = new();
        Product product = new()
        {
            Name = "Domates",
            Price = 100
        };

        _context.Set<Product>().Add(product); //ekleme
        _context.Set<Product>().AddRange(product, product); //toplu ekleme
        _context.Set<Product>().AddRange(products); //toplu ekleme
        await _context.Set<Product>().AddAsync(product, cancellationToken); //async ekleme
        await _context.Set<Product>().AddRangeAsync(products, cancellationToken); //async toplu ekleme        

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateMethods(CancellationToken cancellationToken)
    {
        List<Product> products = new();
        Product product = new()
        {
            //Id = 1,
            Name = "Domates",
            Price = 100
        };

        _context.Set<Product>().Update(product); //güncelleme
        _context.Set<Product>().UpdateRange(product, product); //toplu güncelleme
        _context.Set<Product>().UpdateRange(products); //toplu güncelleme
        _context.Set<Product>()
          .Where(p => p.Price > 100)
          .ExecuteUpdate(p => p.SetProperty(
              product => product.Price,
              product => product.Price * 1.10m)); // Fiyatı %10 artır
        await _context.Set<Product>()
          .Where(p => p.Price > 100)
          .ExecuteUpdateAsync(p => p.SetProperty(
              product => product.Price,
              product => product.Price * 1.10m), cancellationToken); // async Fiyatı %10 artır           

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> DeleteMethods(CancellationToken cancellationToken)
    {
        List<Product> products = new();
        Product product = new()
        {
            //Id = 1,
            Name = "Domates",
            Price = 100
        };

        _context.Set<Product>().Remove(product); //silme
        _context.Set<Product>().RemoveRange(product, product); //silme
        _context.Set<Product>().RemoveRange(products); //silme
        _context.Set<Product>()
          .Where(p => p.Price > 100)
          .ExecuteDelete(); // Fiyatı 100 den büyük olanları sil
        await _context.Set<Product>().ExecuteDeleteAsync(cancellationToken); //async tüm kayıtları siler

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> ReadMethods(CancellationToken cancellationToken = default)
    {
        List<Product> products = _context.Set<Product>().ToList(); //ürün listesini verir
        products = await _context.Set<Product>().ToListAsync(cancellationToken); //ürün listesini verir

        products = _context.Set<Product>().Where(p => p.Name.Contains("o")).ToList(); //sorgudaki şartı sağlayan tüm ürün listesini verir

        HashSet<string> hashSetProducts =
            _context.Set<Product>()
            .Select(s => s.Name)
            .ToHashSet(); //select ile seçilen değerlerden unique olanları tespit edip getirir

        var objectProducts =
            _context.Set<Product>()
            .Select(s => new { Name = s.Name, Price = s.Price * 1.1m })
            .ToList(); //işlenmiş özel listeleri select ile dönebiliyorsunuz

        List<ProductDto> productDtos = _context.Set<Product>().Select(s => new ProductDto
        {
            Name = s.Name,
            Price = s.Price,
        }).ToList(); // product listesinin işlenerek ProductDto'ya dönüştürülmüş halini verir

        List<RecordProductDto> recordProductDtos =
            _context.Set<Product>()
            .Select(s => new RecordProductDto(s.Name, s.Price))
            .ToList(); // product listesinin işlenerek ProductDto'ya dönüştürülmüş halini verir

        Product product = _context.Set<Product>().First(); //ilk productı bulur bulamazsa hata fırlatır

        Product? nullableProduct = _context.Set<Product>().FirstOrDefault(); //ilk productı bulur, bulamazsa null dönderir
        nullableProduct = await _context.Set<Product>().FirstOrDefaultAsync(cancellationToken); //ilk productı bulur, bulamazsa null dönderir

        nullableProduct =
            _context.Set<Product>()
            .Where(p => (p.Price > 100 && p.Name == "Domates") || p.Price == 102)
            .FirstOrDefault(); //ilk productı bulur, bulamazsa null dönderir

        nullableProduct =
            _context.Set<Product>()
            .Where(p => p.Name.ToLower().Contains("n".ToLower()))
            .FirstOrDefault(); //aradığım şarta göre ilk productı bulur, bulamazsa null dönderir

        product =
            _context.Set<Product>()
            .Where(p => p.Name == "Domates")
            .Single(); //ilk kaydı bulur ama aynı kayıttan bir tane daha varsa hata fırlatır kaydı bulamazsa da hata fırlatır

        nullableProduct =
            _context.Set<Product>()
            .Where(p => p.Name == "Domates")
            .SingleOrDefault(); //ilk productı bulur, bulamazsa null dönderir

        nullableProduct = await _context.Set<Product>().Where(p => p.Name == "Domates").SingleOrDefaultAsync(cancellationToken);

        // product =
        //   _context.Set<Product>()
        // .Where(p => p.Price > 100).Select(s => s.Name)
        //.ToList()
        //.Select(s => new Product() { Id = 0, Name = s, Price = 100 })
        //.First(); //mix lenmiş sorgu örneği

        decimal maxPrice = _context.Set<Product>().Max(p => p.Price);
        maxPrice = await _context.Set<Product>().MaxAsync(p => p.Price, cancellationToken);
        decimal minPrice = _context.Set<Product>().Min(p => p.Price);
        minPrice = await _context.Set<Product>().MinAsync(p => p.Price, cancellationToken);

        products = _context.Set<Product>().ToList();

        var maxBy = products.MaxBy(p => p.Price); //bu sadece memory de liste varsa çalışır. Db de yani EFCore Query olarak çalışmıyor
        var minBy = products.MinBy(p => p.Price); //bu sadece memory de liste varsa çalışır. Db de yani EFCore Query olarak çalışmıyor

        hashSetProducts = _context.Set<Product>().Select(s => s.CategoryName).OrderBy(p => p).ToHashSet();

        var result = _context.Set<Product>()
            .GroupBy(p => p.CategoryName)
            .Select(s => new
            {
                CategoryName = s.Key,
                TotalPrice = s.Sum(s => s.Price)
            })
            .OrderBy(p => p.CategoryName)
            .ToList();


        var resultFrom = (from p in _context.Set<Product>()
                          select new
                          {
                              Name2 = p.Name,
                              Price2 = p.Price * 1.10m
                          }).ToList();


        result.Where(p => p.CategoryName == "Sebze");

        //List
        //IEnumerable
        //HashSet
        //IList
        IQueryable<Product> query = _context.Set<Product>();

        query = query.Where(p => p.Price > 50 || p.Price < 150);
        query = query.Where(p => p.CategoryName.Contains("S"));

        var list = await query.ToListAsync(cancellationToken);

        //ToList, ToHashSet, First, FirstOrDefault, Single, SingleOrdefault, Max, Min => bunar execute komutudur. Ve bunları yazıldığında veri artık Db'den memory'e alınır.

        //IQuerable ya da AsQuerable da ise sorgu execute edilene kadar DB'de devam eder.


        products = _context.Set<Product>().ToList();
        products = products.Where(p => p.Price > 50).ToList();
        products = products.Where(p => p.CategoryName.Contains("S")).ToList();

        bool isExists =
            _context.Set<Product>()
            .Any(p => p.Name == "Domates"); //varsa true yoksa false

        isExists =
            await _context.Set<Product>()
            .AnyAsync(cancellationToken); //herhangi bir  kayıt varsa true yoksa false

        return NoContent(); //IQuerable
    }
}

/*
Async methodlarda Cancellation token göndermek best practicesdir. 
Gönderemediğimiz durumda "default" keywordü ya da "CancellationToken.None" verilebilir. 
*/

public sealed class ProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}

public sealed record RecordProductDto(string Name, decimal Price);