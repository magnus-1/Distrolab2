
using System.Collections.Generic;
using community.ListUtils;
using community.Models.BusinessModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;

namespace community.Business
{
    public class BusinessModelConverter
    {
        public static List<T> Reverse<T>(List<T> list)
        {
            list.Reverse();
            return list;
        }

        public static MessageBL ConvertMessageVM(MessageVM msg)
        {
            return new MessageBL { Content = msg.Content };
        }
        public static GroupBL ConvertGroupVM(GroupVM groupVM)
        {
            return new GroupBL
            {
                Id = groupVM.GroupId,
                Title = groupVM.Title,
                Messages = Reverse(ListConverter.Map(groupVM.Messages, m => new MessageBL { Content = m.Content }))
            };
        }

        public static GroupVM ConvertToGroupVM(GroupBL groupBL)
        {
            return new GroupVM
            {
                GroupId = groupBL.Id,
                Title = groupBL.Title,
                Messages = Reverse(ListConverter.Map(groupBL.Messages, m => new MessageVM { Content = m.Content }))
            };
        }
    }
}