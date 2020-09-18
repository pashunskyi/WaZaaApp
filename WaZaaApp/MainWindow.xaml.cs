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
        public User usr { get; set; }
        int menuindex = 0;
        public MainWindow()
        {
            InitializeComponent();
            OpenRegisterWindow();
            Thread myThread = new Thread(DynamicUpdateChats);
            myThread.Start();
        }
        //динамічне оновлення чатів
        void DynamicUpdateChats()
        {
            while (true)
            {
                if (usr != null)
                {
                    using (AppContext ctx = new AppContext())
                    {
                        int i = 0;
                        int q = 0;
                        i = ctx.UsersChats.Where(q => q.UserId == usr.Id).Count();
                        this.Dispatcher.Invoke(() =>
                        {
                            q = Int32.Parse(StackChats.Children.Count.ToString());
                            
                        });
                        if (i != q)
                        {
                            RefreshChatList();
                        }
                    }
                }
                Thread.Sleep(1000);
            }
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
        }
        //показ діалогу
        public void ShowUserDialog(User contact)
        {
            NameOfCurrentChat.Text = contact.Login;
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
                                if (item2.ChatId == item.ChatId && item2.UserId == contact.Id)
                                {
                                    using (AppContext ctx3 = new AppContext())
                                    {
                                        var currentChat = ctx3.Chats.Where(q => q.Id == item2.ChatId).FirstOrDefault();
                                        DialogUC.CurrentChat = currentChat;
                                        DialogUC.usr = usr;
                                        DialogUC.usr2 = contact;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        public delegate void DelegateUser(User u);
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
                                        DelegateUser d = new DelegateUser(ShowUserDialog);

                                        ChatsUC ch = new ChatsUC(item2.UserId, d);
                                        
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
            }
        }
    }
}
