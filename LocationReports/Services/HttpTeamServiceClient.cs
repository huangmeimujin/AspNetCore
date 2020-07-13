using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LocationReports.Models;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace LocationReports.Services
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private HttpClient httpClient;
        private readonly ILogger logger;

        public HttpTeamServiceClient(IOptions<TeamServiceOptions> options, ILogger<HttpTeamServiceClient> logger)
        {
            this.logger = logger;
            var url = options.Value.Url;
            logger.LogInformation("团队服务HTTP客户端Url{0}",url);
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);

        }

        public Guid GetTeamForMember(Guid memberId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var requestUrl = string.Format("/members/{0}/team",memberId);
            var httpResponse =httpClient.GetAsync(requestUrl).Result;

            if (httpResponse.IsSuccessStatusCode)
            {
                string json =httpResponse.Content.ReadAsStringAsync().Result;
                var team = JsonConvert.DeserializeObject<TeamIDResponse>(json);
                return team.TeamID;

            }
            else
            {
                return Guid.Empty;
            }
        }

       
    }
    public class TeamIDResponse
    {
        public Guid TeamID { get; set; }
    }
}
