using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Media;

namespace WaZaaApp
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [NotMapped, MaxLength(20)]
        public string Login { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped, MaxLength(20)]
        public string Password { get; set; }
        [NotMapped]
        public bool IsOnline { get; set; }
        [NotMapped]
        public byte[] Avatar { get; set; }
        [NotMapped]
        public virtual ICollection<Chat> Chats { get; set; }
    }
}
