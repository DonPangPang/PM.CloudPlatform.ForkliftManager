using System;
using System.Buffers;
using NbazhGPS.Protocol;
using NbazhGPS.Protocol.Enums;
using SuperSocket.ProtoBase;

namespace PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters
{
    public class EV26PipelineFilter7979 : BeginEndMarkPipelineFilter<NbazhGpsPackage>
    {
        private static byte[] _beginMark = new byte[] { 0x79, 0x79 };
        private static byte[] _endMark = new byte[] { 0x0D, 0x0A };

        private NbazhGpsSerializer _nbazhGpsSerializer;

        private IPipelineFilter<NbazhGpsPackage> SwitchFilter { get; }

        public EV26PipelineFilter7979(IPipelineFilter<NbazhGpsPackage> switcher) : base(_beginMark, _endMark)

        {
            SwitchFilter = switcher;
            _nbazhGpsSerializer = new NbazhGpsSerializer(false);
        }

        protected override NbazhGpsPackage DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            NextFilter = SwitchFilter;
            return _nbazhGpsSerializer.Deserialize(buffer.ToArray(), PackageType.Type2);
        }
    }
}