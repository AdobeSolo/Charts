using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Cvte.Windows.Controls.Chart
{
    public class ChartItem
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public double Percent { get; set; }

        public Brush Color { get; set; }

        public Brush SelectedColor { get; set; }

        public object Tag { get; set; }

        public IList<string> Details { get; set; }


        public int Number
        {
            get { return Details == null ? 0 : Details.Count; }
        }

        public ChartItem()
            : this(String.Empty, 0.0, new List<string>())
        {

        }

        public ChartItem(string name, double value)
            : this(name, value, new List<string>())
        {

        }

        public ChartItem(string name, double value, IList<string> details)
        {
            Name = name;
            Value = value;
            Details = details;
        }

        public ChartItem(string name, double value, double percent, IList<string> details)
        {
            Name = name;
            Value = value;
            Details = details;
            Percent = percent;
        }
    }
}
