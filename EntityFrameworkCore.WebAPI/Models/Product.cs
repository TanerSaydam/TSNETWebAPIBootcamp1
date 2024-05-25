using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.WebAPI.Models;

[Index(nameof(Name), IsUnique = true)]
//[Index(nameof(CategoryName),IsUnique = true)]
//[Keyless]
public sealed class Product
{
    //  [Key]
    // best practices Id yazılmalıdır
    public int Id { get; set; }//EF Id kelimesini otomatik Identity Key
    //public int ProductId { get; set; }//EF tablo adı + Id kelimesini otomatik Identity Key    
    //public int MyId { get; set; }

    //[Column(TypeName = "varchar(50)", Order = 1)]
    //[MinLength(3, ErrorMessage = "Name en az 3 karakter olmalıdır")] //artık çalışmıyor
    public string Name { get; set; } = string.Empty;

    //[Column(TypeName = "varchar(50)", Order = 3)]
    public string CategoryName { get; set; } = string.Empty;

    //[Column(TypeName = "Money", Order = 2)]
    public decimal Price { get; set; }
    //[Range(1, 50000, ErrorMessage = "Quantity 1 ile 50000 arasında olmalıdır")]//artık çalışmıyor
    public int Quantity { get; set; }
}
//DBFirst 1. yöntemi