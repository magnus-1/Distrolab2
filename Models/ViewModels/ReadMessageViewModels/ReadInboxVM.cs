using System.Collections.Generic;

namespace community.Models.ViewModels.ReadMessageViewModels
{

    public class ReadInboxVM
    {
        public List<FromUser> incomingFrom { get; set; }

    }

    public class FromUser
    {
        public int senderId {get;set;}
        public string senderName {get;set;}
        public int recevedCount {get;set;}
    }

    

}