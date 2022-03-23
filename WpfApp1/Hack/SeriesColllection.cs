using System;
using LiveCharts.Geared;
namespace Geared.Wpf.Hack
{
    public class SeriesCollection : LiveCharts.SeriesCollection, IDisposable
    {
        private bool _disposed;
        private LiveCharts.Wpf.Charts.Base.Chart _getChart()
        {
            if (this.Chart != null && this.Chart.View != null && this.Chart.View is LiveCharts.Wpf.Charts.Base.Chart)
                return (LiveCharts.Wpf.Charts.Base.Chart)this.Chart.View;
            else
                return null;
        }

        public SeriesCollection() : base()
        {
            _disposed = false;
        }

        public SeriesCollection(object configuration) : base(configuration)
        {
            _disposed = false;
        }

        private void _disposeChartView()
        {
            var chartView = _getChart();
            if (chartView != null && chartView is IDisposable disposedChartView)
                disposedChartView.Dispose();
        }

        private void _disposeSeries()
        {
            if (!_disposed)
            {
                for (var index = 0; index < this.Count; index++)
                {
                    var series = this[index];
                    var gearedvalues = (GearedValues<double>)series.Values;
                    var disposable = series.Values as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();
                }
            }
        }

        public void Dispose()
        {
            _disposeSeries();
            _disposeChartView();
            _disposed = true;
        }
    };
}
