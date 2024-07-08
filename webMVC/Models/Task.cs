using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webMVC.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Tasks { get; set; }

        [Required]
        public string Progress { get; set; }
    }
}