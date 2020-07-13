using LocationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Repositorys
{
    public class InMemoryLacationRecordRepository : ILocationRecordRepository
    {
        private static Dictionary<Guid, SortedList<long, LocationRecord>> LocationRecords;
        public InMemoryLacationRecordRepository()
        {
            if(LocationRecords == null) 
                LocationRecords = new Dictionary<Guid, SortedList<long, LocationRecord>>();
        }
        public LocationRecord Add(LocationRecord record)
        {
            var memberRecords = this.GetMemberRecords(record.MemberID);
            memberRecords.Add(record.Timestamp,record);
            return record;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            var memberRecords = GetMemberRecords(memberId);
            return memberRecords.Values.Where(l => l.MemberID == memberId).ToList();
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            var memberRecords = GetMemberRecords(memberId);
            LocationRecord lr = memberRecords.Values.Where(l => l.ID == recordId).FirstOrDefault();

            if (lr != null)
            {
                memberRecords.Remove(lr.Timestamp);
            }
            return lr;
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            var memberRecords = GetMemberRecords(memberId);

            LocationRecord lr = memberRecords.Values.LastOrDefault();
            return lr;
        }

        public LocationRecord Update(LocationRecord record)
        {
            return Delete(record.MemberID, record.ID);
        }

        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            var memberRecords = GetMemberRecords(memberId);

            LocationRecord lr = memberRecords.Values.Where(l => l.ID == recordId).FirstOrDefault();
            return lr;
        }
        private SortedList<long,LocationRecord> GetMemberRecords(Guid memberId)
        {
            if (!LocationRecords.ContainsKey(memberId))
            {
                LocationRecords.Add(memberId, new SortedList<long, LocationRecord>());
            }
            return LocationRecords[memberId];
        }


       
    }
}
