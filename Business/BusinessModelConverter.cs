
using System;
using System.Collections.Generic;
using community.ListUtils;
using community.Models.BusinessModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;
using community.Models.ViewModels.ReadMessageViewModels;

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
        public static List<DestinationVM> ConvertToDestinationVM(List<DestinationBL> destsBL)
        {
            return ListConverter.Map(destsBL, m => new DestinationVM
            {
                isGroup = m.IsGroup,
                destinationId = m.Id,
                destinationName = m.Name
            });
        }

        public static GetMessageBodyVM ConvertToGetMessageBodyVM(MessageBodyBL msgBodyBL)
        {
            return new GetMessageBodyVM { id = msgBodyBL.Id, content = msgBodyBL.Content };
        }

        public static MessageBodyBL ConvertGetMessageBodyVM(GetMessageBodyVM msgBodyVM)
        {
            return new MessageBodyBL { Id = msgBodyVM.id, Content = msgBodyVM.content };
        }

        public static ReadMessageIndexVM ConvertToReadMessageIndexVM(List<MessageBL> msg)
        {
            return new ReadMessageIndexVM
            {
                messages = ListConverter.Map(msg,
                m => new ReadMessageVM
                {
                    id = m.Id,
                    isRead = m.IsRead,
                    title = "not implemented yet",
                    time = " time stamp now?",
                    from = m.Sender.UserName
                })
            };
            
        }

        public static GroupVM ConvertToGroupVM(GroupBL groupBL)
        {
            return new GroupVM
            {
                GroupId = groupBL.Id,
                Title = groupBL.Title,
                Messages = Reverse(ListConverter.Map(groupBL.Messages, m => new MessageVM { Content = m.Content, SenderName = (m.Sender != null) ? m.Sender.UserName : "Unknown" }))
            };
        }
        public static List<GroupInfoVM> ConvertListToGroupInfoVM(List<GroupBL> groups)
        {
            return ListConverter.Map(groups, g => new GroupInfoVM { Title = g.Title, GroupId = g.Id });
        }
    }
}