using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Cvte.Windows.Controls.Chart;

namespace Cvte.Windows.Controls.ChartsDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            SetChartItemSources();
        }

        private void SetChartItemSources()
        {
            IList<ChartItem> items = new List<ChartItem>();
            items.Add(new ChartItem
            {
                Name = "A",
                Value = 0.6,
                Label = "A",
                Color = new SolidColorBrush(Colors.Red)
            });

            items.Add(new ChartItem
            {
                Name = "B",
                Value = 0.2,
                Label = "B",
                Color = new SolidColorBrush(Colors.Yellow)
            });

            items.Add(new ChartItem
            {
                Name = "C",
                Value = 0.1,
                Label = "C",
                Color = new SolidColorBrush(Colors.Green)
            });

            items.Add(new ChartItem
            {
                Name = "D",
                Value = 0.3,
                Label = "D",
                Color = new SolidColorBrush(Colors.Blue)
            });

            items.Add(new ChartItem
            {
                Name = "E",
                Value = 0.8,
                Label = "E",
                Color = new SolidColorBrush(Colors.DarkMagenta)
            });

            Chart.ChartItemSources = items;
        }
    }
}
