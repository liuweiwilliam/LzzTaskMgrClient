﻿using System;
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
using System.Windows.Shapes;

namespace 飘浮球
{
    /// <summary>
    /// Menus.xaml 的交互逻辑
    /// </summary>
    public partial class Menus : Window
    {
        public Menus()
        {
            InitializeComponent();
        }

        private void closeMenu(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void menuMouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = new SolidColorBrush(Colors.GreenYellow);
        }

        private void menuMouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
