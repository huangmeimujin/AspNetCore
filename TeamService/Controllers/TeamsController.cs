using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeamService.Models;
using TeamService.Persistence;

namespace TeamService.Controllers
{
    [Route("[controller]")]
    public class TeamsController:Controller
    {
        ITeamRepository Repository;
        public TeamsController(ITeamRepository repository)
        {
            this.Repository = repository;
        }
        [HttpGet]
        public virtual IActionResult  GetAllTeams()
        {

            return this.Ok(Repository.List());
        }
        [HttpGet("{id}")]
        public IActionResult GetTeam(Guid id)
        {
            var team = Repository.Get(id);
            if (team==null)
            {
                return this.NotFound();
            }
            return this.Ok(team);
        }

        [HttpPost]
        public virtual IActionResult CreateTeam([FromBody]Team newteam)
        {
            Repository.Add(newteam);
            return this.Created($"Team/{newteam.ID}", newteam);
        }
        [HttpPut("{id}")]
        public virtual IActionResult UpdateTeam([FromBody]Team newteam,Guid id)
        {
            newteam.ID = id;
            if(Repository.Update(newteam)==null)
            {
                return this.NotFound();
            }
            return this.Ok(newteam);
        }
        [HttpDelete("{id}")]
        public virtual IActionResult DeleteTeam(Guid id)
        {
            var team = Repository.Delete(id);
            if (team == null)
                return this.NotFound();
            else
                return this.Ok(team);
        }
    }
}