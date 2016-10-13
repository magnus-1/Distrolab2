
using System.Collections.Generic;
using community.Models.BusinessModels;
using community.Models.ViewModels;
using community.Models.ViewModels.GroupViewModels;

namespace community.Business {
    public class BusinessModelConverter {
        public static List<MessageBL> ConvertMessageListVM (List<MessageVM> msgList) {
            List<MessageBL> bl =  new List<MessageBL>();
<<<<<<< HEAD
            msgList.ForEach(p =>bl.add(new MessageBL{Title = p.Title , Content = p.Content}));
=======
            msgList.ForEach(p => bl.Add(new MessageBL{ Content = p.Content}));
>>>>>>> 1e663b49b21b62738e138b192f57ed4732e4ea87
            System.Console.WriteLine("ConvertMessageListVM: " + bl.ToString());
            return bl;
        }

        public static MessageBL ConvertMessageVM(MessageVM msg) {
            return new MessageBL{ Content = msg.Content};
        }
        public static GroupBL ConvertGroupVM(GroupVM groupVM) {
            return new GroupBL{Id = groupVM.GroupId, Title = groupVM.Title , Messages = ConvertMessageListVM(groupVM.Messages) };
        }
    }
}