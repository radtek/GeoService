using GeoLib.Data;
using GeoService;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Tests
{
   [TestFixture]
    public class Class1
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

    }
}
