#define HACK
using System;
using System.Windows.Controls;
using System.Collections.Generic;
using LiveCharts.Geared;
using System.Reflection;
#if HACK 
using SeriesCollection = Geared.Wpf.Hack.SeriesCollection;
#else
using SeriesCollection = LiveCharts.SeriesCollection;
#endif
namespace Geared.Wpf.MultipleSeriesTest
{
    /// <summary>
    /// Interaction logic for RecommendedSettingsView.xaml
    /// </summary>
    /// 
    public partial class MultipleSeriesViewHack : UserControl, IDisposable
    {
        public MultipleSeriesViewHack()
        {
            InitializeComponent();
        }
#if HACK
        public void Dispose()
        {
            var vm = (MultipleSeriesVmHack)DataContext;
            vm.Series1.Dispose();
            vm.Series2.Dispose();
            vm.Series1 = null;
            vm.Series2 = null;
        }
#else
        public void Dispose()
        {
            var vm = (MultipleSeriesVm)DataContext;
            for (var index = 0; index < vm.Series1.Count; index++)
            {
                var series = vm.Series1[index];
                var disposable = series.Values as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }

            for (var index = 0; index < vm.Series2.Count; index++)
            {
                var series = vm.Series2[index];
                var disposable = series.Values as IDisposable;
                if (disposable != null)
                {
                    disposable.Dispose();
                }
            }
            vm.Series1 = null;
            vm.Series2 = null;
        }
#endif
    }
}

