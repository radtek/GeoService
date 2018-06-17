using Geolib.Client.Contracts;
using GeoLib.Proxies;
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

namespace GeoLib.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            sClient = new StatefulGeoClient();
        }
        GeoClient client = null;
        StatefulGeoClient sClient = null;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace( zipCode.Text ))
            {
                 client = new GeoClient("tcpEP");
                var result = client.GetZipcodeInfo(zipCode.Text);
                if(result != null)
                {
                    lblCity.Content = result.City;
                    lblState.Content = result.State;
                }

                //client.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtState.Text))
            {
                EndpointAddress address = new EndpointAddress("net.tcp://localhost:8009/GeoService");
                System.ServiceModel.Channels.Binding bind = new NetTcpBinding();
                GeoClient client = new GeoClient(bind,address);
                var result = client.GetZips(txtState.Text);
                if (result != null)
                {
                    lstZips.ItemsSource = result;
                }

                client.Close();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var msg = txtMsg.Text;
            ChannelFactory<IMessageService> factory = new ChannelFactory<Geolib.Client.Contracts.IMessageService>("");
            var client = factory.CreateChannel();

            client.ShowMessage(txtMsg.Text);
            factory.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(zipCode.Text))
            {
               
                sClient.PushZip(zipCode.Text);
                //if (result != null)
                //{
                //    lblCity.Content = result.City;
                //    lblState.Content = result.State;
                //}

                //client.Close();
            }
        }
    }
}
