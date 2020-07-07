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
    /// Interaction logic for LoginUC.xaml
    /// </summary>
    public partial class LoginUC : UserControl
    {
        public LoginUC()
        {
            InitializeComponent();
        }
        //регістрація
        private void LogInBtm_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                using (AppContext ctx = new AppContext())
                {
                    User a = new User
                    {
                        Login = LoginTb.Text,
                        Password = PasswordTb.Text,
                        Email = EmailTb.Text,
                        //IsOnline = true
                    };
                    ctx.Users.Add(a);
                    ctx.SaveChanges();

                }
            }
            catch
            {
              
            }
        }
        //вхід
        private void SignInBtm_Click(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
