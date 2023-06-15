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
            var engineerResult = engineerController.GetEngineer();
            ActionResult result = engineerController.GetEngineer().Result.Result;
            var data = result as OkObjectResult;
            List<Engineer> lstEngineer = (List<Engineer>)data.Value;

            //assert
            Assert.NotNull(engineerResult);
            Assert.Equal(GetEngineerData().Result.Count, lstEngineer?.Count);
            Assert.Equal(GetEngineerData().Result.ToString(), lstEngineer?.ToString());
        }

        private Task<List<Engineer>> GetEngineerData()
        {
            List<Engineer> engineerData = new List<Engineer>
        {
            new Engineer
            {
                id = new Guid(),
                Name = "Test 1",
                Country = "India",
                Speciality = "Computer"
            },
             new Engineer
            {
                id = new Guid(),
                Name = "Test 2",
                Country = "India",
                Speciality = "Electrical"
            },
             new Engineer
            {
                id = new Guid(),
                Name = "Test 3",
                Country = "India",
                Speciality = "Mechanical"
            },
        };
            return Task.Run(() => engineerData); ;
        }
    }
}
