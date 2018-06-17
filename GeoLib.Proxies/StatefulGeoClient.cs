﻿using GeoLib.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Proxies
{
    public class StatefulGeoClient : ClientBase<IStatefulGeoService>, IStatefulGeoService
    {
        public ZipCodeData GetZipcodeInfo()
        {
          return  Channel.GetZipcodeInfo();
        }

        public IEnumerable<ZipCodeData> GetZips(int range)
        {
            return Channel.GetZips(range);
        }

        public void PushZip(string zip)
        {
            Channel.PushZip(zip);
        }
    }
}
