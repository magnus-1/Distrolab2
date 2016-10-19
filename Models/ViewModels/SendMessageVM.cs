
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using community.Models.ViewModels;
namespace community.Models.ViewModels
{
    public class DestinationVM 
    {
        public bool isGroup {get; set;}
        public int destinationId {get; set;}
        public string destinationName {get; set;}
        public override string ToString() {

            return destinationName + ", ";
            
            // return "DestinationVM: " +  " : IsGroup = " + isGroup +  
            // " : destinationId = " + destinationId +  
            // " : destinationName = " + destinationName;

        }
    }
    public class NewMessageVM {
        [Required(ErrorMessage = "Need to enter destinations")]
        public List<DestinationVM> destinations  {get; set;}
        [Required(ErrorMessage = "Need title")]
        [StringLength(60, MinimumLength = 2,ErrorMessage = "should be 2 to 60 in lenght")]
        public string title { get; set; }
        
        //[StringLength(60, MinimumLength = 2,ErrorMessage = "Text needs to be what")]
        [Required(ErrorMessage = "Need to enter message")]
        public string textArea { get; set; }
        public override string ToString() {
            return "NewMessageVM: " + "Dest : count" + destinations.Count ?? " no 0" +  "Title : " + title ?? " null" +
                    "Message : " + textArea;
        }
    }

    public class CreateMessageResponseVM {

        public string title { get; set; }
        public List<DestinationVM> destinations  {get; set;}
        public string timeStamp { get; set; }
    }

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class SendMessageVM
    {
        public string title { get; set; }
        public string Tmptext {get;set;}
        public List<DestinationVM> DestinationInfo {get; set;}
        //public NewMessageVM message  {get; set;}
        public override string ToString() {
            return "SendMessageVM: " + Tmptext ?? "tmp:null" + " : " + DestinationInfo.ToString() ?? "null";
        }

    }
}
