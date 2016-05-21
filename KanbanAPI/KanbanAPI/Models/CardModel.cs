using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanAPI.Models
{
    public class CardModel
    {
        public int CardID { get; set; }
        public int ListID { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Text { get; set; }
    }
}