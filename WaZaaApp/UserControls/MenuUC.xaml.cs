using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    public partial class MenuUC : UserControl
    {
        public User usr { get; set; }

        public delegate int DisplayHandler();
        public MenuUC(int id)
        {
            InitializeComponent();
            using (AppContext ctx = new AppContext())
            {
                var u = ctx.Users.Where(q => q.Id == id).FirstOrDefault();
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
        }
        //оновлення аватару
        public void RefreshAvatar()
        {
            using (AppContext ctx = new AppContext())
            {
                var u = ctx.Users.Where(q => q.Id == usr.Id).FirstOrDefault();
                using (var ms = new System.IO.MemoryStream(u.Avatar))
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = ms;
                    image.EndInit();
                    img.ImageSource = image;
                }
            }
        }
        //оновлення імені юзера
        public void RefreshUsername()
        {
            using (AppContext ctx = new AppContext())
            {
                var u = ctx.Users.Where(q => q.Id == usr.Id).FirstOrDefault();
                UsernameTb.Text = u.Login.ToString();
            }
        }
        //оновлення вікна меню
        public void RefreshMenu(int id)
        {
            using (AppContext ctx = new AppContext())
            {
                var u = ctx.Users.Where(q => q.Id == id).FirstOrDefault();
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
        }
        //добавлення контакту
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
                                    MessageBox.Show("Such a chat already exists");
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
                    MessageBox.Show("No such user found");
                }
                if (b == true)
                {
                    var usr1 = ctx.Users.Where(c => c.Id == usr.Id).FirstOrDefault();
                    var usr2 = ctx.Users.Where(c => c.Login == UsernameNewFrendTb.Text).FirstOrDefault();

                    var chat = new Chat() { Name = usr1.Login + ", " + usr2.Login };

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
        //зміна імені юзера
        private void NewUsernameBtm_Click(object sender, RoutedEventArgs e)
        {
            if (IsloginNotNull() == true && IsSameLogin() == true)
            {
                using (AppContext ctx = new AppContext())
                {
                    var u = ctx.Users.Where(q => q.Id == usr.Id).FirstOrDefault();
                    u.Login = NewUsernameTb.Text;
                    ctx.SaveChanges();
                }
                RefreshUsername();
            }
        }
        //перевірка чи нік не пустий або не перевищує кількість допустимих символів і чи є вони допустимі
        public bool IsloginNotNull()
        {
            bool b = false;
            for (int i = 0; i < NewUsernameTb.Text.Length; i++)
            {
                if (!((NewUsernameTb.Text[i] >= 'A' && NewUsernameTb.Text[i] <= 'Z') ||
                    (NewUsernameTb.Text[i] >= 'a' && NewUsernameTb.Text[i] <= 'z') ||
                    (NewUsernameTb.Text[i] >= '0' && NewUsernameTb.Text[i] <= '9')))
                {
                    b = true;
                }
            }
            if (b == true)
            {
                MessageBox.Show("There are invalid characters in the login");
                return false;
            }
            else if (NewUsernameTb.Text.Length > 20)
            {
                MessageBox.Show("The maximum number of characters for login is 20");
                return false;
            }
            if (string.IsNullOrEmpty(NewUsernameTb.Text))
            {
                MessageBox.Show("Enter your username");
                return false;
            }
            else
            {
                return true;
            }
        }
        //перевірка на ідентичні імена
        public bool IsSameLogin()
        {
            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.Users)
                {
                    if (item.Login == NewUsernameTb.Text)
                    {
                        MessageBox.Show("Such a nickname is already registered");
                        return false;
                    }
                }
            }
            return true;
        }

        private void NewIamgeBtm_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] data = File.ReadAllBytes(filename);
                using (AppContext ctx = new AppContext())
                {
                    var u = ctx.Users.Where(q => q.Id == usr.Id).FirstOrDefault();
                    u.Avatar = data;
                    ctx.SaveChanges();
                }
                RefreshAvatar();
            }
        }
    }
}
