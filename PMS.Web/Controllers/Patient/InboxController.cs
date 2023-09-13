using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PMS.Web.Configuration;
using PMS.Web.Models;
using PMS.Web.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Controllers.Patient
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeRoles(UserRolesModels.Physician, UserRolesModels.Nurse)]
    public class InboxController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IInboxService _inboxService;

        public InboxController(IInboxService inboxService, IUserService userService)
        {
            _userService = userService;
            _inboxService = inboxService;
        }
        [HttpPost("{email}")]
        public async Task<ActionResult<PatientModel>> AddMessage(string email,[FromBody] MessageModel newMessage)
        {
            string appUserId = await _userService.GetAppUserId(email);
            newMessage.FromAppUserId = appUserId;
            newMessage.UpdatedDate = DateTime.Now;            
            if (appUserId != null)
            {
                var result = await _inboxService.AddMessage(newMessage);
                if (result>0)
                    return Ok(result);
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Message not added" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "App user id is null" });
        }
        [HttpPost("Reply/{email}")]
        public async Task<ActionResult<PatientModel>> AddReplyMessage(string email, [FromBody] ReplyMessageModel newMessage)
        {
            string appUserId = await _userService.GetAppUserId(email);
            newMessage.FromAppUserId = appUserId;
            newMessage.UpdatedDate = DateTime.Now;
            if (appUserId != null)
            {
                var result = await _inboxService.AddReplyMessage(newMessage);
                if (result > 0)
                    return Ok(result);
                else
                    return Ok(new { status = StatusCodes.Status400BadRequest, success = false, data = "Message not added" });
            }
            else
                return BadRequest(new { status = StatusCodes.Status400BadRequest, success = false, data = "App user id is null" });
        }
        [HttpGet("GetMessages/{command}/{email}")]
        public async Task<IEnumerable<MessageModel>> GetMessages(string command, string email)
        {
            string appUserId = await _userService.GetAppUserId(email);
            if (appUserId != null)
            {
                var result = await _inboxService.GetMessages(appUserId, command);
                if (result != null)
                    return result;
                else
                    return null;
            }
            else
                return null;
        }
    }
}
