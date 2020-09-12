using System;
using System.Collections.Generic;
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
using static WaZaaApp.MainWindow;

namespace WaZaaApp
{
    /// <summary>
    /// Interaction logic for ChatsUC.xaml
    /// </summary>
    public partial class ChatsUC : UserControl
    {
        DelegateUser del;
        public User usr { get; set; }
        public ChatsUC(int userId, DelegateUser delUser)
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
            Thread myThread = new Thread(DynamicUpdateChat);
            myThread.Start();
            del = delUser;
        }
        //динамічне оновлення чату
        void DynamicUpdateChat()
        {
            while (true)
            {
                if (usr != null)
                {

                    using (AppContext ctx = new AppContext())
                    {
                        var u = ctx.Users.Where(q => q.Id == usr.Id).FirstOrDefault();
                        if(u.Login != usr.Login || u.Avatar != usr.Avatar)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                using (var ms = new System.IO.MemoryStream(u.Avatar))
                                {
                                    var image = new BitmapImage();
                                    image.BeginInit();
                                    image.CacheOption = BitmapCacheOption.OnLoad;
                                    image.StreamSource = ms;
                                    image.EndInit();
                                    AvatarImg.ImageSource = image;
                                }
                                UsernameTb.Text = u.Login;
                            });
                            usr = u;
                            
                        }
                    }
                }
                Thread.Sleep(3000);
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            del(usr);
        }
    }
}
