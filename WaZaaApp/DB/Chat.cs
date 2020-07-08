using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaZaaApp
{
    [Table("tblChats")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<UsersChat> UsersChats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        //public virtual ICollection<User> Users { get; set; }
        //[NotMapped]
        //public virtual ICollection<Message> Messages { get; set; }
    }
}
