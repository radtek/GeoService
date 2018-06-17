using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Service
{
    public class StatefulGeoManager : IStatefulGeoService
    {
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
            IZipCodeRepository repo = new ZipCodeRepository();
            _zipCodeEntity= repo.GetByZip(zip);

        }
    }
}
