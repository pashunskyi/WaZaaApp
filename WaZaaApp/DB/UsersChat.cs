using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaZaaApp
{
    public class UsersChat
    {
        [ForeignKey("User"), Key, Column(Order = 0)]
        public int UserId { get; set; }
        [ForeignKey("Chat"), Key, Column(Order = 1)]
        public int ChatId { get; set; }
        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }
    }
}
