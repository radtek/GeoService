using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;

namespace GeoLib.WebHost
{
    public class CustomHostFactory:ServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type constructorString, Uri[] baseAddresses)
        {
            ServiceHost host = new ServiceHost(constructorString, baseAddresses);
            return host;
        }
    }
}