using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Input.Manipulations;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.ComponentModel;



namespace CircumferenceRotatePanel2
{

    public class CircumferenceRotatePanel2 : Canvas
    {
        

        public CircumferenceRotatePanel2()
        {
            Thread thread = new Thread(OnShowMenu);
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            


        }

        #region >>DP

        /// <summary>
        /// 自定义是否显示菜单区域
        /// </summary>
        public static readonly DependencyProperty ShowMenuProperty = DependencyProperty.Register(
            "ShowMenu", typeof(bool), typeof(CircumferenceRotatePanel2), new PropertyMetadata(default(bool)));

        //自定义显示旋转圈数
        public static readonly DependencyProperty RotateNoumberProperty = DependencyProperty.Register(
            "RotateNoumber", typeof(double), typeof(CircumferenceRotatePanel2), new PropertyMetadata(default(double)));

        //自定义旋转速度
        public static readonly DependencyProperty RotateSpeedProperty = DependencyProperty.Register(
            "RotateSpeed", typeof(double), typeof(CircumferenceRotatePanel2), new PropertyMetadata(default(double)));

        //自定义鼠标操作还是触屏操作
        public enum OperateEnum
        {
            MouseOperate,
            TouchuOperate
        }

        public static readonly DependencyProperty OperateTypeProperty = DependencyProperty.Register(
            "OperateType", typeof(OperateEnum), typeof(CircumferenceRotatePanel2), new PropertyMetadata(default(OperateEnum)));

        


        //自定义图标宽和高
        public static readonly DependencyProperty IconWidthAndHeightProperty = DependencyProperty.Register(
            "IconWidthAndHeight", typeof (double), typeof (CircumferenceRotatePanel2), new PropertyMetadata(default(double)));

        #endregion

        #region >>属性
        [Description("*自定义是否显示菜单区域")]
        public bool ShowMenu
        {
            get { return (bool)GetValue(ShowMenuProperty); }
            set { SetValue(ShowMenuProperty, value); }
        }

        [Description("*自定义旋转度数")]
        public double RotateNoumber
        {
            get { return (double)GetValue(RotateNoumberProperty); }
            set { SetValue(RotateNoumberProperty, value); }
        }

        [Description("*自定义旋转速度")]
        public double RotateSpeed
        {
            get { return (double)GetValue(RotateSpeedProperty); }
            set { SetValue(RotateSpeedProperty, value); }
        }

        [Description("*自定义操作方式")]
        public OperateEnum OperateType
        {
            get { return (OperateEnum)GetValue(OperateTypeProperty); }
            set { SetValue(OperateTypeProperty, value); }
        }

        [Description("*自定义图标宽高")]
        public double IconWidthAndHeight
        {
            get { return (double)GetValue(IconWidthAndHeightProperty); }
            set { SetValue(IconWidthAndHeightProperty, value); }
        }

        #endregion

        #region >>自定义方法
        //线程处理
        private void OnShowMenu()
        {
            this.Dispatcher.Invoke(
                new Action(
                    delegate
                    {

                        //若显示中间图标
                        DivideCircleOnShowMenu();
                        FrameworkElement fp = this.Parent as FrameworkElement;
                        if (OperateType == OperateEnum.MouseOperate)
                        {
                            this.MouseDown += Rotate_MouseDown;
                            fp.MouseUp += Fp_MouseUp;

                        }
                        else
                        {

                            this.TouchDown += Fotate_TouchDown;
                            fp.TouchUp += Rotate_TouchUp;

                        }
                        centerP.X = this.Width / 2;
                        centerP.Y = this.Height / 2;

                    }
            ));
        }

        #region 变量
        private Point mBefore = new Point(); //鼠标点击前位置
        private TouchPoint mBeforeTouchPoint; //触摸前位置
        private bool isMove = false; //鼠标是否移动
        private double rotateNumber;//旋转角度
        private Point centerP; //中心点位置
        private TouchPoint centerTouchPoint;//中心点位置
        private double rotateAng = 0; //旋转总角度
        #endregion

        #region 鼠标事件
        //点击中间图标让周围图标旋转一圈
        private void F_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RotateAnimation(this, RotateNoumber);
            RotateAnimation((UIElement)sender, -RotateNoumber);
        }
        //选中一个图标鼠标按下事件
        private void Rotate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.mBefore = e.GetPosition(this);

