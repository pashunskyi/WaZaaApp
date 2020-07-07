using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaZaaApp
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public virtual ICollection<User> Users { get; set; }
        [NotMapped]
        public virtual ICollection<Message> Messages { get; set; }
    }
}
