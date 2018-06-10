using Geolib.WindowsHost.Contracts;
using System.ServiceModel;

namespace Geolib.WindowsHost
{
    [ServiceBehavior(UseSynchronizationContext =false)]
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUi.ShowMessage(message);
        }
    }
}
