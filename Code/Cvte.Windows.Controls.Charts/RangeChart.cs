using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Visifire.Charts;

namespace Cvte.Windows.Controls.Chart
{
    public class RangeChart:Chart
    {
        public static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(
            "Maximum",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), MaximumChanged));

        [Bindable(true)]
        public double Maximum
        {
            get { return (double)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }


        private static void MaximumChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as RangeChart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var maxScore = (double)args.NewValue;
            chart.AxisX.AxisMaximum = maxScore;
        }


        public static readonly DependencyProperty IntervalProperty = DependencyProperty.Register(
            "Interval",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), IntervalChanged));

        [Bindable(true)]
        public double Interval
        {
            get { return (double)GetValue(IntervalProperty); }
            set { SetValue(IntervalProperty, value); }
        }


        private static void IntervalChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as RangeChart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var scoreStep = (double)args.NewValue;
            chart.AxisX.Interval = scoreStep;
        }


        public static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(
            "Minimum",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), MinScoreChanged));

        [Bindable(true)]
        public double Minimum
        {
            get { return (double)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }


        private static void MinScoreChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as RangeChart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            chart.AxisX.AxisMinimum = (double)args.NewValue;
        }

        public RangeChart()
        {
            AxisX.AxisMinimum = 0;
        }

        protected override void InitDataMappings()
        {
            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = "AxisXLabel",
                Path = "Name"
            });

            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = "XValue",
                Path = "Range"
            });

            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = "YValue",
                Path = "Value"
            });
            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = "LabelText",
                Path = "Value"
            });
            DataSeries.DataMappings.Add(new DataMapping
            {
                MemberName = "Color",
                Path = "Color"
            });
        }

        protected override void UpdateChartSource(IList<ChartItem> dataSource)
        {
            IList<RangeChartItem> rangeChartItems = new List<RangeChartItem>();
            
            foreach (ChartItem chartItem in dataSource)
            {
                var rangeChartItem = chartItem as RangeChartItem;
                if(rangeChartItem == null) continue;
                var index = (int)(rangeChartItem.Range/Interval);
                double value = Math.Round((2 * index + 1) * Interval / 2,4);
                rangeChartItem.Range = value;
                bool isContain = false;
                foreach (RangeChartItem item in rangeChartItems)
                {
                    if (Math.Abs(item.Range - value) <= 0)
                    {
                        item.Value += rangeChartItem.Value;
                        foreach (var detail in rangeChartItem.Details)
                        {
                            item.Details.Add(detail);
                        }
                        isContain = true;
                    }
                }
                if (!isContain)
                {
                    rangeChartItems.Add(rangeChartItem);
                }
            }

            DataSeries.DataSource = rangeChartItems;
            CorrectDataSeries.DataSource = rangeChartItems;
        }
    }
}
