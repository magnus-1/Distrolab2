using System.Collections.Generic;

namespace community.Models{

    public class Conference{
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TicketPrice { get; set; }

        public List<Session> Sessions { get; set; }
        
        
        public override string ToString(){
            return this.Name + ", "+this.TicketPrice;
        }
    }
}