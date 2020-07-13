using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LocationService.Repositorys;
using LocationService.Models;

namespace LocationService.Controller
{
    [Route("locations/{memberId}")]
    public class LocationRecordController : ControllerBase
    {
        private ILocationRecordRepository Repository;

        public LocationRecordController(ILocationRecordRepository repository)
        {
            this.Repository = repository;
        }

        [HttpPost]
        public IActionResult AddLocation([FromBody] LocationRecord record,Guid memberId)
        {
            Repository.Add(record);
            return this.Created($"/locations/{memberId}/{record.ID}",record);
        }
        [HttpGet]
        public IActionResult GetLocationsForMember(Guid memberId)
        {
            return this.Ok(Repository.AllForMember(memberId));
        }
        [HttpGet("latest")]
        public IActionResult GetLatestForMember(Guid memberId)
        {
            return this.Ok(Repository.GetLatestForMember(memberId));

        }
    }
}
