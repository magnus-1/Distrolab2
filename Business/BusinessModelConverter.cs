
using System.Collections.Generic;
using community.Models.BusinessModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;

namespace community.Business {
    public class BusinessModelConverter {
        public static List<MessageBL> ConvertMessageListVM (List<MessageVM> msgList) {
            List<MessageBL> bl =  new List<MessageBL>();
            msgList.ForEach(p =>bl.add(new MessageBL{Title = p.Title , Content = p.Content});
            System.Console.WriteLine("ConvertMessageListVM: " + bl.ToString());
            return bl;
        }

        public static MessageBL ConvertMessageVM (MessageVM msg) {
            return new MessageBL{Title = msg.Title , Content = msg.Content};
        }
        static GroupBL ConvertGroupVM(GroupVM groupVM) {
            return new GroupBL{Id = groupVM.GroupId, Title = groupVM.Title , Messages = ConvertMessageListVM(groupVM.Messages) };
        }
    }
}