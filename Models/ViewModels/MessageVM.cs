

namespace community.Models.ViewModels {
    public class MessageVM
    {
        public string Title {get; set;}
        public string Content { get; set; }
        public string SenderName {get;set;} 
        public string ReceiverName {get;set;}
        public override string ToString() {
            return "MessageVM: Title:" + Title + " Content:" + Content;
        }
    }
}