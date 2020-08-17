using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for MenuUC.xaml
    /// </summary>
    public partial class MenuUC : UserControl
    {
        public MenuUC()
        {
            InitializeComponent();
            
            
            //BitmapImage btm;
            //using (MemoryStream ms = new MemoryStream(u.Avatar))
            //{
            //    btm = new BitmapImage();
            //    btm.BeginInit();
            //    btm.StreamSource = ms;
            //    // Below code for caching is crucial.
            //    btm.CacheOption = BitmapCacheOption.OnLoad;
            //    btm.EndInit();
            //    btm.Freeze();
            //    img.ImageSource = btm;
            //}
        }

    }
}
