namespace First.WEBAPI;

public sealed class Test
{
    public Test()
    {
        HttpContextAccessor httpContextAccessor = new();
        var context = httpContextAccessor.HttpContext;
    }
}
