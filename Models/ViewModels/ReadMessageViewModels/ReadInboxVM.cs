using System.Collections.Generic;

namespace community.Models.ViewModels.ReadMessageViewModels
{

    public class ReadInboxVM
    {
        public List<FromUser> incomingFrom { get; set; }
        public int picked {get;set;}
        public int totalReceivedMessages { get; set; }
        public int totalReadMessages { get; set; }
        public int totalDeletedMessages { get; set; }

    }

    public class FromUser
    {
        public int senderId {get;set;}
        public string senderName {get;set;}
        public int recevedCount {get;set;}
        public int totalMessages {get;set;} = 0;
        public int deletedMessages {get;set;} = 0;
    }

    public class ShowMessageFromSenderVM
    {
        public int senderId {get;set;}
        public override string ToString() {
            return "ShowMessageFromSenderVM: " + senderId;
        }
    }
}