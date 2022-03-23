using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Geared.Wpf.MultipleSeriesTest;
namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UserControl _content;
        public bool HackChart { get; set; }
        public UserControl ChartContent
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged("ChartContent");
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void _onClickRefresh(object sender, RoutedEventArgs e)
        {
            if (ChartContent != null)
            {
                var disposable =  ChartContent as IDisposable;
                if (disposable != null)
                    disposable.Dispose();
            }
            ChartContent = HackChart ? (UserControl)new MultipleSeriesViewHack() : (UserControl)new MultipleSeriesView();
        }
    }
}
