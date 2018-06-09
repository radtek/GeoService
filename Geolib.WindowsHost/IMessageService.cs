using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Geolib.WindowsHost.Contracts
{
    [ServiceContract()]
    interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
