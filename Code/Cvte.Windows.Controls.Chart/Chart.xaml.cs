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

namespace Cvte.Windows.Controls.Chart
{
    /// <summary>
    /// Chart.xaml 的交互逻辑
    /// </summary>
    public partial class Chart
    {
        private WrapPanel WrapPanel;

        #region View3D

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

        #endregion View3D

        #region ChartItemSources
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

        #endregion

        #region SelectedValue
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

        #endregion

        #region Title

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

       public static readonly DependencyProperty TitleFontFamilyProperty = DependencyProperty.Register(
      "TitleFontFamily",
      typeof(FontFamily),
      typeof(Chart),
      new PropertyMetadata(new FontFamily("Microsoft YaHei"), TitleFontFamilyChanged));

        [Bindable(true)]
        public FontFamily TitleFontFamily
        {
            get
            {
                return (FontFamily)GetValue(TitleFontFamilyProperty);
            }
            set
            {
                SetValue(TitleFontFamilyProperty, value);
            }
        }


        private static void TitleFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.ChartTitle.FontFamily = new FontFamily(args.NewValue.ToString());
        }


        public static readonly DependencyProperty TitleFontWeightProperty = DependencyProperty.Register(
     "TitleFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, TitleFontWeightChanged));

        [Bindable(true)]
        public FontWeight TitleFontWeight
        {
            get
            {
                return (FontWeight)GetValue(TitleFontWeightProperty);
            }
            set
            {
                SetValue(TitleFontWeightProperty, value);
            }
        }


        private static void TitleFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
// ReSharper disable once PossibleNullReferenceException
            chart.ChartTitle.FontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
        }

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register(
     "TitleFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, TitleFontSizeChanged));

        [Bindable(true)]
        public double TitleFontSize
        {
            get
            {
                return (double)GetValue(TitleFontSizeProperty);
            }
            set
            {
                SetValue(TitleFontSizeProperty, value);
            }
        }


        private static void TitleFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.ChartTitle.FontSize = fontsize;
        }

        public static readonly DependencyProperty TitleForegroundProperty = DependencyProperty.Register(
   "TitleForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, TitleForegroundChanged));

        [Bindable(true)]
        public Color TitleForeground
        {
            get
            {
                return (Color)GetValue(TitleForegroundProperty);
            }
            set
            {
                SetValue(TitleForegroundProperty, value);
            }
        }


        private static void TitleForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
// ReSharper disable once PossibleNullReferenceException
            chart.ChartTitle.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
        }


        #endregion

        #region Tip

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

        public static readonly DependencyProperty TipFontFamilyProperty = DependencyProperty.Register(
    "TipFontFamily",
    typeof(FontFamily),
    typeof(Chart),
    new PropertyMetadata(new FontFamily("Microsoft YaHei"), TipFontFamilyChanged));

        [Bindable(true)]
        public FontFamily TipFontFamily
        {
            get
            {
                return (FontFamily)GetValue(TipFontFamilyProperty);
            }
            set
            {
                SetValue(TipFontFamilyProperty, value);
            }
        }


