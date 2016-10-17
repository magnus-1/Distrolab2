using System;
using System.Collections.Generic;
using community.ListUtils;
using community.Models.BusinessModels;
using community.Models.DBModels;

namespace community.DBLayer
{

    public class DBModelConverter
    {
        public static MessageDB ConvertMessageBL(MessageBL msg)
        {
            return new MessageDB { Content = msg.Content, Sender = msg.Sender, IsRead = msg.IsRead, IsDeleted = msg.IsDeleted };
        }

        public static MessageBL ConvertToMessageBL(MessageDB msg)
        {
            return new MessageBL
            {
                Id = msg.Id,
                IsRead = msg.IsRead,
                IsDeleted = msg.IsDeleted,
                Content = msg.Content,
                Sender = msg.Sender
            };
        }
        public static GroupDB ConvertGroupBL(GroupBL groupBL)
        {
            return new GroupDB
            {
                Id = groupBL.Id,
                Title = groupBL.Title,
                Messages = ListConverter.Map(groupBL.Messages, m => new MessageDB { Content = m.Content })
            };
        }

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
        public static List<GroupBL> ConvertListToGroupBL(List<GroupDB> groups)
        {
            return ListConverter.Map(groups, g => new GroupBL { Title = g.Title, Id = g.Id });
        }

        internal static List<MessageBL> ListConvertToMessageBL(List<MessageDB> msg)
        {
            return ListConverter.Map(msg,m => ConvertToMessageBL(m));
        }
    }
}