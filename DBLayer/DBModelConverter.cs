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
<<<<<<< HEAD
            msgList.ForEach(p => dbl.Add(new MessageDB {  Content = p.Content }));
=======

            msgList.ForEach(p => dbl.Add(new MessageDB {  Content = p.Content }));
            
>>>>>>> 40eb7e4be8105e5b9c3ae4a1a713ec440c947d6b
            System.Console.WriteLine("ConvertMessageListBL: " + dbl.ToString());
            return dbl;
        }

        public static List<MessageBL> ConvertToMessageListBL(List<MessageDB> msgList)
        {
            List<MessageBL> dbl = new List<MessageBL>();

            msgList.ForEach(p => dbl.Add(new MessageBL {  Content = p.Content }));
            
            System.Console.WriteLine("ConvertToMessageListBL: " + dbl.ToString());
            return dbl;
        }

        public static MessageDB ConvertMessageBL(MessageBL msg)
        {
            return new MessageDB {  Content = msg.Content };
        }
        public static GroupDB ConvertGroupBL(GroupBL groupBL)
        {
            return new GroupDB { Id = groupBL.Id, Title = groupBL.Title, Messages = ConvertMessageListBL(groupBL.Messages) };
        }

        public static GroupBL ConvertToGroupBL(GroupDB groupDB) {
            return new GroupBL{Title = groupDB.Title, Id = groupDB.Id, Messages = ConvertToMessageListBL(groupDB.Messages)};
        }
        
    }
}