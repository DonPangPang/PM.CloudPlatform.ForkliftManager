using NbazhGPS.Protocol;

#nullable disable

namespace PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps
{
    public class EV26Response
    {
        public NbazhGpsPackage Package { get; set; }

        public int MinBufferSize { get; set; }

        public EV26Response()
        {
        }

        public EV26Response(NbazhGpsPackage package, int minBufferSize = 1024)
        {
            Package = package;
            MinBufferSize = minBufferSize;
        }
    }
}