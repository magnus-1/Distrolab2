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
            return new MessageDB { Content = msg.Content };
        }
        public static GroupDB ConvertGroupBL(GroupBL groupBL)
        {
            return new GroupDB
            {
                Id = groupBL.Id,
                Title = groupBL.Title,
                Messages = ListConverter.Map(groupBL.Messages, m => new MessageDB{Content = m.Content})
            };
        }

        public static GroupBL ConvertToGroupBL(GroupDB groupDB)
        {   

            return new GroupBL
            {
                Title = groupDB.Title,
                Id = groupDB.Id,
                Messages = ListConverter.Map(groupDB.Messages, m => new MessageBL{Content = m.Content})
            };
        }
        public static List<GroupBL> ConvertListToGroupBL(List<GroupDB> groups){
            return ListConverter.Map(groups,g => new GroupBL {Title = g.Title, Id = g.Id});
        }
    }
}