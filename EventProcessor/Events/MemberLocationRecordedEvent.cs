using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Events
{
    
    public class MemberLocationRecordedEvent
    {
        public String Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberID { get; set; }
        public long RecordedTime { get; set; }
        public Guid ReportID { get; set; }
        public Guid TeamID { get; set; }

        public string toJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static MemberLocationRecordedEvent FromObject(string obj)
        {
            return JsonConvert.DeserializeObject<MemberLocationRecordedEvent>(obj);
        }
    }
}
