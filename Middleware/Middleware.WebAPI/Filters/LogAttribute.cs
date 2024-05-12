using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Middleware.WebAPI.Filters;

//IAction Filter => 2 tane metodu var. Action metodumuz başlamadan önce bir bittikten sonra son kullanıcıya dönmeden önce araya girip işlem yapabiliyoruz. Body kısmında gönderilen ve alınan objeleri + httpcontext'i yakalıyor.

//IResultFilter => 2 tane metodu var. MVC projelerinde View oluşmadan önce ve View oluştuktan sonra araya girip işlem yapabiliyor

//IResouseFilter => Action filter'e benziyor ama body kısmında request'den gelen objeyi göremiyoruz. + IAction filterden metot başında önce çalışıyor, metot bitiminde en son çalışıyor

//IOrderedFilter => Filterların sırasını değiştirmemizi sağlayan bir property veriyor


public sealed class MyAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues value);
        if (string.IsNullOrEmpty(value) && value != "Taner Saydam")
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = 401;
            await context.HttpContext.Response.WriteAsync("You need authorization to do this");

            context.Result = new UnauthorizedResult();
        }
        //Console.WriteLine(value);
    }
}

//public sealed class Log1Attribute : Attribute, IActionFilter, IOrderedFilter
//{
//    public int Order => 2;

//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method tamamlandıktan sonra

//    }

//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method çalışmadan önce

//    }
//}


//public sealed class Log2Attribute : Attribute, IActionFilter, IOrderedFilter
//{
//    public int Order => 1;
//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method tamamlandıktan sonra

//    }

//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method çalışmadan önce

//    }
//}

//public sealed class LogFilter : IActionFilter
//{
//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method tamamlandıktan sonra

//    }

//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method çalışmadan önce

//    }
//}


//public sealed class LogAttribute : Attribute, IResourceFilter
//{
//    public void OnResourceExecuted(ResourceExecutedContext context)
//    {
//        //method tamamlandıktan sonra
//    }

//    public void OnResourceExecuting(ResourceExecutingContext context)
//    {
//        //method çalışmadan önce
//    }
//}


//public sealed class Log2Attribute : Attribute, IActionFilter
//{
//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method tamamlandıktan sonra

//    }

//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method çalışmadan önce

//    }
//}

//public sealed class LogAttribute : Attribute, IResultFilter //mvc için geçerli
//{
//    public void OnResultExecuted(ResultExecutedContext context)
//    {
//        //method tamamlandıktan sonra OnResultExecuting'in arkasına bura çalışır
//        //MVC view oluştuktan sonra
//    }

//    public void OnResultExecuting(ResultExecutingContext context)
//    {
//        //method tamamlandıktan sonra önce bura çalışır
//        //MVC view oluşmadan önce
//    }
//}


//public sealed class LogAttribute : Attribute, IAsyncActionFilter
//{
//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        await Task.CompletedTask;
//    }
//}

//public sealed class LogAttribute : Attribute, IActionFilter
//{
//    public void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method tamamlandıktan sonra

//    }

//    public void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method çalışmadan önce

//    }
//}


//public sealed class LogAttribute : ActionFilterAttribute
//{
//    public override void OnActionExecuting(ActionExecutingContext context)
//    {
//        //method başlamadan önce
//        base.OnActionExecuting(context);
//    }

//    public override void OnActionExecuted(ActionExecutedContext context)
//    {
//        //method bittikten sonra
//        base.OnActionExecuted(context);
//    }
//} 