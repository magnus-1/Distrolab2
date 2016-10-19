
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
        /**
        * taes a list and reverse it
        */
        public static List<T> Reverse<T>(List<T> list)
        {
            list.Reverse();
            return list;
        }
        /**
        * converts MessageVM to MessageBL
        */
        public static MessageBL ConvertMessageVM(MessageVM msg)
        {
            return new MessageBL {Title = msg.Title, Content = msg.Content };
        }
        /**
        * converts GroupVM to GroupBL
        */
        public static GroupBL ConvertGroupVM(GroupVM groupVM)
        {
            return new GroupBL
            {
                Id = groupVM.GroupId,
                Title = groupVM.Title,
                Messages = Reverse(ListConverter.Map(groupVM.Messages, m => new MessageBL {Title = m.Title, Content = m.Content }))
            };
        }
        /**
        * converts DestinationBL to DestinationVM
        */
        public static List<DestinationVM> ConvertToDestinationVM(List<DestinationBL> destsBL)
        {
            return ListConverter.Map(destsBL, m => new DestinationVM
            {
                isGroup = m.IsGroup,
                destinationId = m.Id,
                destinationName = m.Name
            });
        }

        /**
        * converts MessageBodyBL to GetMessageBodyVM
        */
        public static GetMessageBodyVM ConvertToGetMessageBodyVM(MessageBodyBL msgBodyBL)
        {
            return new GetMessageBodyVM { id = msgBodyBL.Id, content = msgBodyBL.Content };
        }

        /**
        * converts GetMessageBodyVM to MessageBodyBL
        */
        public static MessageBodyBL ConvertGetMessageBodyVM(GetMessageBodyVM msgBodyVM)
        {
            return new MessageBodyBL { Id = msgBodyVM.id, Content = msgBodyVM.content };
        }

        /**
        * converts MessageBL to ReadMessageIndexVM
        */
        public static ReadMessageIndexVM ConvertToReadMessageIndexVM(List<MessageBL> msg)
        {
            return new ReadMessageIndexVM
            {
                messages = ListConverter.Map(msg,
                m => new ReadMessageVM
                {
                    id = m.Id,
                    isRead = m.IsRead,
                    title = m.Title,
                    time = m.TimeStamp.ToString(),
                    from = m.Sender.UserName
                })
            };
            
        }
        /**
        * converts groupBL to groupVM
        */
        public static GroupVM ConvertToGroupVM(GroupBL groupBL)
        {
            return new GroupVM
            {
                GroupId = groupBL.Id,
                Title = groupBL.Title,
                Messages = Reverse(ListConverter.Map(groupBL.Messages, m => new MessageVM {Title = m.Title, Content = m.Content, SenderName = (m.Sender != null) ? m.Sender.UserName : "Unknown" }))
            };
        }
        /**
        * converts list of groupBL to list of GroupInfoVM
        */
        public static List<GroupInfoVM> ConvertListToGroupInfoVM(List<GroupBL> groups)
        {
            return ListConverter.Map(groups, g => new GroupInfoVM { Title = g.Title, GroupId = g.Id });
        }
        /**
        * converts list of InboxBL to list of ReadInboxVM
        */
        internal static ReadInboxVM ConvertInboxListToInboxVM(List<InboxBL> inboxes)
        {
            ReadInboxVM inbox = new ReadInboxVM {};
            inbox.incomingFrom = ListConverter.Map(inboxes, f => new FromUser{senderId = f.UserId, senderName = f.Sender.UserName, recevedCount = f.UnreadMessages});
            return inbox;
        }
    }
}