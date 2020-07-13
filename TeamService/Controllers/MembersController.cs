using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamService.Persistence;
using System.Linq;
using TeamService.Models;
using TeamService.LocationClient;

namespace TeamService.Controllers
{
    
    [Route("/teams/{teamId}/[controller]")]
    public class MembersController:Controller
    {
        private ITeamRepository TeamRepository;
        private ILocationClient LocationClient;
        public MembersController(ITeamRepository teamrepository,ILocationClient locationClient)
        {
            this.TeamRepository = teamrepository;
            this.LocationClient = locationClient;
        }
        [HttpGet("{memberId}")]
        public async virtual Task<IActionResult> GetMember(Guid teamId,Guid memberId)
        {
            var team = TeamRepository.Get(teamId);
            if (team==null)
            {
                return this.NotFound();
            }
            else
            {
                var q = team.Members.Where(m=>m.ID==memberId);
                if (q.Count()<1)
                {
                    return this.NotFound();
                }
                else
                {
                    Member member = q.First();
                    var locatedMember = new LocatedMember()
                    {
                        ID = member.ID,
                        FirstName = member.FirstName,
                        LastName = member.LastName,
                        LastLocation = await this.LocationClient.GetLatestForMember(memberId)

                    };
                    return this.Ok(locatedMember);
                }
            }

        }

        //获取成员所在的团队ID
        [HttpGet]
        [Route("/members/{memberId}/team")]
        public IActionResult GetMemberTeamId(Guid memberId)
        {
            Guid result = GetTeamIdForMember(memberId);
            if (result != Guid.Empty)
            {
                return this.Ok(new
                {
                    TeamID = result
                });
            }
            else
            {
                return this.NotFound();
            }
        }

        /// <summary>
        /// 获取成员所在的团队ID
        /// </summary>
        /// <param name="memberId"></param>
        /// <returns></returns>
        private Guid GetTeamIdForMember(Guid memberId)
        {
            var teams = TeamRepository.List();
            foreach (var t in teams)
            {
                var m= t.Members.FirstOrDefault(p=>p.ID==memberId);
                if (m != null)
                {
                    return t.ID;
                }
                   
            }
            return Guid.Empty;
        }
    }
}
