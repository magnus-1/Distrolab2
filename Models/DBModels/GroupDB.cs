using System.Collections.Generic;

namespace community.Models.DBModels{
    public class GroupDB{


        public int Id { get; set; }
        public string Title {get;set;}

        public virtual List<MessageDB> messages {get;set;}
        

    }
}