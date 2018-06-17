using Geo.Service;
using GeoService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.ConsoleHost
{
    public class GeoServiceComponent:ServiceBase
    {
        private ServiceHost _hostGeoManager;
        private ServiceHost _statefulHost;
        private string logfile = @"C:\geoService.txt";


        protected override void OnStart(string[] args)
        {
            try
            {
                if (_hostGeoManager != null)
                {
                    _hostGeoManager.Close();
                    _hostGeoManager = null;
                }

                _hostGeoManager = new ServiceHost(typeof(GeoManager));
                _hostGeoManager.Open();

                if (_statefulHost != null)
                {
                    _statefulHost.Close();
                    _statefulHost = null;
                }

                _statefulHost = new ServiceHost(typeof(StatefulGeoManager));
                _statefulHost.Open();
            }
            catch (Exception ex)
            {
                File.WriteAllText(logfile, ex.Message);
               
            }
           
        }


        protected override void OnStop()
        {
            if (_hostGeoManager != null)
            {
                _hostGeoManager.Close();
            }

            if (_statefulHost != null)
            {
                _statefulHost.Close();
            }
           
        }
    }
}
