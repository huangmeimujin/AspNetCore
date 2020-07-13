using EventProcessor.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Events
{
    /// <summary>
    /// 基于GPS工具类的检测器
    /// </summary>
    public class ProximityDetector
    {
        public ICollection<ProximityDetectedEvent> DetectProximityEvents(
            MemberLocationRecordedEvent memberLocationEvent,
            ICollection<MemberLocation> memberLocations,
            double distanceThreshold)
        {
            GpsUtility gpsUtility = new GpsUtility();
            GpsCoordinate sourceCoordinate = new GpsCoordinate()
            {
                Latitude = memberLocationEvent.Latitude,
                Longitude = memberLocationEvent.Longitude
            };

            return memberLocations.Where(
                     ml => ml.MemberID != memberLocationEvent.MemberID &&
                     gpsUtility.DistanceBetweenPoints(sourceCoordinate, ml.Location) < distanceThreshold)
               .Select(ml => {
                   return new ProximityDetectedEvent()
                   {
                       SourceMemberID = memberLocationEvent.MemberID,
                       TargetMemberID = ml.MemberID,
                       TeamID = memberLocationEvent.TeamID,
                       DetectionTime = DateTime.UtcNow.Ticks,
                       SourceMemberLocation = sourceCoordinate,
                       TargetMemberLocation = ml.Location,
                       MemberDistance = gpsUtility.DistanceBetweenPoints(sourceCoordinate, ml.Location)
                   };
               }).ToList();
        }
    }
}
