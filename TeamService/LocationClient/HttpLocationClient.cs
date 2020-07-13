using TeamService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TeamService.LocationClient
{
    public class HttpLocationClient : ILocationClient
    {
        private string URL { get; set; }
        public HttpLocationClient()
        {
            this.URL = ConfigurationManager.AppSettings["LocationClientUrl"];
        }
        public async Task<LocationRecord> GetLatestForMember(Guid meberId)
        {
            LocationRecord locationRecord = null;
            using (var httpClient=new HttpClient())
            {
                httpClient.BaseAddress = new Uri(this.URL);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                string requestUrl = string.Format("/locations/{0}/lastest", meberId);
                var response =await httpClient.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    locationRecord = JsonConvert.DeserializeObject<LocationRecord>(json);
                }
             
            }
            return locationRecord;
        }
    }
}
