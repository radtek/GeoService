using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Contracts
{
    [ServiceContract]
    public interface IGeoService
    {
        [OperationContract]
        ZipCodeData GetZipcodeInfo(string zip);

        [OperationContract]
        IEnumerable<string> GetStates(bool primaryOnly);
        [OperationContract(Name ="GetZipsbyState")]
        IEnumerable<ZipCodeData> GetZips(string state);
        [OperationContract(Name ="GetZipsForRange")]
        IEnumerable<ZipCodeData> GetZips(string zip, int range);




    }
}
