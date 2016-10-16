
using System.Collections.Generic;
using community.Models.ViewModels;
namespace community.Models.ViewModels
{
    public class DestinationVM 
    {
        public bool isGroup {get; set;}
        public int destinationId {get; set;}
        public string destinationName {get; set;}
    }
    public class NewMessageVM {
        public List<DestinationVM> destinations  {get; set;}
        //public MessageVM message {get; set;}
        public string textArea { get; set; }
        public override string ToString() {
            return "NewMessageVM: " +  " : " + textArea;
        }
    }
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class SendMessageVM
    {
        public string Tmptext {get;set;}
        public List<DestinationVM> DestinationInfo {get; set;}
        //public NewMessageVM message  {get; set;}
        public override string ToString() {
            return "SendMessageVM: " + Tmptext ?? "tmp:null" + " : " + DestinationInfo.ToString() ?? "null";
        }

    }

    

    
}