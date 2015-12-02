using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortUrl.Data.Models
{
    public class ShortUrl
    {
        [Key]
        public String Key { get; set; }

        [Required]
        public String Url { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
