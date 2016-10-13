using System.Collections.Generic;
using community.Models.BusinessModels;
using community.Models.DBModels;

namespace community.DBLayer
{

    public class DBModelConverter
    {
        public static List<MessageDB> ConvertMessageListBL(List<MessageBL> msgList)
        {
            List<MessageDB> dbl = new List<MessageDB>();
            msgList.ForEach(p => dbl.add(new MessageDB { Title = p.Title, Content = p.Content });
            System.Console.WriteLine("ConvertMessageListBL: " + dbl.ToString());
            return dbl;
        }

        public static MessageDB ConvertMessageBL(MessageBL msg)
        {
            return new MessageDB { Title = msg.Title, Content = msg.Content };
        }
        static GroupDB ConvertGroupBL(GroupBL groupBL)
        {
            return new GroupDB { Id = groupBL.Id, Title = groupBL.Title, Messages = ConvertMessageListBL(groupBL.Messages) };
        }
    }
}