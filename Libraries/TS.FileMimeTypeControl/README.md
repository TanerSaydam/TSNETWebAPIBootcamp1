# File Mime Type Control NuGet Package

Bu paket ile requesten ald���n�z **IFormFile** dosyalar�n� jpg ve png i�in type kontrol�ne tabi tutabilirsiniz. Kontrol� byte �zerinden yapar ve e�er ba�ar�s�z olursa **false** d�nerir.


## Usage
```csharp
bool checkfileForJpg = file.CheckForJpg(); //ba�ar�s�z ise false d�ner 
bool checkfileForPng = file.CheckForPng(); //ba�ar�s�z ise false d�ner
```


## Resource Code
```chsarp
public static class ExtensionMethods
{
    public static bool CheckForJpg(this IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            byte[] fileBytes = stream.ToArray();
            string jpgValue = fileBytes[0].ToString() + fileBytes[1].ToString() + fileBytes[2].ToString();


            if (jpgValue != "255216255")
            {
                return false;
            }
        }
        return true;
    }

    public static bool CheckForPng(this IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            byte[] fileBytes = stream.ToArray();
            string jpgValue = 
                fileBytes[0].ToString() + 
                fileBytes[1].ToString() + 
                fileBytes[2].ToString() + 
                fileBytes[3].ToString();


            if (jpgValue != "137807871")
            {
                return false;
            }
        }
        return true;
    }
}

```