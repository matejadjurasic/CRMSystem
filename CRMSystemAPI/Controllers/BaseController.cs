using CRMSystemAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CRMSystemAPI.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        protected int CurrentUserId
        {
            get
            {
                if (int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userId))
                {
                    return userId;
                }
                throw new NotFoundException("User not found.");
            }
        }
    }
}
