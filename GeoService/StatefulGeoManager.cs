using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Service
{
   [ServiceBehavior(IncludeExceptionDetailInFaults =true)]
    public class StatefulGeoManager : IStatefulGeoService
    {
        private string logfile = @"C:\geoService.txt";
        private ZipCode _zipCodeEntity;
        private ZipCodeRepository _zipCodeRepository;

        public ZipCodeData GetZipcodeInfo()
        {
            ZipCodeData data = null;

            if (_zipCodeEntity != null)
            {
                data = new ZipCodeData
                {
                    City = _zipCodeEntity.City,
                    State = _zipCodeEntity.State.Abbreviation,
                    ZipCode = _zipCodeEntity.Zip

                };
            }

            return data;
        }

        public IEnumerable<ZipCodeData> GetZips(int range)
        {
            var zipRepo = _zipCodeRepository ?? new ZipCodeRepository();
            //var zipResult = zipRepo.GetByZip(zip);
            var result = zipRepo.GetZipsForRange(_zipCodeEntity, range);

            return result.Select(it => new ZipCodeData { City = it.City, State = it.State.Abbreviation, ZipCode = it.Zip });
        }

        public void PushZip(string zip)
        {
            try
            {
                IZipCodeRepository repo = new ZipCodeRepository();
                _zipCodeEntity = repo.GetByZip(zip);
            }
            catch (Exception ex)
            {
                File.WriteAllText(logfile, ex.Message);

                throw ex;
            }
           

        }
    }
}
