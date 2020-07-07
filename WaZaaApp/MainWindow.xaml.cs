using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //bool isin = false;
        public MainWindow()
        {
            InitializeComponent();
            
            using (AppContext ctx = new AppContext())
            {
                //User a = new User {Id = 1, Login = "asdasfag", Password = "asdash", Email= "asgag" };
                //ctx.Users.Add(a);
                ctx.Chats.Count();
                ctx.SaveChanges();

            }
            OpenRegisterWindow();


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
            Application.Current.MainWindow.Width = 500;
        }
        //відкрити інтерфейс користувача
        public void OpenUserWindow()
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
        }


    }
}
