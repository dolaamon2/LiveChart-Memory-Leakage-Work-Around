using System;
using System.Windows.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using LiveCharts.Geared;
namespace Geared.Wpf.Hack
{
    public class CartesianChart : LiveCharts.Wpf.CartesianChart, IDisposable
    {
        private bool _disposed;
        private static Dictionary<LiveCharts.Wpf.Charts.Base.Chart, Boolean> s_gearedvaluedict;
        static CartesianChart()
        {
            FieldInfo[] fields = typeof(GearedValues<double>).GetFields(
                            BindingFlags.NonPublic |
                            BindingFlags.Static);
            var obj = fields[0].GetValue(null);
            if (obj is Dictionary<LiveCharts.Wpf.Charts.Base.Chart, Boolean>)
                s_gearedvaluedict = (Dictionary<LiveCharts.Wpf.Charts.Base.Chart, Boolean>)obj;
            Debug.Assert(s_gearedvaluedict != null);
        }

        public bool DisPosed
        {
            get { return _disposed; }
        }

        public CartesianChart()
        {
            _disposed = false;
            this.Unloaded += (sender, args) =>
            {
                Dispose();
            };
        }

        public void Dispose()
        {
            _removeGearedChart();
            _removeDispatch();
            _disposed = true;
        }

        private void _removeGearedChart()
        {
            if (CartesianChart.s_gearedvaluedict.ContainsKey(this))
                CartesianChart.s_gearedvaluedict.Remove(this);
        }

        private void _removeDispatch()
        {
            if (!_disposed)
            {
                var updater = Model.Updater;
                if (updater != null)
                {
                    var timer = updater.GetType().GetProperty("Timer").GetValue(updater, null);
                    if (timer != null && timer is System.Windows.Threading.DispatcherTimer)
                        (timer as System.Windows.Threading.DispatcherTimer).Stop();
                }
            }
        }
    }
}
