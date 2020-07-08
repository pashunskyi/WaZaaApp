using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Media;

namespace WaZaaApp
{
    [Table("tblUsers")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(20)]
        public string Login { get; set; }
        [StringLength(150)]
        public string Email { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        public bool IsOnline { get; set; }
        
        public byte[] Avatar { get; set; }
        
        public virtual ICollection<UsersChat> UsersChats { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
