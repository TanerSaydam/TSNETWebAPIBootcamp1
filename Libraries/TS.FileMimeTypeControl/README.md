# File Mime Type Control NuGet Package

Bu paket ile requesten aldýðýnýz **IFormFile** dosyalarýný jpg ve png için type kontrolüne tabi tutabilirsiniz. Kontrolü byte üzerinden yapar ve eðer baþarýsýz olursa **false** dönerir.


## Usage
```csharp
bool checkfileForJpg = file.CheckForJpg(); //baþarýsýz ise false döner 
bool checkfileForPng = file.CheckForPng(); //baþarýsýz ise false döner
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