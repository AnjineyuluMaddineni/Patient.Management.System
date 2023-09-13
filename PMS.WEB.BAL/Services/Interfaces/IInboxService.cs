using PMS.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public interface IInboxService
    {
        Task<int> AddMessage(MessageModel newMessage);
        Task<int> AddReplyMessage(ReplyMessageModel newReplyMessage);
        Task<IEnumerable<MessageModel>> GetMessages(string userId,string command);
    }
}
