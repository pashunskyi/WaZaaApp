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
    /// Interaction logic for MessageUC.xaml
    /// </summary>
    public partial class MessageUC : UserControl
    {
        public User thisUser { get; set; }
        public Message mes { get; set; }
        public MessageUC(Message message, User user)
        {
            InitializeComponent();
            MessageTb.Text = message.Text;
            mes = message;
            thisUser = user;
            Thread myThread = new Thread(DynamicUpdateMessage);
            myThread.Start();
        }
        //динамічне оновлення повідомлення
        public void DynamicUpdateMessage()
        {
            while (true)
            {
                using (AppContext ctx = new AppContext())
                {
                    var a = ctx.Messages.Where(q => q.Id == mes.Id).FirstOrDefault();
                    if (a != null)
                    {
                        if (a.Text != mes.Text)
                        {
                            mes = a;
                            this.Dispatcher.Invoke(() =>
                            {
                                
                            MessageTb.Text = mes.Text;
                            });
                        }
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            MessageUCn.Visibility = Visibility.Collapsed;
                        });
                    }
                }
                Thread.Sleep(1000);
            }
        }
        //кнопка видалення
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext ctx = new AppContext())
            {
                var a = ctx.Messages.Where(q => q.Id == mes.Id).FirstOrDefault();
                ctx.Messages.Remove(a);
                ctx.SaveChanges();
            }
        }
        //кнопка редагування
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            OkIcon.Visibility = Visibility.Visible;
            CanselIcon.Visibility = Visibility.Visible;
            ConfirmBtm.Visibility = Visibility.Visible;
            CancelBtm.Visibility = Visibility.Visible;
            MessageTb.Visibility = Visibility.Hidden;
            EditTb.Visibility = Visibility.Visible;
            EditTb.Text = MessageTb.Text;
            EditTb.Focus();
        }
        //кнопка підтвердження
        private void ConfirmBtm_Click(object sender, RoutedEventArgs e)
        {
            using (AppContext ctx = new AppContext())
            {
                var a = ctx.Messages.Where(q => q.Id == mes.Id).FirstOrDefault();
                a.Text = EditTb.Text;
                ctx.SaveChanges();
            }
            ConfirmBtm.Visibility = Visibility.Collapsed;
            CancelBtm.Visibility = Visibility.Collapsed;
            OkIcon.Visibility = Visibility.Collapsed;
            CanselIcon.Visibility = Visibility.Collapsed;
            MessageTb.Visibility = Visibility.Visible;
            EditTb.Visibility = Visibility.Collapsed;
        }
        //кнопка скасування
        private void CancelBtm_Click(object sender, RoutedEventArgs e)
        {
            ConfirmBtm.Visibility = Visibility.Collapsed;
            CancelBtm.Visibility = Visibility.Collapsed;
            OkIcon.Visibility = Visibility.Collapsed;
            CanselIcon.Visibility = Visibility.Collapsed;
            MessageTb.Visibility = Visibility.Visible;
            EditTb.Visibility = Visibility.Collapsed;
        }
    }
}
