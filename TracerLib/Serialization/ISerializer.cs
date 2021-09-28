using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization
{
    interface ISerializer
    {
        string Serialize(TraceResult traceResult);
    }
}
