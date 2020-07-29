using Microsoft.Win32;
using System;
using System.Collections.Generic;
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
        public LoginUC()
        {
            InitializeComponent();

        }
        public User u { get; set; }
        //регістрація
        private void LogInBtm_Click(object sender, RoutedEventArgs e)
        {
            if (IsSameLogin() == true && IsloginNotNull() == true && IsRegisterPasswordCorrect() == true)
            {
                using (AppContext ctx = new AppContext())
                {
                    User user = new User
                    {
                        Login = LoginTb.Text,
                        Password = PasswordTb.Password,
                        Email = EmailTb.Text,
                        //Avatar = u.Avatar
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
        //вхід
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
            MessageBox.Show("логін чи пароль не вірні");
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
                MessageBox.Show("В логіні є не домустимі символи");
                return false;
            }
            else if (LoginTb.Text.Length > 20)
            {
                MessageBox.Show("Максимальна кількість символів для логіну 20");
                return false;
            }
            if (string.IsNullOrEmpty(LoginTb.Text))
            {
                MessageBox.Show("Введіть логін");
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
                        MessageBox.Show("Такий нік уже зареєстрований");
                        return false;
                    }
                }
            }
            return true;
        }
        // перевірка на допустимі символи та на максимальну і мінімальну кількість символів для пароля
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
                MessageBox.Show("В паролі є не домустимі символи");
                return false;
            }
            else if (PasswordTb.Password.Length > 200)
            {
                MessageBox.Show("Максимальна кількість символів для пароля 200");
                return false;
            }
            else if (PasswordTb.Password.Length < 5 || string.IsNullOrEmpty(PasswordTb.Password))
            {
                MessageBox.Show("Короткий пароль. Мінімальна кількість символів 6");
                return false;
            }
            return true;
        }

        private void SelectIamgeBtm_Click(object sender, RoutedEventArgs e)
        {
            FromFileTuUi();
        }
        public void FromFileTuUi()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".png";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                string filename = dlg.FileName;
                //RegisterAvatarImg.ImageSource = new BitmapImage(new Uri(filename, UriKind.Absolute));
                //u.Avatar.Source = RegisterAvatarImg.ImageSource;
                //Image myImage = new Image();
                //myImage.Source = new BitmapImage(new Uri(filename, UriKind.Absolute));
                //RegisterAvatarImg.ImageSource = myImage.Source;

                
                //var encoder = new PngBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create((BitmapSource)RegisterAvatarImg.ImageSource));
                //using (FileStream stream = new FileStream(filename, FileMode.Create))
                //    encoder.Save(stream);


                //u.Avatar =  encoder;



                //FromFileTuDb(filename);
                //BitmapImage b = new BitmapImage(new Uri(filename, UriKind.Absolute));

            }
        }

        public void FromFileTuDb(string filename)
        {
            // u = new User();
            // Image i = new Image();
            //Convert.ToByte(filename);
            // u.Avatar = new Image();


            //Image.Save(filename, ImageFormat.Png);
            //System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
            //myImage.Source = new BitmapImage(new Uri(filename));

            //var encoder = new PngBitmapEncoder();
            //encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(filename, UriKind.Absolute))));
            //using (var stream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            //{
                
                

            //    encoder.Save(stream);
            //}
        }
    }
    
}
