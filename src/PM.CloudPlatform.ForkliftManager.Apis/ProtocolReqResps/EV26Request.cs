using NbazhGPS.Protocol;

namespace PM.CloudPlatform.ForkliftManager.Apis.ProtocolReqResps
{
    public class EV26Request
    {
        public NbazhGpsPackage Package { get; set; }

        public byte[] OriginalPackage { get; }

        public EV26Request(NbazhGpsPackage package, byte[] originalPackage)
        {
            Package = package;
            OriginalPackage = originalPackage;
        }
    }
}