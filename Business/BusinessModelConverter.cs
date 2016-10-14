
using System.Collections.Generic;
using community.Models.BusinessModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;

namespace community.Business {
    public class BusinessModelConverter {
        public static List<MessageBL> ConvertMessageListVM (List<MessageVM> msgList) {
            
            List<MessageBL> bl =  new List<MessageBL>();

            msgList.ForEach(p => bl.Add(new MessageBL{ Content = p.Content}));

            System.Console.WriteLine("ConvertMessageListVM: " + bl.ToString());
            return bl;
        }
        
        public static List<MessageVM> ConvertToMessageListVM (List<MessageBL> msgList) {
            List<MessageVM> bl =  new List<MessageVM>();

            msgList.ForEach(p => bl.Add(new MessageVM{ Content = p.Content}));

            System.Console.WriteLine("ConvertMessageListVM: " + bl.ToString());
            return bl;
        }

        public static MessageBL ConvertMessageVM(MessageVM msg) {
            return new MessageBL{ Content = msg.Content};
        }
        public static GroupBL ConvertGroupVM(GroupVM groupVM) {
            return new GroupBL{Id = groupVM.GroupId, Title = groupVM.Title , Messages = ConvertMessageListVM(groupVM.Messages) };
        }

        public static GroupVM ConvertToGroupVM(GroupBL groupBL) {
            return new GroupVM{GroupId = groupBL.Id, Title = groupBL.Title , Messages = ConvertToMessageListVM(groupBL.Messages) };
        }
    }
}