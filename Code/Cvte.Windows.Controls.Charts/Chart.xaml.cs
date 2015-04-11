using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Visifire.Charts;

namespace Cvte.Windows.Controls.Charts
{
    /// <summary>
    /// Chart.xaml 的交互逻辑
    /// </summary>
    public partial class Chart
    {
        ItemColorSet ItemColorSet;
        private WrapPanel WrapPanel;

        public static readonly DependencyProperty View3DProperty = DependencyProperty.Register(
      "View3D",
      typeof(bool),
      typeof(Chart),
      new PropertyMetadata(true, View3DChanged));

        [Bindable(true)]
        public bool View3D
        {
            get
            {
                return (bool)GetValue(View3DProperty);
            }
            set
            {
                SetValue(View3DProperty, value);
            }
        }


        private static void View3DChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.View3D = (bool)args.NewValue;
        }

        public static readonly DependencyProperty ChartItemSourcesProperty = DependencyProperty.Register(
       "ChartItemSources",
       typeof(IEnumerable<ChartItem>),
       typeof(Chart),
       new PropertyMetadata(new List<ChartItem>(),ChartItemSourcesChanged));

        [Bindable(true)]
        public IEnumerable<ChartItem> ChartItemSources
        {
            get
            {
                return (IEnumerable<ChartItem>)GetValue(ChartItemSourcesProperty);
            }
            set
            {
                SetValue(ChartItemSourcesProperty, value);
            }
        }

