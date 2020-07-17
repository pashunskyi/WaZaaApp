using System;
using System.Collections.Generic;
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
            if(IsSameLogin() == true)
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
           
        }
        //вхід
        private void SignInBtm_Click(object sender, RoutedEventArgs e)
        {

        }
        //перевірка на ідентичні імена
        public bool IsSameLogin()
        {
            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.Users)
                {
                    if(item.Login == LoginTb.Text)
                    {
                        MessageBox.Show("Такий нік уже зареєстрований");
                        return false;
                    }
                    
                }
            }
            return true;

            

        }
        
    }
}
