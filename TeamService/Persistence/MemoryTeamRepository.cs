using TeamService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TeamService.Persistence
{
    public class MemoryTeamRepository : ITeamRepository
    {
        protected static ICollection<Team> Teams;
        
        public MemoryTeamRepository()
        {
            if(Teams == null)
            {
                Teams = new List<Team>();
            }
        }
        public MemoryTeamRepository(ICollection<Team> teams)
        {
            Teams = teams;
        }
       

        public IEnumerable<Team> List()
        {
            return Teams;
        }

        public Team Get(Guid id)
        {
            return Teams.FirstOrDefault(p=>p.ID==id);
        }

        public Team Add(Team team)
        {
            Teams.Add(team);
            return team;
        }
        public Team Update(Team t)
        {
            Team team = this.Delete(t.ID);
            if(team!=null)
            {
                team=this.Add(t);
                
            }
            return team;
        }

        public Team Delete(Guid id)
        {
            var q = Teams.Where(p => p.ID == id);
            Team team = null;
            if (q.Count()>0)
            {
                team = q.First();
                Teams.Remove(team);
            }
            return team;
        }
    }
}
