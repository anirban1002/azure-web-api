using azure_web_api.Controllers;
using azure_web_api.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EngineerXUnitTest
{
    public class UnitTestController
    {
        private readonly Mock<IEngineerService> engineerService;
        public UnitTestController()
        {
            engineerService = new Mock<IEngineerService>();
        }
        [Fact]
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
            Assert.Equal(GetEngineerData().Result.Count, lstEngineer?.Count);
            Assert.Equal(GetEngineerData().Result.ToString(), lstEngineer?.ToString());
        }

        [Theory]
        [InlineData("837af9fa-39e5-4d36-827b-07ad6d4c7e55", "837af9fa-39e5-4d36-827b-07ad6d4c7e55")]
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
            Assert.Equal(engineerList.Result[1].id, engineer.id);
            Assert.True(engineerList.Result[1].id == engineer.id);
        }

        [Fact]
        public void AddEngineer_Engineer()
        {
            //arrange
            var engineerList = GetEngineerData();
            engineerService.Setup(x => x.AddEngineer(engineerList.Result[2]))
                .Returns(Task.Run(() => engineerList.Result[2]));

            var engineerController = new EngineerController(engineerService.Object);

            //act
            ActionResult result = engineerController.AddEngineer(engineerList.Result[2]).Result.Result;
            var data = result as OkObjectResult;
            Engineer engineer = (Engineer)data.Value;

            //assert
            Assert.NotNull(engineer);
            Assert.Equal(engineerList.Result[2].Name, engineer.Name);
            Assert.True(engineerList.Result[2].Name == engineer.Name);
        }

        [Fact]
        public void UpdateEngineer_Engineer()
        {
            //arrange
            var engineerList = GetEngineerData();
            engineerService.Setup(x => x.UpdateEngineer(engineerList.Result[0]))
                .Returns(Task.Run(() => engineerList.Result[0]));

            var engineerController = new EngineerController(engineerService.Object);

            //act
            ActionResult result = engineerController.UpdateEngineer(engineerList.Result[0]).Result.Result;
            var data = result as OkObjectResult;
            Engineer engineer = (Engineer)data.Value;

            //assert
            Assert.NotNull(engineer);
            Assert.Equal(engineerList.Result[0].Name, engineer.Name);
            Assert.True(engineerList.Result[0].Name == engineer.Name);
        }

        [Theory]
        [InlineData("837af9fa-39e5-4d36-827b-07ad6d4c7e55", "837af9fa-39e5-4d36-827b-07ad6d4c7e55")]
        public void DeleteEngineer_Engineer(string id, string partitionkey)
        {
            //arrange
            var engineerList = GetEngineerData();
            engineerService.Setup(x => x.DeleteEngineer(id, partitionkey))
                .Returns(Task.Run(() => "200"));

            var engineerController = new EngineerController(engineerService.Object);

            //act
            ActionResult result = engineerController.DeleteEngineer(id, partitionkey).Result.Result;
            var data = result as OkObjectResult;
            string deleteStatus = (string)data.Value;

            Guid idGuid = new Guid(id);

            //assert
            Assert.NotNull(result);
            Assert.True(deleteStatus == "200");
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
