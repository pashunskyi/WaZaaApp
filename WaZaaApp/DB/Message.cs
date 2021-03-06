﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WaZaaApp
{
    [Table("tblMessages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required, MaxLength(4000)]
        public string Text { get; set; }
        [ForeignKey("Chat")]
        public int ChatId { get; set; }

        public virtual User User { get; set; }
        public virtual Chat Chat { get; set; }

    }
}
