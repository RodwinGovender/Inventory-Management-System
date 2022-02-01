using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlumbingInventory.Models
{
    public class ItemRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemRecord_ID { get; set; }


        public int ItemRecord_QtyUsed { get; set; }

        public string ItemRecord_Status { get; set; }


        
        public int Item_ID { get; set; }

        public int Job_ID { get; set; }

        public virtual Item Item { get; set; }
        public virtual Job Job { get; set; }


    }
}