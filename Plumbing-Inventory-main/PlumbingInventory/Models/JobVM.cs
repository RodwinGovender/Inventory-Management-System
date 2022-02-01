using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlumbingInventory.Models
{
    public class JobVM
    {

        [Key]

        public int Job_IDs { get; set; }


        public string Job_Names { get; set; }

        public string Job_Dates { get; set; }

        public string Job_Statuss { get; set; }


        public List<ItemRecord> ItemRecords { get; set; }






    }
}