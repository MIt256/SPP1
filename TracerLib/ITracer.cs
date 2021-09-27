using System;
using System.Collections.Generic;
using System.Text;

namespace TracerFolder
{
    interface ITracer
    {
        void StartTrace();​
    
        void StopTrace();​
   
        TraceResult GetTraceResult();
    }
}
