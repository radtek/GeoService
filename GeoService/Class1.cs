using GeoLib.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoService
{
    public class GeoManager : IGeoService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IZipCodeRepository _zipCodeRepository;

        public GeoManager()
        {

        }

        public GeoManager(IStateRepository stateRepository)
        {
            this._stateRepository = stateRepository;
        }

        public GeoManager(IZipCodeRepository zipCodeRepository)
        {
            this._zipCodeRepository = zipCodeRepository;
        }

        public GeoManager(IStateRepository stateRepository,IZipCodeRepository zipCodeRepository)
        {
            this._zipCodeRepository = zipCodeRepository;
            this._stateRepository = stateRepository;
        }


        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            var stateRepo = _stateRepository?? new StateRepository();
            var result = stateRepo.Get(primaryOnly);
            return result.Select(it => it.Abbreviation);
        }

        public ZipCodeData GetZipcodeInfo(string zip)
        {
            ZipCodeData data = null;
            IZipCodeRepository repo = _zipCodeRepository?? new ZipCodeRepository();
            var zipCodeEntity = repo.GetByZip(zip);
            if (zipCodeEntity != null)
            {
                data = new ZipCodeData
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip

                };
            }

            return data;

        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            var zipRepo = _zipCodeRepository??  new ZipCodeRepository();
            var result = zipRepo.GetByState(state);
            return result.Select(it => new ZipCodeData { City = it.City, State = it.State.Abbreviation, ZipCode = it.Zip });
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {

            var zipRepo = _zipCodeRepository ?? new ZipCodeRepository();
            var zipResult = zipRepo.GetByZip(zip);
            var result = zipRepo.GetZipsForRange(zipResult, range);

            return result.Select(it => new ZipCodeData { City = it.City, State = it.State.Abbreviation, ZipCode = it.Zip });
        }
    }
}
