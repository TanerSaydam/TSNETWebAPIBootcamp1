namespace TaskManager.WebAPI.Utilities;

public static class ExtensionMethods
{
    public static string CreateFileName(this string fileName)
    {
        return string.Join("-",DateTime.Now.ToFileTime(),fileName);
    }
}
