using System;
using System.Collections.Generic;
using community.ListUtils;
using community.Models.BusinessModels;
using community.Models.DBModels;

namespace community.DBLayer
{

    public class DBModelConverter
    {   
        /**
        * converts a MessageBL to MessageDB
        */
        public static MessageDB ConvertMessageBL(MessageBL msg)
        {
            return new MessageDB {Title = msg.Title, Content = msg.Content, Sender = msg.Sender, IsRead = msg.IsRead, IsDeleted = msg.IsDeleted };
        }

        /**
        * converts a MessageDB to MessageBL
        */
        public static MessageBL ConvertToMessageBL(MessageDB msg)
        {
            return new MessageBL
            {
                Id = msg.Id,
                Title = msg.Title,
                IsRead = msg.IsRead,
                IsDeleted = msg.IsDeleted,
                Content = msg.Content,
                Sender = msg.Sender,
                Receiver = msg.Receiver,
                TimeStamp = msg.TimeStamp
            };
        }
        /**
        * converts a GroupBL to GroupDB
        */
        public static GroupDB ConvertGroupBL(GroupBL groupBL)
        {
            return new GroupDB
            {
                Id = groupBL.Id,
                Title = groupBL.Title,
                Messages = ListConverter.Map(groupBL.Messages, m => new MessageDB {Title = m.Title, Content = m.Content })
            };
        }
        /**
        * converts a GroupDB to GroupBL
        */
        public static GroupBL ConvertToGroupBL(GroupDB groupDB)
        {

            return new GroupBL
            {
                Title = groupDB.Title,
                Id = groupDB.Id,
                Messages = ListConverter.Map(groupDB.Messages, m => ConvertToMessageBL(m))
                //Messages = ListConverter.Map(groupDB.Messages, m => new MessageBL { Content = m.Content, Sender = m.Sender })
            
            };
        }
        /**
        * converts a InboxDB to InboxBL
        */
        public static InboxBL ConvertToInboxBL(InboxDB inboxDB)
        {
            return new InboxBL
            {
                UserId = inboxDB.UserId,
                UnreadMessages = inboxDB.UnreadMessages,
                Sender = inboxDB.Sender
            };
        }


        /**
        * converts a list if InboxDB to InboxBL
        */
        public static List<InboxBL> ConvertListToInboxBL(List<InboxDB> inboxes)
        {   
            return ListConverter.Map(inboxes,i => new InboxBL {UserId = i.UserId,  UnreadMessages = i.UnreadMessages, Sender = i.Sender});
        }
        /**
        * converts a list if GroupDB to GroupBL
        */
        public static List<GroupBL> ConvertListToGroupBL(List<GroupDB> groups)
        {
            return ListConverter.Map(groups, g => new GroupBL { Title = g.Title, Id = g.Id });
        }
        /**
        * converts a list if MessageDB to MessageBL
        */
        internal static List<MessageBL> ListConvertToMessageBL(List<MessageDB> msg)
        {
            return ListConverter.Map(msg,m => ConvertToMessageBL(m));
        }

        internal static List<InboxBL> ConvertInboxStatisticsToInboxBL(List<UserSenderDB> stats)
        {
            return ListConverter.Map(stats, i => new InboxBL
            {
                UserId = i.UserId,
                UnreadMessages = i.UnreadMessages,
                DeletedMessages = i.DeletedMessages,
                TotalMessages = i.TotalMessages,
                Sender = i.Sender
            });
        }
    }
}