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

namespace LzzMenu
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:LzzMenu"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:LzzMenu;assembly=LzzMenu"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
    public class LzzMenu : Image
    {
        static LzzMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LzzMenu), new FrameworkPropertyMetadata(typeof(LzzMenu)));
        }

        public LzzMenu()
        {
            bindMouseEvent();
            //InitializeComponent();
        }

        //正常图标
        public static readonly DependencyProperty NormalSourceProperty = DependencyProperty.Register(
           "NormalSource", typeof(string), typeof(LzzMenu), new PropertyMetadata(default(string)));

        //选中图标
        public static readonly DependencyProperty SelectSourceProperty = DependencyProperty.Register(
            "SelectSource", typeof(string), typeof(LzzMenu), new PropertyMetadata(default(string)));

        public string NormalSource
        {
            get
            {
                return (string)this.GetValue(NormalSourceProperty);
            }
            set
            {
                this.SetValue(NormalSourceProperty, value);
            }
        }

        public string SelectSource
        {
            get
            {
                return (string)this.GetValue(SelectSourceProperty);
            }
            set
            {
                this.SetValue(SelectSourceProperty, value);
            }
        }

        private void bindMouseEvent()
        {
            this.MouseEnter += mouseEnter;
            this.MouseLeave += mouseLeave;
        }

        private void mouseEnter(object sender, MouseEventArgs e)
        {
            this.Source = new BitmapImage(new Uri(SelectSource));
        }

        private void mouseLeave(object sender, MouseEventArgs e)
        {
            this.Source = new BitmapImage(new Uri(NormalSource));
        }
    }
}
