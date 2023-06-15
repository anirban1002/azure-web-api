using azure_web_api.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APINUnitTests
{
    public class EngineerServiceNUnitTest
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void Engineer_Add_Returns200()
        {
            var engineerService = new Mock<IEngineerService>();
            engineerService.Setup(x => x.)
            Assert.Pass();
        }
    }
}
