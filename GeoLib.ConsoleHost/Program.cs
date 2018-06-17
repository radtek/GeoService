using Geo.Service;
using GeoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                ServiceHost hostGeoManager = new ServiceHost(typeof(GeoManager));
                hostGeoManager.Open();

                ServiceHost statefulHost = new ServiceHost(typeof(StatefulGeoManager));
                statefulHost.Open();
                Console.WriteLine("Service started. Press anykey to close the service");
                Console.ReadLine();
                hostGeoManager.Close();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                     new GeoServiceComponent()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
