﻿using System;
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
    /// Interaction logic for DialogUC.xaml
    /// </summary>
    public partial class DialogUC : UserControl
    {
        public DialogUC()
        {
            InitializeComponent();
            var a = new MessageUC();
            a.HorizontalAlignment = HorizontalAlignment.Left;
            var b = new MessageUC();
            b.HorizontalAlignment = HorizontalAlignment.Right;
            StackPanelCh.Children.Add(a);
            StackPanelCh.Children.Add(b);

        }
    }
}