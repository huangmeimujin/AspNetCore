using LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Repositorys
{
    public interface ILocationRecordRepository
    {
        LocationRecord Add(LocationRecord record);
        LocationRecord Update(LocationRecord record);

        LocationRecord Delete(Guid memberId,Guid recordId);
        LocationRecord GetLatestForMember(Guid memberId);
        ICollection<LocationRecord> AllForMember(Guid memberId);
        LocationRecord Get(Guid memberId,Guid recordId);
    }
}
