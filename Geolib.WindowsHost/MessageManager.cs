using Geolib.WindowsHost.Contracts;

namespace Geolib.WindowsHost
{
    public class MessageManager : IMessageService
    {
        public void ShowMessage(string message)
        {
            MainWindow.MainUi.ShowMessage(message);
        }
    }
}
