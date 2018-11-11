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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAnimatedGif;

namespace 飘浮球
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mouseLeftDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {
            }
        }
        


        private void showMenus(object sender, MouseButtonEventArgs e)
        {
            DockPanel panel = (DockPanel)sender;
            double p_width = panel.Width;
            double p_height = panel.Height;

            Menus menus = new Menus();
            menus.WindowStartupLocation = WindowStartupLocation.Manual; 
            menus.Left = PosUtil.GetAbsolutePlacement(panel, true).Left + p_width / 2 - menus.Width/2 ;
            menus.Top = PosUtil.GetAbsolutePlacement(panel, true).Top + p_height / 2 - menus.Height/2 ;

            this.Opacity = 0.8;

            menus.ShowDialog();

            this.Opacity = 1;
        }

        private void showSysMenus(object sender, MouseButtonEventArgs e) {
            SysMenus sys_menus = new SysMenus();
            sys_menus.Owner = this;
            sys_menus.ShowDialog();
        }

        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
