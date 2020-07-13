using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Location.Redis
{
    public class RedisLocationCache : ILocationCache
    {
        public IList<MemberLocation> GetMemberLocations(Guid teamId)
        {
            throw new NotImplementedException();
        }

        public void Put(Guid teamId, MemberLocation memberLocation)
        {
            throw new NotImplementedException();
        }
    }
}
