
namespace community.Models.ViewModels {
    public class HomeVM
    {
        public string UserName{get; set;}
        public int UnreadMessages {get;set;}
        public string TimeStamp  {get;set;} 
        public int noOfLoginThisMonth {get;set;}
    }
}