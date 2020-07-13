using EventProcessor.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Queues
{
    public delegate void MemberLocationRecordedEventReceivedDelegate(MemberLocationRecordedEvent evt);
    /// <summary>
    /// 事件订阅
    /// </summary>
    public interface IEventSubscriber
    {
        /// <summary>
        /// 订阅
        /// </summary>
        void Subscribe();
        /// <summary>
        /// 取消订阅
        /// </summary>
        void Unsubscribe();
        /// <summary>
        /// 接收 事件（成员位置记录）
        /// </summary>
        event MemberLocationRecordedEventReceivedDelegate MemberLocationRecordedEventReceived;
    }
}
