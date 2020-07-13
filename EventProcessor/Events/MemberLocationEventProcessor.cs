using EventProcessor.Location;
using EventProcessor.Queues;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Events
{
    /// <summary>
    /// 事件处理
    /// </summary>
    public class MemberLocationEventProcessor : IEventProcessor
    {
        private ILogger logger;
        private IEventSubscriber subscriber;

        private IEventEmitter eventEmitter;

        private ProximityDetector proximityDetector;

        private ILocationCache locationCache;
        public MemberLocationEventProcessor(ILogger<MemberLocationEventProcessor> logger,
            IEventSubscriber subscriber,
            IEventEmitter emitter, 
            ProximityDetector proximityDetector,
            ILocationCache locationCache)
        {
            this.logger = logger;
            this.subscriber = subscriber;
            this.eventEmitter = emitter;
            this.proximityDetector = proximityDetector;
            this.locationCache = locationCache;

            this.subscriber.MemberLocationRecordedEventReceived +=(mlre) =>{ 
                var memberLocations = locationCache.GetMemberLocations(mlre.TeamID);
                ICollection<ProximityDetectedEvent> proximityDetectedEvents = proximityDetector.DetectProximityEvents(mlre,memberLocations,30.0f);
                foreach (var proximityEvent in proximityDetectedEvents)
                {
                    eventEmitter.EmitProximityDetectedEvent(proximityEvent);
                }

                locationCache.Put(mlre.TeamID, new MemberLocation
                {
                    MemberID = mlre.MemberID,
                    Location = new GpsCoordinate
                    {
                        Latitude = mlre.Latitude,
                        Longitude = mlre.Longitude
                    }
                });



            };

        }

        public void Start()
        {
            this.subscriber.Subscribe();
        }

        public void Stop()
        {
            this.subscriber.Unsubscribe();
        }
    }
}
