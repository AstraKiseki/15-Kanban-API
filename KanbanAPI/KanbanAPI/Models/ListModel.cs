using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KanbanAPI.Models
{
    public class ListModel
    {
        public int ListID { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}