            this.MouseMove += Rotate_MouseMove;
        }

        //拖动图标旋转事件
        private void Rotate_MouseMove(object sender, MouseEventArgs e)
        {
            /*Point mAfter = e.GetPosition(this); //获取鼠标移动过程中的坐标


            Point n1 = new Point(centerP.X - mBefore.X, centerP.Y - mBefore.Y);
            Point n2 = new Point(centerP.X - mAfter.X, centerP.Y - mAfter.Y);

            //n1*n2
            double n1n2 = n1.X * n2.X + n1.Y * n2.Y;
            //n1的模
            double n1mo = Math.Sqrt(Math.Pow(n1.X, 2) + Math.Pow(n1.Y, 2));
            //n2的模
            double n2mo = Math.Sqrt(Math.Pow(n2.X, 2) + Math.Pow(n2.Y, 2));


            //得带旋转角度
            double rotateNum = Math.Acos(n1n2 / (n1mo * n2mo));

            //相对坐标原点位置
            Point potM = new Point();
            potM.X = mAfter.X - centerP.X;
            potM.Y = centerP.Y - mAfter.Y;
            Point potD = new Point();
            potD.X = mBefore.X - centerP.X;
            potD.Y = centerP.Y - mBefore.Y;


            //当鼠标移动超出边界时停止旋转
            if (mAfter.X < 0 || mAfter.X > this.Width || mAfter.Y < 0 || mAfter.Y > this.Height)
            {
                this.MouseMove -= Rotate_MouseMove;

            }

            else
            {
                if (GetcLockwise(potD, potM))
                {
                    rotateAng += rotateNum;
                }
                else
                {
                    rotateAng -= rotateNum;
                }
            }

            //执行旋转动画

            IconRotateAnimation(-rotateAng);
            CanvansRotateAnimation(rotateAng);
            */
        }

        #endregion

        #region Touch事件
        void F_TouchUp(object sender, TouchEventArgs e)
        {
            RotateAnimation(this, RotateNoumber);
            RotateAnimation((UIElement)sender, -RotateNoumber);
        }

        //TouchDown事件
        void Fotate_TouchDown(object sender, TouchEventArgs e)
        {

            this.mBeforeTouchPoint = e.GetTouchPoint(this);
            this.TouchMove += Rotate_TouchMove;
        }

        //TouchMove事件
        private void Rotate_TouchMove(object sender, TouchEventArgs e)
        {
            TouchPoint mAfterTouchPoint = e.GetTouchPoint(this); //获取鼠标移动过程中的坐标

            Point n1 = new Point(centerP.X - mBeforeTouchPoint.Position.X, centerP.Y - mBeforeTouchPoint.Position.Y);
            Point n2 = new Point(centerP.X - mAfterTouchPoint.Position.X, centerP.Y - mAfterTouchPoint.Position.Y);

            //n1*n2
            double n1n2 = n1.X * n2.X + n1.Y * n2.Y;
            //n1的模
            double n1mo = Math.Sqrt(Math.Pow(n1.X, 2) + Math.Pow(n1.Y, 2));
            //n2的模
            double n2mo = Math.Sqrt(Math.Pow(n2.X, 2) + Math.Pow(n2.Y, 2));


            //得带旋转角度
            double rotateNum = Math.Acos(n1n2 / (n1mo * n2mo));
            if (rotateNum.ToString() == "非数字")
            {
                rotateNum = 0;
            }
            //相对坐标原点位置
            Point potM = new Point();
            potM.X = mAfterTouchPoint.Position.X - centerP.X;
            potM.Y = centerP.Y - mAfterTouchPoint.Position.Y;
            Point potD = new Point();
            potD.X = mBeforeTouchPoint.Position.X - centerP.X;
            potD.Y = centerP.Y - mBeforeTouchPoint.Position.Y;


            //当鼠标移动超出边界时停止旋转
            if (mAfterTouchPoint.Position.X < 0 || mAfterTouchPoint.Position.X > this.Width || mAfterTouchPoint.Position.Y < 0 || mAfterTouchPoint.Position.Y > this.Height)
            {
                this.TouchMove -= Rotate_TouchMove;

            }

            else
            {
                if (GetcLockwise(potD, potM))
                {
                    rotateAng += rotateNum;
                }
                else
                {
                    rotateAng -= rotateNum;
                }
            }

            //执行旋转动画

            CanvansRotateAnimation(rotateAng);
            IconRotateAnimation(-rotateAng);
        }
        //释放父类
        private void Fp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.MouseMove -= Rotate_MouseMove;
        }


        //TouchUp事件
        private void Rotate_TouchUp(object sender, TouchEventArgs e)
        {
            this.TouchMove -= Rotate_TouchMove;

        }
        #endregion

        #region 动画处理
        //等分圆周 显示中间和周围图标
        private void DivideCircleOnShowMenu()
        {
            //获取孩子节点
            UIElementCollection children = this.Children;

            FrameworkElement f;

            
            //外圆半径
            double outCircleRadius = this.Width/2-IconWidthAndHeight/2;

            //平分度数
            double divideEquallyAngle = 360 / (children.Count - 1);

            for (int i = 0; i < children.Count; i++)
            {
                f = children[i] as FrameworkElement;
                
                //第一个中间图标
                if (i == 0)
                {
                    if (ShowMenu)
                    {
                        f.SetValue(Canvas.TopProperty, outCircleRadius );
                        f.SetValue(Canvas.LeftProperty, outCircleRadius );
                        if (OperateType == OperateEnum.MouseOperate)
                        {
                            f.MouseDown += F_MouseDown;
                        }
                        else
                        {
                            f.TouchUp += F_TouchUp;
                        }
                    }

                    else
                    {
                        f.SetValue(Canvas.TopProperty, outCircleRadius - f.Width / 2);
                        f.SetValue(Canvas.LeftProperty, outCircleRadius - f.Width / 2);
                        f.Visibility = Visibility.Hidden;
                    }
                }

                else
                {
                    //内角度数  角度转换为弧度
                    double innerAngle = divideEquallyAngle * (i-1) * Math.PI / 180;

                    //TOP距离
                    double topHeight = outCircleRadius - Math.Cos(innerAngle) * outCircleRadius;

                    //Left距离
                    double leftWidth = Math.Sin(innerAngle) * outCircleRadius;

                    if (innerAngle <= 180)
                    {
                        f.SetValue(Canvas.TopProperty, topHeight );
                        f.SetValue(Canvas.LeftProperty, outCircleRadius + leftWidth );
                    }
                    if (innerAngle > 180)
                    {
                        f.SetValue(Canvas.TopProperty, topHeight);
                        f.SetValue(Canvas.LeftProperty, outCircleRadius - leftWidth );
                    }
                }
            }
        }

        /// <summary>
        /// 获取顺时针还是逆时针
        /// </summary>
        /// <param name="potD">按下坐标</param>
        /// <param name="potM">移动坐标</param>
        /// <returns>True：顺，False：逆</returns>
        private bool GetcLockwise(Point potD, Point potM)
        {
            if (potM.Y >= 0 && potD.Y >= 0) //一二象限
            {
                return potM.X >= potD.X;
            }
            if (potM.Y < 0 && potD.Y < 0)   //三四象限
            {
                return potM.X <= potD.X;
            }
            if (potM.X >= 0 && potD.X >= 0) //一四象限
            {
                return potM.Y <= potD.Y;
            }
            if (potM.X < 0 && potD.X < 0)   //二三象限
            {
                return potM.Y >= potD.Y;
            }
            else
            {
                return true;
            }
        }

        //图标绕中心点旋转
        private void OnShowMenuAnimation(double rotateNumber)
        {
            RotateTransform rtf = new RotateTransform();
            UIElementCollection children = this.Children;
            FrameworkElement f;
            //图标宽高
            double iconWidthAndHeight = 50;
            double left;
            double top;
            //中心点
            double centerPointX = this.Width / 2-iconWidthAndHeight/2;
            double centerPointY = this.Height / 2-iconWidthAndHeight/2;

            //每个图标相对中心点的点
            double relativePointX;
            double relativePointY;

            for (int i = 0; i < children.Count; i++)
            {
                f = children[i] as FrameworkElement;
                //不是第一个图标时
                if (i != 0)
                {

                    left = (double)f.GetValue(Canvas.LeftProperty);
                    top = (double)f.GetValue(Canvas.TopProperty);

                    relativePointX = (centerPointX - left) / iconWidthAndHeight;
                    relativePointY = (centerPointY - top) / iconWidthAndHeight;

                    f.RenderTransform = rtf;
                    f.RenderTransformOrigin = new Point(relativePointX, relativePointY);


                }
            }
            //定义动画路径和事件
            DoubleAnimation dbAnimation = new DoubleAnimation(0, rotateNumber,
                new Duration(TimeSpan.FromSeconds(RotateSpeed)));
            //定义动画次数
            dbAnimation.RepeatBehavior = new RepeatBehavior(RotateNoumber);
            //开始动画
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAnimation);
        }

        //图标自旋转
        private void IconRotateAnimation(double rotateNumber)
        {
            if (rotateNumber.ToString() == "非数字")
            {
                rotateNumber = 0;
            }
            RotateTransform rtf = new RotateTransform();
            UIElementCollection children = this.Children;
            FrameworkElement f;
            for (int i = 0; i < children.Count; i++)
            {
                f = children[i] as FrameworkElement;

                f.RenderTransform = rtf;
                f.RenderTransformOrigin = new Point(0.5, 0.5);

            }

            //定义动画路径和事件
            DoubleAnimation dbAnimation = new DoubleAnimation(0, rotateNumber,
                new Duration(TimeSpan.FromSeconds(0)));


            //开始动画
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAnimation);
        }

        //canvans旋转
        private void CanvansRotateAnimation(double rotateNumber)
        {
            if (rotateNumber.ToString() == "非数字")
            {
                rotateNumber = 0;
            }
            RotateTransform rtf = new RotateTransform();
            this.RenderTransform = rtf;
            this.RenderTransformOrigin = new Point(0.5, 0.5);

            //定义动画路径和事件
            DoubleAnimation dbAnimation = new DoubleAnimation(0, rotateNumber,
                new Duration(TimeSpan.FromSeconds(0.0)));


            //开始动画
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAnimation);

        }

        //canvans旋转
        private void RotateAnimation(UIElement uIelement, double rotateNumber)
        {
            RotateTransform rtf = new RotateTransform();
            uIelement.RenderTransform = rtf;
            uIelement.RenderTransformOrigin = new Point(0.5, 0.5);

            //定义动画路径和事件
            DoubleAnimation dbAnimation = new DoubleAnimation(0, rotateNumber,
                new Duration(TimeSpan.FromSeconds(RotateSpeed)));

            //开始动画
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAnimation);
        }
        #endregion
        #endregion
    }
}


