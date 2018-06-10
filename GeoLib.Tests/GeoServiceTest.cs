using GeoLib.Contracts;
using GeoLib.Data;
using GeoService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Tests
{
   [TestFixture]
    public class GeoServiceTest
    {
        [Test]
        public void TestZipCodeRetrival()
        {
            var repo = new Mock<IZipCodeRepository>();
            repo.Setup<ZipCode>(r => r.GetByZip(It.Is<string>(str => str == "07035")))
                .Returns(new ZipCode() { City = "LINCON PARK", Zip = "07035", State = new State() { Abbreviation = "NJ" } });
            var srv = new GeoManager(repo.Object);
            var result = srv.GetZipcodeInfo("07035");
            Assert.AreEqual(result.ZipCode, "07035");

        }

        [Test]
        public void GeoLib_integrationTest()
        {
            var address = "net.pipe://localhost/GeoService";
            Binding binding = new NetNamedPipeBinding();
            var host = new ServiceHost(typeof(GeoManager));
            // host.
            host.AddServiceEndpoint(typeof(IGeoService), binding, address);
            host.Open();
            ChannelFactory<IGeoService> factory = new ChannelFactory<IGeoService>(binding,new EndpointAddress(address));
            //factory.
            var proxy = factory.CreateChannel();
            var zip = proxy.GetZipcodeInfo("07035");
            Assert.IsNotNull(zip);
            Assert.AreEqual("NJ", zip.State);

           
            factory.Close();
            host.Close();

        }

    }
}
