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

namespace WaZaaApp
{
    /// <summary>
    /// Interaction logic for DialogUC.xaml
    /// </summary>
    public partial class DialogUC : UserControl
    {
        public Chat CurrentChat { get; set; }
        public User usr { get; set; }
        public User usr2 { get; set; }
        public DialogUC()
        {
            InitializeComponent();
            Thread myThread = new Thread(DynamicUpdateDialog);
            myThread.Start();
        }
        public List<int> RefreshDialog(List<int> Mesid)
        {
            if (Mesid != null)
            {
                Mesid.Clear();
            }
            this.Dispatcher.Invoke(() =>
            {
                MessageUC m = new MessageUC(new Message(), usr);
                StackPanelCh.Children.Add(m);
                StackPanelCh.Children.Clear();
            });
            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.Messages)
                {
                    if (item.ChatId == CurrentChat.Id)
                    {
                        Mesid.Add(item.Id);
                        this.Dispatcher.Invoke(() =>
                        {
                            MessageUC m = new MessageUC(item, usr);
                            if (item.UserId == usr.Id)
                            {
                                m.HorizontalAlignment = HorizontalAlignment.Right;
                            }
                            else
                            {
                                m.HorizontalAlignment = HorizontalAlignment.Left;
                                m.CanselIcon.Visibility = Visibility.Collapsed;
                                m.OkIcon.Visibility = Visibility.Collapsed;
                                m.BoxMore.Visibility = Visibility.Collapsed;
                            }
                            StackPanelCh.Children.Add(m);
                        });
                    }
                }
            }
            this.Dispatcher.Invoke(() =>
            {
                ScrollViver.ScrollToBottom();
            });
            return Mesid;
        }
        public void DynamicUpdateDialog()
        {
            List<int> Mesid = new List<int>();
            while (true)
            {
                if (CurrentChat != null)
                {
                    var tempchat = CurrentChat;
                    while (true)
                    {
                        if (tempchat.Id == CurrentChat.Id && Mesid != null)
                        {
                            using (AppContext ctx = new AppContext())
                            {
                                foreach (var item in ctx.Messages)
                                {
                                    if (item.ChatId == CurrentChat.Id)
                                    {
                                        bool b = false;
                                        foreach (var item2 in Mesid)
                                        {
                                            if (item2 == item.Id)
                                            {
                                                b = true;
                                                break;
                                            }
                                        }
                                        if (b == false)
                                        {
                                            Mesid.Add(item.Id);
                                            this.Dispatcher.Invoke(() =>
                                            {
                                                MessageUC m = new MessageUC(item, usr);
                                                if (item.UserId == usr.Id)
                                                {
                                                    m.HorizontalAlignment = HorizontalAlignment.Right;
                                                }
                                                else
                                                {
                                                    m.HorizontalAlignment = HorizontalAlignment.Left;
                                                    m.CanselIcon.Visibility = Visibility.Collapsed;
                                                    m.OkIcon.Visibility = Visibility.Collapsed;
                                                    m.BoxMore.Visibility = Visibility.Collapsed;
                                                }
                                                StackPanelCh.Children.Add(m);
                                                ScrollViver.ScrollToBottom();
                                            });
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            tempchat = CurrentChat;
                            Mesid = RefreshDialog(Mesid);
                        }
                        Thread.Sleep(1000);
                    }
                }
                Thread.Sleep(1000);
            }
        }
        
        private void SendBtm_Click(object sender, RoutedEventArgs e)
        {
            if (MessageTb.Text != "")
            {
                if (usr != null)
                {

                    using (AppContext ctx = new AppContext())
                    {
                        var tempusr = ctx.Users.Where(c => c.Id == usr.Id).FirstOrDefault();
                        var tempchat = ctx.Chats.Where(q => q.Id == CurrentChat.Id).FirstOrDefault();
                        Message mes = new Message();
                        mes.Chat = tempchat;
                        mes.User = tempusr;
                        mes.Text = MessageTb.Text;
                        ctx.Messages.Add(mes);
                        ctx.SaveChanges();
                    }
                    MessageTb.Text = "";
                    MessageTb.Focus();
                }
            }
        }
    }
}
