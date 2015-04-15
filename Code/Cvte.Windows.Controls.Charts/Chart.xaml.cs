using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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

        IList<string> _dataPointPropertiesNameList = new List<string>();

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
            chart.UpdateDataSource(dataSource);
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

        public static readonly DependencyProperty TitleEnabledProperty = DependencyProperty.Register(
 "TitleEnabled",
 typeof(bool),
 typeof(Chart),
 new PropertyMetadata(true, TitleEnabledChanged));

        [Bindable(true)]
        public bool TitleEnabled
        {
            get
            {
                return (bool)GetValue(TitleEnabledProperty);
            }
            set
            {
                SetValue(TitleEnabledProperty, value);
            }
        }


        private static void TitleEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var enable = Convert.ToBoolean(args.NewValue.ToString());
            chart.SPTitle.Visibility = enable?Visibility.Visible:Visibility.Collapsed;
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
     new PropertyMetadata(26.0, AxisYLabelsFontSizeChanged));

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

        #region Label
        public static readonly DependencyProperty LabelEnabledProperty = DependencyProperty.Register(
   "LabelEnabled",
   typeof(bool),
   typeof(Chart),
   new PropertyMetadata(true, LabelEnabledChanged));

        [Bindable(true)]
        public bool LabelEnabled
        {
            get
            {
                return (bool)GetValue(LabelEnabledProperty);
            }
            set
            {
                SetValue(LabelEnabledProperty, value);
            }
        }


        private static void LabelEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var enable = Convert.ToBoolean(args.NewValue.ToString());
            chart.DataSeries.LabelEnabled = enable;
            chart.CorrectDataSeries.LabelEnabled = enable;
        }

        public static readonly DependencyProperty LabelFontFamilyProperty = DependencyProperty.Register(
"LabelFontFamily",
typeof(FontFamily),
typeof(Chart),
new PropertyMetadata(new FontFamily("Microsoft YaHei"), LabelFontFamilyChanged));

        [Bindable(true)]
        public FontFamily LabelFontFamily
        {
            get
            {
                return (FontFamily)GetValue(LabelFontFamilyProperty);
            }
            set
            {
                SetValue(LabelFontFamilyProperty, value);
            }
        }


        private static void LabelFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.DataSeries.LabelFontFamily = fontFamily;
            chart.CorrectDataSeries.LabelFontFamily = fontFamily;
        }


        public static readonly DependencyProperty LabelFontWeightProperty = DependencyProperty.Register(
     "LabelFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, LabelFontWeightChanged));

        [Bindable(true)]
        public FontWeight LabelFontWeight
        {
            get
            {
                return (FontWeight)GetValue(LabelFontWeightProperty);
            }
            set
            {
                SetValue(LabelFontWeightProperty, value);
            }
        }


        private static void LabelFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.DataSeries.LabelFontWeight = fontWeight;
            chart.CorrectDataSeries.LabelFontWeight = fontWeight;
        }

        public static readonly DependencyProperty LabelFontSizeProperty = DependencyProperty.Register(
     "LabelFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, LabelFontSizeChanged));

        [Bindable(true)]
        public double LabelFontSize
        {
            get
            {
                return (double)GetValue(LabelFontSizeProperty);
            }
            set
            {
                SetValue(LabelFontSizeProperty, value);
            }
        }


        private static void LabelFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.DataSeries.LabelFontSize = fontsize;
            chart.CorrectDataSeries.LabelFontSize = fontsize;
        }

        public static readonly DependencyProperty LabelForegroundProperty = DependencyProperty.Register(
   "LabelForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, LabelForegroundChanged));

        [Bindable(true)]
        public Color LabelForeground
        {
            get
            {
                return (Color)GetValue(LabelForegroundProperty);
            }
            set
            {
                SetValue(LabelForegroundProperty, value);
            }
        }


        private static void LabelForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.DataSeries.LabelFontColor = foreground;
            chart.CorrectDataSeries.LabelFontColor = foreground;
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
            chart.UpdateDataSource(dataSource);
        }
        #endregion

        #region Legend
        public static readonly DependencyProperty ShowInLegendProperty = DependencyProperty.Register(
   "ShowInLegend",
   typeof(bool),
   typeof(Chart),
   new PropertyMetadata(true, ShowInLegendChanged));

        [Bindable(true)]
        public bool ShowInLegend
        {
            get
            {
                return (bool)GetValue(ShowInLegendProperty);
            }
            set
            {
                SetValue(ShowInLegendProperty, value);
            }
        }


        private static void ShowInLegendChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var enable = Convert.ToBoolean(args.NewValue.ToString());
            chart.DataSeries.ShowInLegend = enable;
            chart.CorrectDataSeries.ShowInLegend = enable;
        }

        public static readonly DependencyProperty LegendFontFamilyProperty = DependencyProperty.Register(
"LegendFontFamily",
typeof(FontFamily),
typeof(Chart),
new PropertyMetadata(new FontFamily("Microsoft YaHei"), LegendFontFamilyChanged));

        [Bindable(true)]
        public FontFamily LegendFontFamily
        {
            get
            {
                return (FontFamily)GetValue(LegendFontFamilyProperty);
            }
            set
            {
                SetValue(LegendFontFamilyProperty, value);
            }
        }


        private static void LegendFontFamilyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var fontFamily = new FontFamily(args.NewValue.ToString());
            chart.Legend.FontFamily = fontFamily;
            chart.CorrectLegend.FontFamily = fontFamily;
        }


        public static readonly DependencyProperty LegendFontWeightProperty = DependencyProperty.Register(
     "LegendFontWeight",
     typeof(FontWeight),
     typeof(Chart),
     new PropertyMetadata(FontWeights.Light, LegendFontWeightChanged));

        [Bindable(true)]
        public FontWeight LegendFontWeight
        {
            get
            {
                return (FontWeight)GetValue(LegendFontWeightProperty);
            }
            set
            {
                SetValue(LegendFontWeightProperty, value);
            }
        }


        private static void LegendFontWeightChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var fontWeight = (FontWeight)new FontWeightConverter().ConvertFromString(args.NewValue.ToString());
            chart.Legend.FontWeight = fontWeight;
            chart.CorrectLegend.FontWeight = fontWeight;
        }

        public static readonly DependencyProperty LegendFontSizeProperty = DependencyProperty.Register(
     "LegendFontSize",
     typeof(double),
     typeof(Chart),
     new PropertyMetadata(26.0, LegendFontSizeChanged));

        [Bindable(true)]
        public double LegendFontSize
        {
            get
            {
                return (double)GetValue(LegendFontSizeProperty);
            }
            set
            {
                SetValue(LegendFontSizeProperty, value);
            }
        }


        private static void LegendFontSizeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            double fontsize;
            double.TryParse(args.NewValue.ToString(), out fontsize);
            chart.Legend.FontSize = fontsize;
            chart.CorrectLegend.FontSize = fontsize;
        }

        public static readonly DependencyProperty LegendForegroundProperty = DependencyProperty.Register(
   "LegendForeground",
   typeof(Color),
   typeof(Chart),
   new PropertyMetadata(Colors.Black, LegendForegroundChanged));

        [Bindable(true)]
        public Color LegendForeground
        {
            get
            {
                return (Color)GetValue(LegendForegroundProperty);
            }
            set
            {
                SetValue(LegendForegroundProperty, value);
            }
        }


        private static void LegendForegroundChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            // ReSharper disable once PossibleNullReferenceException
            var foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(args.NewValue.ToString()));
            chart.Legend.FontColor = foreground;
            chart.CorrectLegend.FontColor = foreground;
        }
        #endregion

        #region DataPointWidth

        public static readonly DependencyProperty DataPointWidthProperty = DependencyProperty.Register(
            "DataPointWidth",
            typeof (double),
            typeof (Chart),
            new PropertyMetadata(20.0, DataPointWidthChanged));

        [Bindable(true)]
        public double DataPointWidth
        {
            get
            {
                return (double)GetValue(DataPointWidthProperty);
            }
            set
            {
                SetValue(DataPointWidthProperty, value);
            }
        }


        private static void DataPointWidthChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.ChangeDataPointWidth();
        }
        #endregion

        #region Popup

        public static readonly DependencyProperty PopupEnabledProperty = DependencyProperty.Register(
            "PopupEnabled",
            typeof (bool),
            typeof (Chart),
            new PropertyMetadata(true, PopupEnabledChanged));

        [Bindable(true)]
        public bool PopupEnabled
        {
            get
            {
                return (bool)GetValue(PopupEnabledProperty);
            }
            set
            {
                SetValue(PopupEnabledProperty, value);
            }
        }


        private static void PopupEnabledChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {

        }
        #endregion

        #region SelectedItemChanged

        public static readonly RoutedEvent SelectedItemChangedRoutedEvent = EventManager.RegisterRoutedEvent(
                         "SelectedItemChanged", RoutingStrategy.Bubble, 
                         typeof(EventHandler<SelectedItemRoutedEventArgs>), typeof(Chart));

        public event RoutedEventHandler SelectedItemChanged
        {
            add { AddHandler(SelectedItemChangedRoutedEvent, value); }
            remove { RemoveHandler(SelectedItemChangedRoutedEvent, value); }
        }

        #endregion


        #region AxisY

        public static readonly DependencyProperty AxisYMaximumProperty = DependencyProperty.Register(
            "AxisYMaximum",
            typeof(double),
            typeof(Chart),
            new PropertyMetadata(100.0, AxisYMaximumChanged));

        [Bindable(true)]
        public double AxisYMaximum
        {
            get
            {
                return (double)GetValue(AxisYMaximumProperty);
            }
            set
            {
                SetValue(AxisYMaximumProperty, value);
            }
        }


        private static void AxisYMaximumChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisY.AxisMaximum = Convert.ToDouble(args.NewValue.ToString());
        }


        public static readonly DependencyProperty AxisYIntervelProperty = DependencyProperty.Register(
            "AxisYIntervel",
            typeof(double),
            typeof(Chart),
            new PropertyMetadata(20.0, AxisYIntervelChanged));

        [Bindable(true)]
        public double AxisYIntervel
        {
            get
            {
                return (double)GetValue(AxisYIntervelProperty);
            }
            set
            {
                SetValue(AxisYIntervelProperty, value);
            }
        }


        private static void AxisYIntervelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisY.Interval = Convert.ToDouble(args.NewValue.ToString());
        }

        public static readonly DependencyProperty AxisYValueFormatStringProperty = DependencyProperty.Register(
            "AxisYValueFormatString",
            typeof(string),
            typeof(Chart),
            new PropertyMetadata(string.Empty, AxisYValueFormatStringChanged));

        [Bindable(true)]
        public double AxisYValueFormatString
        {
            get
            {
                return (double)GetValue(AxisYValueFormatStringProperty);
            }
            set
            {
                SetValue(AxisYValueFormatStringProperty, value);
            }
        }


        private static void AxisYValueFormatStringChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as Chart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisY.ValueFormatString = args.NewValue.ToString();
        }

      
        #endregion
        public Chart()
        {
            InitializeComponent();
// ReSharper disable once DoNotCallOverridableMethodsInConstructor
            InitDataMappings();
        }

        protected void AddItemToDataMapping(string propertyName,string path)
        {
            CheckPropertyName(propertyName);
            RemoveItemFromDataMapping(propertyName);
            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = propertyName,
                Path = path
            });
            CorrectDataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = propertyName,
                Path = path
            });
        }

        protected void RemoveItemFromDataMapping(string propertyName)
        {
            CheckPropertyName(propertyName);
            foreach (var dataMapping in DataSeries.DataMappings.Where(dataMapping => dataMapping.MemberName.Equals(propertyName)))
            {
                DataSeries.DataMappings.Remove(dataMapping);
                break;
            }

            foreach (var dataMapping in CorrectDataSeries.DataMappings.Where(dataMapping => dataMapping.MemberName.Equals(propertyName)))
            {
                CorrectDataSeries.DataMappings.Remove(dataMapping);
                break;
            }
        }

        private void CheckPropertyName(string propertyName)
        {
            if (_dataPointPropertiesNameList.Count == 0)
            {
                GetDataPointPropertiesName();
            }
            if (!_dataPointPropertiesNameList.Contains(propertyName))
            {
                throw new InvalidDataException("Invalid Property Name");
            }
        }

        private void GetDataPointPropertiesName()
        {
            _dataPointPropertiesNameList.Clear();
            foreach (PropertyInfo propertyInfo in typeof(DataPoint).GetProperties())
            {
                _dataPointPropertiesNameList.Add(propertyInfo.Name);
            }
        }

        protected virtual void InitDataMappings()
        {
            AddItemToDataMapping(DataPoint.AxisXLabelProperty.Name,"Name");
            AddItemToDataMapping(DataPoint.YValueProperty.Name, "Value");
            AddItemToDataMapping(DataPoint.LabelTextProperty.Name, "Label");
            AddItemToDataMapping(DataPoint.ColorProperty.Name, "Color");
        }

        private void ChangeDataPointWidth()
        {
            if (PlotArea.ActualWidth > 0 && !double.IsNaN(PlotArea.ActualWidth))
            {
                VisifireChart.DataPointWidth = DataPointWidth / PlotArea.ActualWidth * 100;
            }
            if (CorrectPlotArea.ActualWidth > 0 && !double.IsNaN(CorrectPlotArea.ActualWidth))
            {
                CorrectChart.DataPointWidth = DataPointWidth / CorrectPlotArea.ActualWidth * 100;
            }
        }

        private void UpdateDataSource(IList<ChartItem> dataSource)
        {
            ChangeDataPointWidth();
            UpdateChartSource(dataSource);
            ExchangedChart(dataSource);
        }

        protected virtual void UpdateChartSource(IList<ChartItem> dataSource)
        {
            DataSeries.DataSource = dataSource;
            CorrectDataSeries.DataSource = dataSource;
        }

        private void ExchangedChart(IList<ChartItem> dataSource)
        {
            //TODO WUYIHU 判断题与选择题的图表统一
            //在判断题与选择题之间切换时，图表的dataSource已经更新,但是有时候AxisXLabel没有更新因此专门用CorrectChart图表显示判断题。
            if (dataSource.Count == 2)
            {
                CorrectChart.Visibility = Visibility.Visible;
                VisifireChart.Visibility = Visibility.Collapsed;
            }
            else
            {
                CorrectChart.Visibility = Visibility.Collapsed;
                VisifireChart.Visibility = Visibility.Visible;
            }
        }

        private void Chart_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeDataPointWidth();
        }
        
        private void DataSeries_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as DataPoint;
            if(item == null) return;
            var chartItem = (ChartItem)item.DataContext;
            Detials.ItemsSource = null;
            Detials.ItemsSource = chartItem.Details;
            Popup.IsOpen = PopupEnabled;
            SelectedValue = item.DataContext as ChartItem;
            SelectedItemRoutedEventArgs args = new SelectedItemRoutedEventArgs(SelectedItemChangedRoutedEvent,this)
            {
                SelectedValue = SelectedValue
            };
            RaiseEvent(args);
        }

        private void Chart_OnLoaded(object sender, RoutedEventArgs e)
        {
            RenderAs renderAs;
            Enum.TryParse(RenderMode.ToString(), out renderAs);
            DataSeries.RenderAs = renderAs;
            CorrectDataSeries.RenderAs = renderAs;
            UpdateDataSource(ChartItemSources.ToList());
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
            SetPopupSize();
            SetPopupLocation();
        }

        private void SetPopupLocation()
        {
            POINT point;
            if (GetCursorPos(out point))
            {
                if (Popup.Width + point.X <= SystemParameters.PrimaryScreenWidth)
                {
                    LeftArrow.Visibility = Visibility.Visible;
                    RightArrow.Visibility = Visibility.Collapsed;
                    Popup.HorizontalOffset = Popup.Width;
                }
                else
                {
                    LeftArrow.Visibility = Visibility.Collapsed;
                    RightArrow.Visibility = Visibility.Visible;
                    Popup.HorizontalOffset = 0;
                }
                Popup.VerticalOffset = 25 - Popup.Height;
            }
        }

        private void SetPopupSize()
        {
            if (Detials.Items.Count <= 3)
            {
                Popup.Width = 150;
                WrapPanel.Width = 100;
            }
            else if (Detials.Items.Count <= 6)
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
            else if (Detials.Items.Count <= 9)
            {
                Popup.Height = 140;
                WrapPanel.Height = 120;
            }
            else
            {
                Popup.Height = 140;
                WrapPanel.Height = 40*Math.Ceiling((double) Detials.Items.Count/3);
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

    public class SelectedItemRoutedEventArgs : RoutedEventArgs
    {
        public SelectedItemRoutedEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }

        public ChartItem SelectedValue { get; set; }
    }
}
