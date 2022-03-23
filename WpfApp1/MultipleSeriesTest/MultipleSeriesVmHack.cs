#define HACK
using System;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Geared;
#if HACK 
using SeriesCollection = Geared.Wpf.Hack.SeriesCollection;
#else
using SeriesCollection = LiveCharts.SeriesCollection;
#endif
namespace Geared.Wpf.MultipleSeriesTest
{
    public class MultipleSeriesVmHack
    {
        public MultipleSeriesVmHack()
        {
            Series1 = new SeriesCollection();
            Series2 = new SeriesCollection();
            var r = new Random();

            for (var i = 0; i < 30; i++) // 30 series
            {
                var trend = 0d;
                var values = new double[10000];

                for (var j = 0; j < 10000; j++) // 10k points each
                {
                    trend += (r.NextDouble() < .8 ? 1 : -1) * r.Next(0, 10);
                    values[j] = trend;
                }

                var series1 = new GLineSeries
                {
                    Values = values.AsGearedValues().WithQuality(Quality.Low),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null //use a null geometry when you have many series
                };
                var series2 = new GLineSeries
                {
                    Values = values.AsGearedValues().WithQuality(Quality.Low),
                    Fill = Brushes.Transparent,
                    StrokeThickness = .5,
                    PointGeometry = null //use a null geometry when you have many series
                };
                Series1.Add(series1);
                Series2.Add(series2);
            }
        }

        public SeriesCollection Series1 { get; set; }
        public SeriesCollection Series2 { get; set; }
    }
}
