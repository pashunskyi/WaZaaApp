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
                bool b = false;
                foreach (var item in ctx.Users)
                {
                    if (item.Login == UsernameNewFrendTb.Text)
                    {
                        b = true;
                    }
                }

                if (b == true)
                {
                    
                    var usr11 = ctx.Users.Where(c => c.Id == usr.Id).FirstOrDefault();
                    var usr22 = ctx.Users.Where(c => c.Login == UsernameNewFrendTb.Text).FirstOrDefault();
                    foreach (var item in ctx.UsersChats)
                    {
                        using (AppContext ctx2 = new AppContext())
                        {
                            foreach (var item2 in ctx2.UsersChats)
                            {
                                if (item.ChatId == item2.ChatId && item.UserId == usr11.Id && item2.UserId == usr22.Id ||
                                    item.ChatId == item2.ChatId && item.UserId == usr22.Id && item2.UserId == usr11.Id)
                                {
                                    b = false;
                                    MessageBox.Show("Такий чат вже є");
                                }
                                if (b == false)
                                {
                                    break;
                                }
                            }
                        }
                        if (b == false)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Такого користувача не знайдено");
                }
                if (b == true)
                {
                    var usr1 = ctx.Users.Where(c => c.Id == usr.Id).FirstOrDefault();
                    var usr2 = ctx.Users.Where(c => c.Login == UsernameNewFrendTb.Text).FirstOrDefault();

                    var chat = new Chat() { Name = usr1.Login + " " + usr2.Login };

                    chat.UsersChats = new List<UsersChat>
                    {
                        new UsersChat
                        {
                            Chat = chat,
                            User = usr1
                        },
                        new UsersChat
                        {
                            Chat = chat,
                            User = usr2
                        }
                    };
                    ctx.Chats.Add(chat);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
