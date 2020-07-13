using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamService.Models;

namespace TeamService.LocationClient
{
    public interface ILocationClient
    {
        Task<LocationRecord> GetLatestForMember(Guid meberId);
    }
}
