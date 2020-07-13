using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationReports.Models
{
    public class LocationReport
    {
        public Guid ReportID { get; set; }
        public String Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberID { get; set; }
    }
}
