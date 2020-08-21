using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        public User usr { get; set; }
        public MenuUC(User u)
        {
            InitializeComponent();

            using (var ms = new System.IO.MemoryStream(u.Avatar))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                img.ImageSource = image;
            }
            UsernameTb.Text = u.Login.ToString();
            EmailTb.Text = u.Email.ToString();
            usr = u;
        }

        private void AddBtm_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.Users)
                {
                    if (item.Login == UsernameNewFrendTb.Text)
                    {
                        //ctx.Users.Find(usr).UsersChats.Add(new UsersChat() { Chat = new Chat() {Name = item.Login + ", " + usr.Login } }) ;
                        //ctx.UsersChats.Where(a => a.User == usr)
                        ctx.UsersChats.Add(new UsersChat() {  Chat = new Chat() { Name = item.Login + ", " + usr.Login } });
                    }
                }
                //foreach (var item in ctx.Users)
                //{
                //    if (item == usr)
                //    {
                //        ctx.Users.Where(a => a.Login == usr.Login);
                //        item.UsersChats.Add(new UsersChat().Chat.);
                //    }
                //}
            }
        }
    }
}
