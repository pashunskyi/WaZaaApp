using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaZaaApp
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public int IdUser { get; set; }
        [NotMapped, MaxLength(1000)]
        public string Mes { get; set; }
        [NotMapped]
        public Chat Chat { get; set; }

    }
}
