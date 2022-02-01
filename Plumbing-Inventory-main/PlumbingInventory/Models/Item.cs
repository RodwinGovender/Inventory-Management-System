using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlumbingInventory.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Item_ID { get; set; }

        [Display(Name = "Item Name")]
        [StringLength(50, ErrorMessage = "Item name cannot be longer than 50 characters.")]
        public string Item_Name { get; set; }


        [Display(Name = "Quantity")]
        [Range(0, Int32.MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
        public int Item_Qty { get; set; }

        [Display(Name = "Quantity Used")]
        public int Item_QtyUsed { get; set; }


        [Display(Name = "Unit Price")]
      
        [DisplayFormat(DataFormatString = "R{0:n2}", ApplyFormatInEditMode = false)]

        public float? Item_Price { get; set; }


        public byte[] Image { get; set; }


        [Display(Name = "Category")]
        public int ItemCat_ID { get; set; }


        public int Item_Job_ID { get; set; }

        
        public virtual ItemCat ItemCat { get; set; }
        public virtual ICollection<ItemRecord> ItemRecord { get; set; }

    }
}