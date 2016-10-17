using System.Collections.Generic;

namespace community.Models.ViewModels.ReadMessageViewModels {

    public class ReadMessageIndexVM {
        public List<ReadMessageVM> messages {get;set;}

    }

    public class ReadMessageVM 
    {

       public int id { get; set; } 
       public bool isRead {get;set;}
       public string title { get; set; } 
       public string time { get; set; } 
       public string from { get; set; } 
    }

    public class GetMessageBodyVM 
    {

       public string id { get; set; } 
       public string content {get; set;}
    }
}