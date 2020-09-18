using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        public byte[] ava;
        public User u { get; set; }
        public LoginUC()
        {
            InitializeComponent();
        }
        //кнопка регістрації
        private void LogInBtm_Click(object sender, RoutedEventArgs e)
        {

            if (IsSameLogin() == true && IsloginNotNull() == true && IsRegisterPasswordCorrect() == true && IsAvatarSelect() == true)
            {
                using (AppContext ctx = new AppContext())
                {
                    User user = new User
                    {
                        Login = LoginTb.Text,
                        Password = PasswordTb.Password,
                        Email = EmailTb.Text,
                        Avatar = ava
                    };
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    foreach (var item in ctx.Users)
                    {
                        if (item.Login == user.Login && item.Password == user.Password)
                        {
                            u = item;
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                PasswordTb.Password = "";
            }
        }
        //кнопка входу
        private void SignInBtm_Click(object sender, RoutedEventArgs e)
        {

            using (AppContext ctx = new AppContext())
            {
                foreach (var item in ctx.Users)
                {
                    if (item.Login == LoginTb.Text && item.Password == PasswordTb.Password)
                    {
                        u = item;
                        this.Visibility = Visibility.Collapsed;
                        return;
                    }
                }
            }
            MessageBox.Show("Username or password is incorrect");
        }
        //перевірка чи вибраний аватар
        public bool IsAvatarSelect()
        {
            if (ava != null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Choose your avatar");
                return false;
            }
        }
        //перевірка чи нік не пустий або не перевищує кількість допустимих символів і чи є вони допустимі
        public bool IsloginNotNull()
        {
            bool b = false;
            for (int i = 0; i < LoginTb.Text.Length; i++)
            {
                if (!((LoginTb.Text[i] >= 'A' && LoginTb.Text[i] <= 'Z') ||
                    (LoginTb.Text[i] >= 'a' && LoginTb.Text[i] <= 'z') ||
                    (LoginTb.Text[i] >= '0' && LoginTb.Text[i] <= '9')))
                {
                    b = true;
                }
            }
            if (b == true)
            {
                MessageBox.Show("There are invalid characters in the username");
                return false;
            }
            else if (LoginTb.Text.Length > 20)
            {
                MessageBox.Show("The maximum number of characters for username is 20");
                return false;
            }
            if (string.IsNullOrEmpty(LoginTb.Text))
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
                    if (item.Login == LoginTb.Text)
                    {
                        MessageBox.Show("Such a nickname is already registered");
                        return false;
                    }
                }
            }
            return true;
        }
        //перевірка на допустимі символи та на максимальну і мінімальну кількість символів для пароля
        public bool IsRegisterPasswordCorrect()
        {
            bool b = false;
            for (int i = 0; i < PasswordTb.Password.Length; i++)
            {
                if (!((PasswordTb.Password[i] >= 'A' && PasswordTb.Password[i] <= 'Z') ||
                    (PasswordTb.Password[i] >= 'a' && PasswordTb.Password[i] <= 'z') ||
                    (PasswordTb.Password[i] >= '0' && PasswordTb.Password[i] <= '9')))
                {
                    b = true;
                }
            }
            if (b == true)
            {
                MessageBox.Show("There are invalid characters in the password");
                return false;
            }
            else if (PasswordTb.Password.Length > 200)
            {
                MessageBox.Show("The maximum number of characters for a password is 200");
                return false;
            }
            else if (PasswordTb.Password.Length < 5 || string.IsNullOrEmpty(PasswordTb.Password))
            {
                MessageBox.Show("Short password. The minimum number of characters is 6");
                return false;
            }
            return true;
        }
        //вибір аватара
        private void SelectIamgeBtm_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                byte[] data = File.ReadAllBytes(filename);
                RegisterAvatarImg.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                ava = data;
            }
        }
    }
}