        private static void TipFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.ChartTip.FontFamily = new FontFamily(args.NewValue.ToString());
        }


        public static readonly DependencyProperty TipFontWeightProperty = DependencyProperty.Register(
     "TipFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, TipFontWeightChanged));

        [Bindable(true)]
        public FontWeight TipFontWeight
        {
            get
            {
                return (FontWeight)GetValue(TipFontWeightProperty);
            }
            set
            {
                SetValue(TipFontWeightProperty, value);
            }
        }


        private static void TipFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            chart.ChartTip.FontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
        }

        public static readonly DependencyProperty TipFontSizeProperty = DependencyProperty.Register(
     "TipFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, TipFontSizeChanged));

        [Bindable(true)]
        public double TipFontSize
        {
            get
            {
                return (double)GetValue(TipFontSizeProperty);
            }
            set
            {
                SetValue(TipFontSizeProperty, value);
            }
        }


        private static void TipFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.ChartTip.FontSize = fontsize;
        }

        public static readonly DependencyProperty TipForegroundProperty = DependencyProperty.Register(
   "TipForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, TipForegroundChanged));

        [Bindable(true)]
        public Color TipForeground
        {
            get
            {
                return (Color)GetValue(TipForegroundProperty);
            }
            set
            {
                SetValue(TipForegroundProperty, value);
            }
        }


        private static void TipForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            chart.ChartTip.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
        }


        #endregion

        #region AxisXLabels
        public static readonly DependencyProperty AxisXLabelsFontFamilyProperty = DependencyProperty.Register(
"AxisXLabelsFontFamily",
typeof(FontFamily),
typeof(Chart),
new PropertyMetadata(new FontFamily("Microsoft YaHei"), AxisXLabelsFontFamilyChanged));

        [Bindable(true)]
        public FontFamily AxisXLabelsFontFamily
        {
            get
            {
                return (FontFamily)GetValue(AxisXLabelsFontFamilyProperty);
            }
            set
            {
                SetValue(AxisXLabelsFontFamilyProperty, value);
            }
        }


        private static void AxisXLabelsFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.AxisXLabels.FontFamily = fontFamily;
            chart.CorrectAxisXLabels.FontFamily = fontFamily;
        }


        public static readonly DependencyProperty AxisXLabelsFontWeightProperty = DependencyProperty.Register(
     "AxisXLabelsFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, AxisXLabelsFontWeightChanged));

        [Bindable(true)]
        public FontWeight AxisXLabelsFontWeight
        {
            get
            {
                return (FontWeight)GetValue(AxisXLabelsFontWeightProperty);
            }
            set
            {
                SetValue(AxisXLabelsFontWeightProperty, value);
            }
        }


        private static void AxisXLabelsFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.AxisXLabels.FontWeight = fontWeight;
            chart.CorrectAxisXLabels.FontWeight = fontWeight;
        }

        public static readonly DependencyProperty AxisXLabelsFontSizeProperty = DependencyProperty.Register(
     "AxisXLabelsFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, AxisXLabelsFontSizeChanged));

        [Bindable(true)]
        public double AxisXLabelsFontSize
        {
            get
            {
                return (double)GetValue(AxisXLabelsFontSizeProperty);
            }
            set
            {
                SetValue(AxisXLabelsFontSizeProperty, value);
            }
        }


        private static void AxisXLabelsFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.AxisXLabels.FontSize = fontsize;
            chart.CorrectAxisXLabels.FontSize = fontsize;
        }

        public static readonly DependencyProperty AxisXLabelsForegroundProperty = DependencyProperty.Register(
   "AxisXLabelsForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, AxisXLabelsForegroundChanged));

        [Bindable(true)]
        public Color AxisXLabelsForeground
        {
            get
            {
                return (Color)GetValue(AxisXLabelsForegroundProperty);
            }
            set
            {
                SetValue(AxisXLabelsForegroundProperty, value);
            }
        }


        private static void AxisXLabelsForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.AxisXLabels.FontColor = foreground;
            chart.CorrectAxisXLabels.FontColor = foreground;
        }
        #endregion

        #region AxisYLabels
        public static readonly DependencyProperty AxisYLabelsFontFamilyProperty = DependencyProperty.Register(
"AxisYLabelsFontFamily",
typeof(FontFamily),
typeof(Chart),
new PropertyMetadata(new FontFamily("Microsoft YaHei"), AxisYLabelsFontFamilyChanged));

        [Bindable(true)]
        public FontFamily AxisYLabelsFontFamily
        {
            get
            {
                return (FontFamily)GetValue(AxisYLabelsFontFamilyProperty);
            }
            set
            {
                SetValue(AxisYLabelsFontFamilyProperty, value);
            }
        }


        private static void AxisYLabelsFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.AxisYLabels.FontFamily = fontFamily;
            chart.CorrectAxisYLabels.FontFamily = fontFamily;
        }


        public static readonly DependencyProperty AxisYLabelsFontWeightProperty = DependencyProperty.Register(
     "AxisYLabelsFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, AxisYLabelsFontWeightChanged));

        [Bindable(true)]
        public FontWeight AxisYLabelsFontWeight
        {
            get
            {
                return (FontWeight)GetValue(AxisYLabelsFontWeightProperty);
            }
            set
            {
                SetValue(AxisYLabelsFontWeightProperty, value);
            }
        }


        private static void AxisYLabelsFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.AxisYLabels.FontWeight = fontWeight;
            chart.CorrectAxisYLabels.FontWeight = fontWeight;
        }

        public static readonly DependencyProperty AxisYLabelsFontSizeProperty = DependencyProperty.Register(
     "AxisYLabelsFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, AxisYTitleFontSizeChanged));

        [Bindable(true)]
        public double AxisYLabelsFontSize
        {
            get
            {
                return (double)GetValue(AxisYLabelsFontSizeProperty);
            }
            set
            {
                SetValue(AxisYLabelsFontSizeProperty, value);
            }
        }


        private static void AxisYLabelsFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.AxisYLabels.FontSize = fontsize;
            chart.CorrectAxisYLabels.FontSize = fontsize;
        }

        public static readonly DependencyProperty AxisYLabelsForegroundProperty = DependencyProperty.Register(
   "AxisYLabelsForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, AxisYLabelsForegroundChanged));

        [Bindable(true)]
        public Color AxisYLabelsForeground
        {
            get
            {
                return (Color)GetValue(AxisYLabelsForegroundProperty);
            }
            set
            {
                SetValue(AxisYLabelsForegroundProperty, value);
            }
        }


        private static void AxisYLabelsForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.AxisYLabels.FontColor = foreground;
            chart.CorrectAxisYLabels.FontColor = foreground;
        }
        #endregion

        #region AxisXTitle
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

        public static readonly DependencyProperty AxisXTitleFontFamilyProperty = DependencyProperty.Register(
"AxisXTitleFontFamily",
typeof(FontFamily),
typeof(Chart),
new PropertyMetadata(new FontFamily("Microsoft YaHei"), AxisXTitleFontFamilyChanged));

        [Bindable(true)]
        public FontFamily AxisXTitleFontFamily
        {
            get
            {
                return (FontFamily)GetValue(AxisXTitleFontFamilyProperty);
            }
            set
            {
                SetValue(AxisXTitleFontFamilyProperty, value);
            }
        }


        private static void AxisXTitleFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.AxisX.TitleFontFamily = fontFamily;
            chart.CorrectAxisX.TitleFontFamily = fontFamily;
        }


        public static readonly DependencyProperty AxisXTitleFontWeightProperty = DependencyProperty.Register(
     "AxisXTitleFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, AxisXTitleFontWeightChanged));

        [Bindable(true)]
        public FontWeight AxisXTitleFontWeight
        {
            get
            {
                return (FontWeight)GetValue(AxisXTitleFontWeightProperty);
            }
            set
            {
                SetValue(AxisXTitleFontWeightProperty, value);
            }
        }


        private static void AxisXTitleFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.AxisX.TitleFontWeight = fontWeight;
            chart.CorrectAxisX.TitleFontWeight = fontWeight;
        }

        public static readonly DependencyProperty AxisXTitleFontSizeProperty = DependencyProperty.Register(
     "AxisXTitleFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, AxisXTitleFontSizeChanged));

        [Bindable(true)]
        public double AxisXTitleFontSize
        {
            get
            {
                return (double)GetValue(AxisXTitleFontSizeProperty);
            }
            set
            {
                SetValue(AxisXTitleFontSizeProperty, value);
            }
        }


        private static void AxisXTitleFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.AxisX.TitleFontSize = fontsize;
            chart.CorrectAxisX.TitleFontSize = fontsize;
        }

        public static readonly DependencyProperty AxisXTitleForegroundProperty = DependencyProperty.Register(
   "AxisXTitleForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, AxisXTitleForegroundChanged));

        [Bindable(true)]
        public Color AxisXTitleForeground
        {
            get
            {
                return (Color)GetValue(AxisXTitleForegroundProperty);
            }
            set
            {
                SetValue(AxisXTitleForegroundProperty, value);
            }
        }


        private static void AxisXTitleForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.AxisX.TitleFontColor = foreground;
            chart.CorrectAxisX.TitleFontColor = foreground;
        }

        #endregion

        #region AxisYTitle
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

        public static readonly DependencyProperty AxisYTitleFontFamilyProperty = DependencyProperty.Register(
   "AxisYTitleFontFamily",
   typeof(FontFamily),
   typeof(Chart),
   new PropertyMetadata(new FontFamily("Microsoft YaHei"), AxisYTitleFontFamilyChanged));

        [Bindable(true)]
        public FontFamily AxisYTitleFontFamily
        {
            get
            {
                return (FontFamily)GetValue(AxisYTitleFontFamilyProperty);
            }
            set
            {
                SetValue(AxisYTitleFontFamilyProperty, value);
            }
        }


        private static void AxisYTitleFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.AxisY.TitleFontFamily = fontFamily;
            chart.CorrectAxisY.TitleFontFamily = fontFamily;
        }


        public static readonly DependencyProperty AxisYTitleFontWeightProperty = DependencyProperty.Register(
     "AxisYTitleFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, AxisYTitleFontWeightChanged));

        [Bindable(true)]
        public FontWeight AxisYTitleFontWeight
        {
            get
            {
                return (FontWeight)GetValue(AxisYTitleFontWeightProperty);
            }
            set
            {
                SetValue(AxisYTitleFontWeightProperty, value);
            }
        }


        private static void AxisYTitleFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.AxisY.TitleFontWeight = fontWeight;
            chart.CorrectAxisY.TitleFontWeight = fontWeight;
        }

        public static readonly DependencyProperty AxisYTitleFontSizeProperty = DependencyProperty.Register(
     "AxisYTitleFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, AxisYTitleFontSizeChanged));

        [Bindable(true)]
        public double AxisYTitleFontSize
        {
            get
            {
                return (double)GetValue(AxisYTitleFontSizeProperty);
            }
            set
            {
                SetValue(AxisYTitleFontSizeProperty, value);
            }
        }


        private static void AxisYTitleFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.AxisY.TitleFontSize = fontsize;
            chart.CorrectAxisY.TitleFontSize = fontsize;
        }

        public static readonly DependencyProperty AxisYTitleForegroundProperty = DependencyProperty.Register(
   "AxisYTitleForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, AxisYTitleForegroundChanged));

        [Bindable(true)]
        public Color AxisYTitleForeground
        {
            get
            {
                return (Color)GetValue(AxisYTitleForegroundProperty);
            }
            set
            {
                SetValue(AxisYTitleForegroundProperty, value);
            }
        }


        private static void AxisYTitleForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.AxisY.TitleFontColor = foreground;
            chart.CorrectAxisY.TitleFontColor = foreground;
        }
        #endregion

        #region RenderMode
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
        #endregion

        public Chart()
        {
            InitializeComponent();
        }

        private static void UpdateDataSource(Chart chart, IList<ChartItem> dataSource)
        {
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
