using azure_web_api.Controllers;
using azure_web_api.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EngineerNUnitTest
{
    public class UnitTestController
    {
        private Mock<IEngineerService> engineerService;

        [SetUp]
        public void Setup()
        {
            engineerService = new Mock<IEngineerService>();
        }
        [Test]
        public void GetEngineer_EngineerList()
        {
            //arrange
            var engineerList = GetEngineerData();
            engineerService.Setup(x => x.GetEngineerDetails())
                .Returns(engineerList);

            var engineerController = new EngineerController(engineerService.Object);

            //act
            ActionResult result = engineerController.GetEngineer().Result.Result;
            var data = result as OkObjectResult;
            List<Engineer> lstEngineer = (List<Engineer>)data.Value;

            //assert
            Assert.NotNull(lstEngineer);
            Assert.AreEqual(GetEngineerData().Result.Count, lstEngineer?.Count);
            Assert.AreEqual(GetEngineerData().Result.ToString(), lstEngineer?.ToString());
        }

        [TestCase("837af9fa-39e5-4d36-827b-07ad6d4c7e55", "837af9fa-39e5-4d36-827b-07ad6d4c7e55")]
        public void GetEngineerById_Engineer(string id, string partitionkey)
        {
            //arrange
            var engineerList = GetEngineerData();
            engineerService.Setup(x => x.GetEngineerDetailsById(id, partitionkey))
                .Returns(Task.Run(() => engineerList.Result[1]));

            var engineerController = new EngineerController(engineerService.Object);

            //act
            ActionResult result = engineerController.GetEngineerById(id, partitionkey).Result.Result;
            var data = result as OkObjectResult;
            Engineer engineer = (Engineer)data.Value;

            //assert
            Assert.NotNull(engineer);
            Assert.AreEqual(engineerList.Result[1].id, engineer.id);
            Assert.True(engineerList.Result[1].id == engineer.id);
        }

        private Task<List<Engineer>> GetEngineerData()
        {
            List<Engineer> engineerData = new List<Engineer>
        {
            new Engineer
            {
                id = new Guid("5bc484b5-42bd-478c-866b-dba469a98908"),
                Name = "Test 1",
                Country = "India",
                Speciality = "Computer"
            },
             new Engineer
            {
                id = new Guid("837af9fa-39e5-4d36-827b-07ad6d4c7e55"),
                Name = "Test 2",
                Country = "India",
                Speciality = "Electrical"
            },
             new Engineer
            {
                id = new Guid("4e55a241-beaf-47cc-8236-0c4bab220feb"),
                Name = "Test 3",
                Country = "India",
                Speciality = "Mechanical"
            },
        };
            return Task.Run(() => engineerData); ;
        }
    }
}
