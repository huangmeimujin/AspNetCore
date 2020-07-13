using LocationService;
using LocationService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Tests
{
    public class SimpleIntegrationTests
    {
        private readonly TestServer testServer;
        private readonly HttpClient testClient;
        private readonly LocationService.Models.LocationRecord record;

        public SimpleIntegrationTests()
        {
            testServer = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            testClient = testServer.CreateClient();

            //teamZombie = new Team()
            //{
            //    ID = Guid.NewGuid(),
            //    Name = "Zombie"

            //};

            record = new LocationService.Models.LocationRecord()
            {
                ID=Guid.NewGuid(),
                Latitude=12.56,
                Longitude=45.567,
                Altitude=1200,
                Timestamp=1234567,
                MemberID=Guid.NewGuid()

            };
        }

        [Fact]
        public async  void TestTeamPostAndGet()
        {
            var str = JsonConvert.SerializeObject(record);
            using (StringContent stringContent = new StringContent(str, Encoding.UTF8, "application/json"))
            {
                HttpResponseMessage postResponse = await testClient.PostAsync("locations/81dfd183-a144-47e9-a43f-9a41ebcdd646", stringContent);
                postResponse.EnsureSuccessStatusCode();

                var getResponse = await testClient.GetAsync("locations/647F2C54-88B4-492B-B1AB-9AA1F4A3DD44");
                getResponse.EnsureSuccessStatusCode();

                string raw = await getResponse.Content.ReadAsStringAsync();
                List<LocationService.Models.LocationRecord> records = JsonConvert.DeserializeObject<List<LocationService.Models.LocationRecord>>(raw);

                //List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(raw);
                //Assert.Equal("Zombie",teams[0].Name);
                //Assert.Equal(teamZombie.ID,teams[0].ID);



            };
        }
             

    }
}
