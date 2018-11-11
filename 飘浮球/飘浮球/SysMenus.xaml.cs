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
using System.Windows.Shapes;

namespace 飘浮球
{
    /// <summary>
    /// SysMenus.xaml 的交互逻辑
    /// </summary>
    public partial class SysMenus : Window
    {
        public SysMenus()
        {
            InitializeComponent();
        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            double w_width = this.Width;
            double w_height = this.Height;

            this.Left = PosUtil.GetAbsolutePlacement(this.Owner, true).Left + w_width/2;
            this.Top = PosUtil.GetAbsolutePlacement(this.Owner, true).Top - w_height;
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderBrush = Brushes.DarkSeaGreen;
            border.BorderThickness = new Thickness(2, 2, 2, 2);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = (Border)sender;
            border.BorderBrush = Brushes.LightGray;
            border.BorderThickness = new Thickness(1, 1, 1, 1);
        }

        private void exit(object sender, MouseButtonEventArgs e)
        {
            this.Owner.Close();
        }

        private void cancel(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
