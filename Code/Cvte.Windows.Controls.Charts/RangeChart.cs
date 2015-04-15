using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using Visifire.Charts;

namespace Cvte.Windows.Controls.Chart
{
    public class RangeChart:Chart
    {
        public static readonly DependencyProperty MaxScoreProperty = DependencyProperty.Register(
            "MaxScore",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), MaxScoreChanged));

        [Bindable(true)]
        public double MaxScore
        {
            get { return (double)GetValue(MaxScoreProperty); }
            set { SetValue(MaxScoreProperty, value); }
        }


        private static void MaxScoreChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as RangeChart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var maxScore = (double)args.NewValue;
            chart.AxisX.AxisMaximum = maxScore;
        }


        public static readonly DependencyProperty ScoreStepProperty = DependencyProperty.Register(
            "ScoreStep",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), ScoreStepChanged));

        [Bindable(true)]
        public double ScoreStep
        {
            get { return (double)GetValue(ScoreStepProperty); }
            set { SetValue(ScoreStepProperty, value); }
        }


        private static void ScoreStepChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var chart = obj as RangeChart;
            if (chart == null) return;
            if (args.NewValue.Equals(args.OldValue)) return;
            var scoreStep = (double)args.NewValue;
            chart.AxisX.Interval = scoreStep;
        }


        public static readonly DependencyProperty MinScoreProperty = DependencyProperty.Register(
            "MinScore",
            typeof(double),
            typeof(RangeChart),
            new PropertyMetadata(default(double), MinScoreChanged));

        [Bindable(true)]
        public double MinScore
        {
            get { return (double)GetValue(MinScoreProperty); }
            set { SetValue(MinScoreProperty, value); }
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
                var index = (int)(rangeChartItem.Range/ScoreStep);
                double value = Math.Round((2 * index + 1) * ScoreStep / 2,4);
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
