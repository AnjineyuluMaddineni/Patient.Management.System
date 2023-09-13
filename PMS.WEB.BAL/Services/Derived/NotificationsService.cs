using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.WEB.BAL.Models;
using PMS.WEB.BAL.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.WEB.BAL.Services.Derived
{
    public class NotificationsService : INotification
    {
        private readonly ApplicationDbContext _context;
        public NotificationsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<NotificationCountResult> GetNotificationCount(string id,string command)
        {           
            switch (command)
            {
                case "set":
                    var res = await _context.Notifications.Where(s => s.AppUserId == id).ToListAsync();
                    foreach(var notification in res)
                    {
                        notification.IsNew = false;
                        _context.Notifications.Update(notification);
                        await _context.SaveChangesAsync();
                    }
                    var count = (from notify in _context.Notifications select notify).Where(x => x.AppUserId == id && x.IsNew).CountAsync();
                    NotificationCountResult result = new();                    
                    result.Count = await count; 
                    return result;
                case "get":
                    var count1 = (from notify in _context.Notifications select notify).Where(x => x.AppUserId == id && x.IsNew).CountAsync();
                    NotificationCountResult result1 = new()
                    {
                        Count = await count1
                    };
                    return result1;
            }
            return null;
        }

        public async Task<List<NotificationResult>> GetNotificationMessage()
        {
            
            var result = from message in _context.Notifications
                         .OrderByDescending(x=>x.Id)                      
                         select new NotificationResult
                         {
                             Message = message.Message,
                             TranType = message.TranType
                         };
            return await result.ToListAsync();
        }
    }
}
