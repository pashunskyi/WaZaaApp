using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace WaZaaApp
{
    class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsOnline { get; set; }
        public byte[] Avatar { get; set; }
        
        
    }
}
