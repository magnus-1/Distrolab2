using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace community.Models.ViewModels.ReadMessageViewModels
{

    public class ReadMessageIndexVM
    {
        public List<ReadMessageVM> messages { get; set; }

    }

    public class ReadMessageVM
    {

        public int id { get; set; }
        public bool isRead { get; set; }
        public string title { get; set; }
        public string time { get; set; }
        public string from { get; set; }
    }

/**
    *   Used to pass information  
    */
    public class GetMessageBodyVM
    {
        [Required(ErrorMessage = "Need message id")]
        public int id { get; set; }
        public string content { get; set; }

        public override string ToString()
        {
            return "DestinationVM: " + " : id = " + id +
            " : content = " + content;
        }
    }

    public class DeleteMessageVM
    {

        public int id { get; set; }

        public override string ToString()
        {
            return "DeleteMessageVM: " + " : id = " + id;
        }
    }
}