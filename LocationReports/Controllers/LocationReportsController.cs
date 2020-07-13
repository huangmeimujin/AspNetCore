using LocationReports.Events;
using LocationReports.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LocationReports.Services;

namespace LocationReports.Controllers
{
    [Route("/api/members/{memberId}/locationreports")]
    public class LocationReportsController :Controller
    {
        private ICommandEventConverter converter;
        private IEventEmitter eventEmitter;
        private ITeamServiceClient teamServiceClient;

        public LocationReportsController(ICommandEventConverter converter,IEventEmitter eventEmitter,ITeamServiceClient teamServiceClient)
        {
            this.converter = converter;
            this.eventEmitter = eventEmitter;
            this.teamServiceClient = teamServiceClient;
        }
        [HttpPost]
        public ActionResult PostLocationReport([FromBody]LocationReport locationReport,Guid memberId)
        {
            var locationRecordedEvent = converter.CommandToEvent(locationReport);
            //locationRecordedEvent.TeamID = teamServiceClient.GetTeamForMember(memberId);
            eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreports/{locationReport.ReportID}",locationReport);
        }
    }
}
