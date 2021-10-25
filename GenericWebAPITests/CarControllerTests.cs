using GenericWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace GenericWebAPITests
{
    public class CarControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public CarControllerTests()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task TestCreateCar()
        {
            var serializedCar = JsonSerializer.Serialize(new Car {
                Type = CarType.Limousine,
                Power = 190,
                Series = "3er Serie (330d)",
                Start = new System.DateTime(2012, 7, 1),
                End = new System.DateTime(2018, 10, 1),
            });

            var result = await _client.PostAsync("api/car", new StringContent(serializedCar, System.Text.Encoding.Default, "application/json"));

            Assert.True(result.IsSuccessStatusCode);
        }
    }
}