        private static void ChartItemSourcesChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null)
            {
                return;
            }
            if (args.NewValue == null)
            {
                return;
            }
            if(args.NewValue.Equals(args.OldValue)) return;
            var dataSource = args.NewValue as IList<ChartItem>;

           
            if(dataSource == null) return;
            UpdateDataSource(chart,dataSource);
        }


        public static readonly DependencyProperty SelectedValueProperty = DependencyProperty.Register(
       "SelectedValue",
       typeof(ChartItem),
       typeof(Chart),
       new PropertyMetadata(new ChartItem()));

        [Bindable(true)]
        public ChartItem SelectedValue
        {
            get
            {
                return (ChartItem)GetValue(SelectedValueProperty);
            }
            set
            {
                SetValue(SelectedValueProperty, value);
            }
        }


        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
       "Title",
       typeof(string),
       typeof(Chart),
       new PropertyMetadata(string.Empty, TitleChanged));

        [Bindable(true)]
        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }


        private static void TitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.ChartTitle.Text = args.NewValue.ToString();
        }


        public static readonly DependencyProperty TipProperty = DependencyProperty.Register(
       "Tip",
       typeof(string),
       typeof(Chart),
       new PropertyMetadata(string.Empty, TipChanged));

        [Bindable(true)]
        public string Tip
        {
            get
            {
                return (string)GetValue(TipProperty);
            }
            set
            {
                SetValue(TipProperty, value);
            }
        }


        private static void TipChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var tip = args.NewValue.ToString();
            if (string.IsNullOrEmpty(tip))
            {
                chart.ChartTip.Visibility = Visibility.Collapsed;
            }
            else
            {
                chart.ChartTip.Text = tip;
                chart.ChartTip.Visibility = Visibility.Visible;
            }
        }

        public static readonly DependencyProperty AxisXTitleProperty = DependencyProperty.Register(
     "AxisXTitle",
     typeof(string),
     typeof(Chart),
     new PropertyMetadata(string.Empty, AxisXTitleChanged));

        [Bindable(true)]
        public string AxisXTitle
        {
            get
            {
                return (string)GetValue(AxisXTitleProperty);
            }
            set
            {
                SetValue(AxisXTitleProperty, value);
            }
        }


        private static void AxisXTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisX.Title = args.NewValue.ToString();
            chart.CorrectAxisX.Title = args.NewValue.ToString();
        }


        public static readonly DependencyProperty AxisYTitleProperty = DependencyProperty.Register(
     "AxisYTitle",
     typeof(string),
     typeof(Chart),
     new PropertyMetadata(string.Empty, AxisYTitleChanged));

        [Bindable(true)]
        public string AxisYTitle
        {
            get
            {
                return (string)GetValue(AxisYTitleProperty);
            }
            set
            {
                SetValue(AxisYTitleProperty, value);
            }
        }


        private static void AxisYTitleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisY.Title = args.NewValue.ToString();
            chart.CorrectAxisY.Title = args.NewValue.ToString();
        }


        public static readonly DependencyProperty RenderModeProperty = DependencyProperty.Register(
      "RenderMode",
      typeof(RenderMode),
      typeof(Chart),
      new PropertyMetadata(RenderMode.Column, RenderModeChanged));

        [Bindable(true)]
        public RenderMode RenderMode
        {
            get
            {
                return (RenderMode)GetValue(RenderModeProperty);
            }
            set
            {
                SetValue(RenderModeProperty, value);
            }
        }


        private static void RenderModeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            //将RenderMode转换为RenderAs
            RenderAs renderAs;
            Enum.TryParse(args.NewValue.ToString(), out renderAs);
            chart.DataSeries.RenderAs = renderAs;
            chart.CorrectDataSeries.RenderAs = renderAs;
            var dataSource = chart.ChartItemSources as IList<ChartItem>;
            if (dataSource == null) return;
            UpdateDataSource(chart,dataSource);
        }

        public Chart()
        {
            InitializeComponent();
        }

        public void InitItemColorSet()
        {
            ItemColorSet = new ItemColorSet();
            IList<string> colorSet = new List<string>();
            if (RenderMode == RenderMode.Pie)
            {
                colorSet.Add("#7dbff2");
                colorSet.Add("#e65d8b");
                colorSet.Add("#b778b4");
            }
            else
            {
                colorSet.Add("#88b220");
                colorSet.Add("#e5b62a");
                colorSet.Add("#158ab5");
                colorSet.Add("#b778b4");
                colorSet.Add("#de5987");
            }
            ItemColorSet.SetColorSet(colorSet);
        }

        private static void UpdateDataSource(Chart chart, IList<ChartItem> dataSource)
        {
            chart.InitItemColorSet();
            chart.DataSeries.LabelEnabled = chart.RenderMode != RenderMode.Pie;
            chart.DataSeries.ShowInLegend = chart.RenderMode == RenderMode.Pie;
            chart.CorrectDataSeries.LabelEnabled = chart.RenderMode != RenderMode.Pie;
            chart.CorrectDataSeries.ShowInLegend = chart.RenderMode == RenderMode.Pie;
            foreach (var chartItem in dataSource)
            {
                //设置Label
                chartItem.Tag = chartItem.Details.Count == 0 && chart.RenderMode == RenderMode.Column
                    ? " "
                    : chartItem.Details.Count.ToString(CultureInfo.InvariantCulture);

                //设置Color
                chartItem.Color = new SolidColorBrush(chart.ItemColorSet.GetCurrentColor());
            }
            if (chart.PlotArea.ActualWidth > 0 && !double.IsNaN(chart.PlotArea.ActualWidth))
            {
                chart.VisifireChart.DataPointWidth = 60 / chart.PlotArea.ActualWidth * 100;
            }
            if (chart.CorrectPlotArea.ActualWidth > 0 && !double.IsNaN(chart.CorrectPlotArea.ActualWidth))
            {
                chart.CorrectChart.DataPointWidth = 60 / chart.CorrectPlotArea.ActualWidth * 100;
            }

            chart.DataSeries.DataSource = dataSource;
            chart.CorrectDataSeries.DataSource = dataSource;
            chart.DataSeries.UpdateLayout();
            chart.CorrectDataSeries.UpdateLayout();

            //TODO WUYIHU 判断题与选择题的图表统一
            //在判断题与选择题之间切换时，图表的dataSource已经更新,但是有时候AxisXLabel没有更新因此专门用CorrectChart图表显示判断题。
            if (dataSource.Count == 2)
            {
                chart.CorrectChart.Visibility = Visibility.Visible;
                chart.VisifireChart.Visibility = Visibility.Collapsed;
            }
            else
            {
                chart.CorrectChart.Visibility = Visibility.Collapsed;
                chart.VisifireChart.Visibility = Visibility.Visible;
            }
        }

        void Chart_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                UpdateDataSource(this, ChartItemSources.ToList());
            }
        }

        private void DataSeries_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as DataPoint;
            if(item == null) return;
            var chartItem = (ChartItem)item.DataContext;
            Detials.ItemsSource = null;
            Detials.ItemsSource = chartItem.Details;
            Popup.IsOpen = true;
            SelectedValue = item.DataContext as ChartItem;
        }

        private void Chart_OnLoaded(object sender, RoutedEventArgs e)
        {
            RenderAs renderAs;
            Enum.TryParse(RenderMode.ToString(), out renderAs);
            DataSeries.RenderAs = renderAs;
            CorrectDataSeries.RenderAs = renderAs;
            UpdateDataSource(this, ChartItemSources.ToList());
        }


        private void VisifireChart_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (PlotArea.ActualWidth > 0 && !double.IsNaN(PlotArea.ActualWidth))
            {
                VisifireChart.DataPointWidth = 60 / PlotArea.ActualWidth * 100;
            }
            DataSeries.UpdateLayout();
        }

        private void CorrectChart_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (CorrectPlotArea.ActualWidth > 0 && !double.IsNaN(CorrectPlotArea.ActualWidth))
            {
                CorrectChart.DataPointWidth = 60 / CorrectPlotArea.ActualWidth * 100;
            }
            CorrectDataSeries.UpdateLayout();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            WrapPanel = sender as WrapPanel;
        }

        private void Popup_OnOpened(object sender, EventArgs e)
        {
            if (!PopupGrid.IsVisible) return;
            if (Detials.Items.Count <= 3)
            {
                Popup.Width = 150;
                WrapPanel.Width = 100;
            }
            else if (Detials.Items.Count <=6)
            {
                Popup.Width = 250;
                WrapPanel.Width = 200;
            }
            else
            {
                Popup.Width = 350;
                WrapPanel.Width = 300;
            }
            
            if (Detials.Items.Count == 1)
            {
                Popup.Height = 60;
                WrapPanel.Height = 40;
            }
            else if (Detials.Items.Count == 2 || Detials.Items.Count == 4)
            {
                Popup.Height = 100;
                WrapPanel.Height = 80;
            }
            else if(Detials.Items.Count <= 9)
            {
                Popup.Height = 140;
                WrapPanel.Height = 120;
            }
            else
            {
                Popup.Height = 140;
                WrapPanel.Height = 40 * Math.Ceiling((double)Detials.Items.Count/3);
            }

            Popup.VerticalOffset = 25 - Popup.Height;

            POINT point;
            if (GetCursorPos(out point))
            {
                if (Popup.Width + point.X <= SystemParameters.PrimaryScreenWidth)
                {
                    LeftArrow.Visibility = Visibility.Visible;
                    RightArrow.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LeftArrow.Visibility = Visibility.Collapsed;
                    RightArrow.Visibility = Visibility.Visible;
                }
            }
        }

         [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

    }

     /// <summary>
    /// 对应RenderAs，在引用该自定义控件时就不需要引用Visifire对应的dll了
    /// </summary>
    public enum RenderMode
    {
        Column,
        Line,
        Pie,
        Bar,
        Area,
        Doughnut,
        StackedColumn,
        StackedColumn100,
        StackedBar,
        StackedBar100,
        StackedArea,
        StackedArea100,
        Bubble,
        Point,
        StreamLineFunnel,
        SectionFunnel,
        Stock,
        CandleStick,
        StepLine,
        Spline,
        Radar,
        Polar,
        Pyramid,
        QuickLine,
    }
}
