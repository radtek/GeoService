using Geolib.WindowsHost.Contracts;
using GeoService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Geolib.WindowsHost
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow MainUi { get; private set; }
        SynchronizationContext _syncContext;
        public MainWindow()
        {
            InitializeComponent();
            MainUi = this;
            this.Title = $"{Process.GetCurrentProcess().Id}. Thread {Thread.CurrentThread.ManagedThreadId}";
            _syncContext = SynchronizationContext.Current;
        }

        ServiceHost _host = null;
        ServiceHost _messageHost = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _host = new ServiceHost(typeof(GeoManager));
            _messageHost = new ServiceHost(typeof(MessageManager));

            _host.Open();
            _messageHost.Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _host.Close();
            _messageHost.Close();
        }

        internal void ShowMessage(string message)
        {
            SendOrPostCallback callback = new SendOrPostCallback(arg =>
            {
                var res = arg;

                lblMsg.Content = $"{Process.GetCurrentProcess().Id}. Thread {Thread.CurrentThread.ManagedThreadId} .MSG: {message}";
            });
            _syncContext.Send(callback, null);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                ChannelFactory<IMessageService> factory = new ChannelFactory<IMessageService>("");
                var proxy = factory.CreateChannel();
                proxy.ShowMessage(DateTime.Now.ToShortTimeString() + "From In Proc " + Thread.CurrentThread.ManagedThreadId);
                factory.Close();
            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
