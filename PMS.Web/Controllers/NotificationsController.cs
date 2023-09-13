using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Services;
using PMS.WEB.BAL.Services.Interfaces;
using System.Threading.Tasks;

namespace PMS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotification _notification;
        private readonly IUserService _userService;
        public NotificationsController(INotification notification,IUserService userService)
        {
            _notification = notification;
            _userService = userService;
        }
        
        [HttpGet("notificationcount/{email}/{command}")]
        public async Task<IActionResult> GetNotificationCount(string email,string command)
        {
            string appUserId = await _userService.GetAppUserId(email);
            var result = await _notification.GetNotificationCount(appUserId,command);
            return Ok(new { status = StatusCodes.Status200OK, success = true, data = result });
        }

        
        [Authorize]
        [Route("notificationresult")]
        [HttpGet]
        public async Task<IActionResult> GetNotificationMessage()
        {
            var result = await _notification.GetNotificationMessage();
            return Ok(new { status = StatusCodes.Status200OK, success = true, data = result });
        }

    }
}
