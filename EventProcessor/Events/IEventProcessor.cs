using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventProcessor.Events
{
    public interface IEventProcessor
    {
        void Start();
        void Stop();
    }
}
