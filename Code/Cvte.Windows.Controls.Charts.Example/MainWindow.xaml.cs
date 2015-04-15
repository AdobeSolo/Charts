using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Cvte.Windows.Controls.Chart;

namespace Cvte.Windows.Controls.ChartsDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetChartItemSources();
            SetRangeChartItemSources();
        }

        private void SetRangeChartItemSources()
        {
            IList<RangeChartItem> items = new List<RangeChartItem>();
            items.Add(new RangeChartItem
            {
                Name = "A",
                Value = 6,
                Label = "A",
                Range = 4,
                Color = new SolidColorBrush(Colors.Red),
                Details = new List<string>
                {
                    "小小明",
                    "小小明"
                }
            });

            items.Add(new RangeChartItem
            {
                Name = "B",
                Value = 13,
                Range = 7,
                Label = "B",
                Color = new SolidColorBrush(Colors.Yellow),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明"
                }
            });

            items.Add(new RangeChartItem
            {
                Name = "C",
                Value = 20,
                Range = 10,
                Label = "C",
                Color = new SolidColorBrush(Colors.Green),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                }
            });

            items.Add(new RangeChartItem
            {
                Name = "D",
                Value = 18,
                Range = 14,
                Label = "D",
                Color = new SolidColorBrush(Colors.Blue),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                    "小小名明",
                    "小打小明",
                    "小小名明",
                    "小打小明",
                }
            });

            items.Add(new RangeChartItem
            {
                Name = "E",
                Value = 4,
                Range = 20,
                Label = "E",
                Color = new SolidColorBrush(Colors.DarkMagenta),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                }
            });

            RangeChart.ChartItemSources = items;
        }

        private void SetChartItemSources()
        {
            IList<ChartItem> items = new List<ChartItem>();
            items.Add(new ChartItem
            {
                Name = "A",
                Value = 0.6,
                Label = "A",
                Color = new SolidColorBrush(Colors.Red),
                Details = new List<string>
                {
                    "小小明",
                    "小小明"
                }
            });

            items.Add(new ChartItem
            {
                Name = "B",
                Value = 0.2,
                Label = "B",
                Color = new SolidColorBrush(Colors.Yellow),
                 Details = new List<string>
                {
                    "小小名明",
                    "小打小明"
                }
            });

            items.Add(new ChartItem
            {
                Name = "C",
                Value = 0.1,
                Label = "C",
                Color = new SolidColorBrush(Colors.Green),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                }
            });

            items.Add(new ChartItem
            {
                Name = "D",
                Value = 0.3,
                Label = "D",
                Color = new SolidColorBrush(Colors.Blue),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                    "小小名明",
                    "小打小明",
                    "小小名明",
                    "小打小明",
                }
            });

            items.Add(new ChartItem
            {
                Name = "E",
                Value = 0.8,
                Label = "E",
                Color = new SolidColorBrush(Colors.DarkMagenta),
                Details = new List<string>
                {
                    "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                     "小小名明",
                    "小打小明",
                }
            });

            Chart.ChartItemSources = items;
        }

        private void Chart_OnSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var chartItem = ((SelectedItemRoutedEventArgs)e).SelectedValue;
            Console.WriteLine(chartItem.Name);
        }
    }
}
