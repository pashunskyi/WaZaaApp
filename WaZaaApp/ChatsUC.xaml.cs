using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaZaaApp
{
    /// <summary>
    /// Interaction logic for ChatsUC.xaml
    /// </summary>
    public partial class ChatsUC : UserControl
    {
        public User usr { get; set; }
        public ChatsUC(int userId)
        {
            InitializeComponent();
            using (AppContext ctx = new AppContext())
            {
                usr = ctx.Users.Where(q => q.Id == userId).FirstOrDefault();
            }
            using (var ms = new System.IO.MemoryStream(usr.Avatar))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                AvatarImg.ImageSource = image;
            }
            UsernameTb.Text = usr.Login;
        }
    }
}
