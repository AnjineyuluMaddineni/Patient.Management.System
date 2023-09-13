using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PMS.Web.Data;
using PMS.Web.Entities;
using PMS.Web.Models;
using PMS.WEB.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PMS.Web.Services
{
    public class InboxService : IInboxService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public InboxService(ApplicationDbContext context, IMapper mapper/*, IHubContext<BroadcastHub, IHubClient> hubContext*/)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddMessage(MessageModel newMessage)
        {
            Message message = _mapper.Map<Message>(newMessage);
            await _context.Messages.AddAsync(message);
            Notification notification = new ()
            {
                AppUserId = message.ToAppUserId,
                Message = message.MessageBody,
                TranType = "Receive",
                IsNew=true
            };
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<int> AddReplyMessage(ReplyMessageModel newReplyMessage)
        {
            ReplyMessage replyMessage = _mapper.Map<ReplyMessage>(newReplyMessage);
            _context.ReplyMessages.Update(replyMessage);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MessageModel>> GetMessages(string userId, string command)
        {
            List<MessageModel> messagesModel = new();
            switch (command)
            {
                case "sent":
                    List<Message> messages = await _context.Messages.Include(s=>s.ReplyMessage).Where(s => s.FromAppUserId == userId).
                    OrderByDescending(s => s.UpdatedDate).ToListAsync();
                    foreach (var message in messages)
                    {
                        message.FromName = await _context.AppUsers.Where(s => s.Id == message.FromAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                        message.ToName = await _context.AppUsers.Where(s => s.Id == message.ToAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                        message.CurrentAppUserId = userId;
                        if(message.ReplyMessage.Count>0)
                        {
                            foreach(var replyMsg in message.ReplyMessage)
                            {
                                replyMsg.FromName = await _context.AppUsers.Where(s => s.Id == replyMsg.FromAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                                replyMsg.ToName = await _context.AppUsers.Where(s => s.Id == replyMsg.ToAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                            }
                        }
                        messagesModel.Add(_mapper.Map<MessageModel>(message));
                    }
                    return messagesModel;
                case "recieved":
                    List<Message> recievedMessages = await _context.Messages.Include(s => s.ReplyMessage).Where(s => s.ToAppUserId == userId).
                    OrderByDescending(s => s.UpdatedDate).ToListAsync();
                    foreach (var message in recievedMessages)
                    {
                        message.FromName = await _context.AppUsers.Where(s => s.Id == message.FromAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                        message.ToName = await _context.AppUsers.Where(s => s.Id == message.ToAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                        message.CurrentAppUserId = userId;
                        if (message.ReplyMessage.Count > 0)
                        {
                            foreach (var replyMsg in message.ReplyMessage)
                            {
                                replyMsg.FromName = await _context.AppUsers.Where(s => s.Id == replyMsg.FromAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                                replyMsg.ToName = await _context.AppUsers.Where(s => s.Id == replyMsg.ToAppUserId).Select(s => s.UserName).FirstOrDefaultAsync();
                                
                            }
                        }
                        messagesModel.Add(_mapper.Map<MessageModel>(message));
                        
                    }
                    return messagesModel;
            }

            return null;
        }       
    }
}
