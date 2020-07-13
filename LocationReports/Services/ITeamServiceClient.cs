using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationReports.Services
{
    public interface ITeamServiceClient
    {
        Guid GetTeamForMember(Guid memberId);
    }
}
