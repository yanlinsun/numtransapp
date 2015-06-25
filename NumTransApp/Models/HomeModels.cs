using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace NumTransApp.Models
{
    public class NumberModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Number")]
        public string Number { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "English Currency Representation")]
        public string TransformedNumber { get; set; }
    }
}