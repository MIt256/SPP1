namespace TracerLib.Serialization
{
    interface ISerializer
    {
        string Serialize(TraceResult traceResult);
    }
}
