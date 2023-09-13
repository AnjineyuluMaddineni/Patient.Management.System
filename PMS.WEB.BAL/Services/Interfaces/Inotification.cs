using PMS.WEB.BAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.WEB.BAL.Services.Interfaces
{
    public interface INotification
    {
        Task<NotificationCountResult> GetNotificationCount(string id,string command);
        Task<List<NotificationResult>> GetNotificationMessage();
    }
}
