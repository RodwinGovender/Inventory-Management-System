using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlumbingInventory.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Job_ID { get; set; }

        [Required]
        [Display(Name = "Job Name")]
        [StringLength(100, ErrorMessage = "Job name cannot be longer than 100 characters.")]
        public string Job_Name { get; set; }

        [Required]
        [Display(Name = "Date of Job")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Job_Date { get; set; }


        [Display(Name = "Job Status")]
        public string Job_Status { get; set; }

     
        public virtual ICollection<ItemRecord> ItemRecord { get; set; }




    }
}