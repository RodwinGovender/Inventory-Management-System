using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlumbingInventory.Models
{
    public class ItemCat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemCat_ID { get; set; }

        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
        public string ItemCat_Name { get; set; }


        public virtual ICollection<Item> Items { get; set; }








    }
}