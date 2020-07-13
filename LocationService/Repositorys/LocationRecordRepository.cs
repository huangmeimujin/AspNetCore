using LocationService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationService.Repositorys
{
    public class LocationRecordRepository : ILocationRecordRepository
    {
        private LocationDbContext DbContext;
        public LocationRecordRepository(LocationDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
        public LocationRecord Add(LocationRecord record)
        {
            this.DbContext.Add(record);
            this.DbContext.SaveChanges();
            return record;
        }

        public ICollection<LocationRecord> AllForMember(Guid memberId)
        {
            var result = this.DbContext.locationRecords.AsParallel().WithExecutionMode( ParallelExecutionMode.ForceParallelism)
                .Where(p => p.MemberID == memberId)
                .OrderBy(o => o.Timestamp).ToList();
            return result;
        }

        public LocationRecord Delete(Guid memberId, Guid recordId)
        {
            var locationRecord = this.Get(memberId, recordId);
            this.DbContext.Remove(locationRecord);
            this.DbContext.SaveChanges();
            return locationRecord;
        }
        
        public LocationRecord Get(Guid memberId, Guid recordId)
        {
            return this.DbContext.locationRecords.Single(p=>p.MemberID==memberId && p.ID==recordId);
        }

        public LocationRecord GetLatestForMember(Guid memberId)
        {
            var locationRecord = this.DbContext.locationRecords
                .Where(p => p.MemberID == memberId)
                .OrderBy(o => o.Timestamp)
                .Last();
            return locationRecord;
        }

        public LocationRecord Update(LocationRecord record)
        {
            this.DbContext.Entry<LocationRecord>(record).State = EntityState.Modified;
            this.DbContext.SaveChanges();
            return record;


        }
    }
}
