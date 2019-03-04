using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using System;
using System.Collections.Generic;
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
            get => (IEnumerable<Microcharts.Entry>)GetValue(ChartEntriesProperty);
            set => SetValue(ChartEntriesProperty, value);
        }

        public new Color BackgroundColor { get; set; }
        public Color LineColor { get; set; }

        public byte PointAreaAlpha { set { (Chart as LineChart).PointAreaAlpha = value; } }
        public byte LineAreaAlpha { set { (Chart as LineChart).LineAreaAlpha = value; } }
        public PointMode PointMode { set { (Chart as LineChart).PointMode = value; } }
        public LineMode LineMode { set { (Chart as LineChart).LineMode = value; } }
        public float PointSize { set { (Chart as LineChart).PointSize = value; } }
        public float LineSize { set { (Chart as LineChart).LineSize = value; } }

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
                entry.Color = ColorToSKColor(lineChart.LineColor);
            }

            lineChart.Chart.BackgroundColor = ColorToSKColor(lineChart.BackgroundColor);
            lineChart.Chart.Entries = entries;
            lineChart.InvalidateSurface();
        }

        static SKColor ColorToSKColor(Color color)
        {
            byte A = (byte)(color.A * 255);
            byte R = (byte)(color.R * 255);
            byte G = (byte)(color.G * 255);
            byte B = (byte)(color.B * 255);

            return new SKColor(R, G, B, A);
        }
    }
}
