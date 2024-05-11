using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceHomeWork.Abstractions;
[Route("api/[controller]/[action]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
}//
