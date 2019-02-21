using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Dingus.UI
{
    class BindableLineChart : ChartView
    {
        public BindableLineChart()
        {
            Chart = new LineChart();
            LineColor = Color.Black;
        }

        public IEnumerable<Microcharts.Entry> ChartEntries
        {
            get { return (IEnumerable<Microcharts.Entry>)GetValue(ChartEntriesProperty); }
            set { SetValue(ChartEntriesProperty, value); }
        }

        public Color LineColor { get; set; }

        public static readonly BindableProperty ChartEntriesProperty =
            BindableProperty.Create(nameof(ChartEntries), typeof(IEnumerable<Microcharts.Entry>), typeof(IEnumerable<Microcharts.Entry>), null, propertyChanged: HandleChartEntriesChanged);

        static void HandleChartEntriesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            BindableLineChart lineChart = (BindableLineChart)bindable;

            IEnumerable<Microcharts.Entry> entries = (IEnumerable<Microcharts.Entry>)newValue;
            if(entries == null)
            {
                return;
            }

            foreach (Microcharts.Entry entry in entries)
            {
                entry.Color = SKColor.Parse(ToHexString(lineChart.LineColor));
            }

            lineChart.Chart.Entries = entries;
            lineChart.InvalidateSurface();
        }

        static string ToHexString(Color color)
        {
            int R = (int)(color.R * 255);
            int G = (int)(color.G * 255);
            int B = (int)(color.B * 255);

            return String.Format("#{0:X2}{1:X2}{2:X2}", R, G, B);
        }
    }
}
