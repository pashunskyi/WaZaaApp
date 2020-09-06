using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;


namespace WaZaaApp
{
    public partial class MainWindow : Window
    {
        User usr = new User();
        int menuindex = 0;
        public MainWindow()
        {
            InitializeComponent();
            OpenRegisterWindow();
        }
        // перевірка чи увійшов користувач
        public void IsLogged()
        {
            while (true)
            {
                if (LoginUC.Visibility == Visibility.Collapsed)
                {
                    usr = LoginUC.u;
                    break;
                }
            }
            OpenUserWindow();
            RefreshChatList();
        }
        //оновлення чатів
        public void RefreshChatList()
        {
            
            this.Dispatcher.Invoke(() =>
            {
                if (StackChats.Children.Count > 0)
                {
                    StackChats.Children.Clear();
                }
            });
            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.UsersChats)
                {
                    if (item.UserId == usr.Id)
                    {
                        using (AppContext ctx2 = new AppContext())
                        {
                            foreach (var item2 in ctx2.UsersChats)
                            {
                                if (item2.ChatId == item.ChatId && item2.UserId != usr.Id)
                                {
                                    Application.Current.Dispatcher.Invoke((Action)delegate
                                    {
                                        ChatsUC ch = new ChatsUC(item2.UserId);
                                        StackChats.Children.Add(ch);
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }
        //відкрити вікно регістрації
        public void OpenRegisterWindow()
        {
            LoginUC.Visibility = Visibility.Visible;
            DialogUC.Visibility = Visibility.Hidden;
            MenuBtbImage.Visibility = Visibility.Hidden;
            Chatsbtm.Visibility = Visibility.Hidden;
            NameOfCurrentChat.Visibility = Visibility.Hidden;
            StackChats.Visibility = Visibility.Hidden;
            UpperLine.Visibility = Visibility.Hidden;
            Application.Current.MainWindow.Height = 190;
            Application.Current.MainWindow.Width = 550;
            Thread myThread = new Thread(IsLogged);
            myThread.Start();
        }
        //відкрити інтерфейс користувача
        public void OpenUserWindow()
        {
            this.Dispatcher.Invoke(() =>
            {
                LoginUC.Visibility = Visibility.Hidden;
                DialogUC.Visibility = Visibility.Visible;
                MenuBtbImage.Visibility = Visibility.Visible;
                Chatsbtm.Visibility = Visibility.Visible;
                NameOfCurrentChat.Visibility = Visibility.Visible;
                StackChats.Visibility = Visibility.Visible;
                UpperLine.Visibility = Visibility.Visible;
                Application.Current.MainWindow.Height = 600;
                Application.Current.MainWindow.Width = 500;
            });
        }
        //відкривання та закривання меню юзера
        private void Chatsbtm_Click(object sender, RoutedEventArgs e)
        {
            if (menuindex == 0)
            {
                DialogUC.Visibility = Visibility.Collapsed;
                MenuUC menu = new MenuUC(usr.Id);
                Grd.Children.Add(menu);
                Grid.SetRow(menu, 1);
                Grid.SetColumn(menu, 1);
                menu.Visibility = Visibility.Visible;
                menuindex = Grd.Children.IndexOf(menu);
            }
            else
            {
                Grd.Children.RemoveAt(menuindex);
                menuindex = 0;
                DialogUC.Visibility = Visibility.Visible;
                RefreshChatList();
            }
        }
    }
}
