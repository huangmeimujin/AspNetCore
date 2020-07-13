using EventProcessor.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Queues
{
    /// <summary>
    /// 事件发送器
    /// </summary>
    public interface IEventEmitter
    {
        /// <summary>
        /// 发送近距离探测事件
        /// </summary>
        /// <param name="proximityDetectedEvent"></param>
        void EmitProximityDetectedEvent(ProximityDetectedEvent proximityDetectedEvent);
    }
}
