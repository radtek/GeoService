using GeoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
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
        public MainWindow()
        {
            InitializeComponent();
        }

        ServiceHost _host = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            _host = new ServiceHost(typeof(GeoManager));
            _host.Open();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _host.Close();
        }
    }
}
