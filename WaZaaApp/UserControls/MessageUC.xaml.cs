using System;
using System.Collections.Generic;
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
        }
    }
}
