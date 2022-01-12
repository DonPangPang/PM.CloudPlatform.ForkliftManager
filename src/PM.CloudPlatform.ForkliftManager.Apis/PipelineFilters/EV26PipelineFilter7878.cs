using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NbazhGPS.Protocol;
using SuperSocket.ProtoBase;

namespace PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters
{
    public class EV26PipelineFilter7878 : BeginEndMarkPipelineFilter<NbazhGpsPackage>
    {
        private static byte[] _beginMark = new byte[] { 0x78, 0x78 };
        private static byte[] _endMark = new byte[] { 0x0D, 0x0A };

        private IPipelineFilter<NbazhGpsPackage> SwitchFilter { get; }
        private NbazhGpsSerializer _nbazhGpsSerializer;

        public EV26PipelineFilter7878(IPipelineFilter<NbazhGpsPackage> switcher) : base(_beginMark, _endMark)

        {
            SwitchFilter = switcher;
            _nbazhGpsSerializer = new NbazhGpsSerializer(false);
        }

        protected override NbazhGpsPackage DecodePackage(ref ReadOnlySequence<byte> buffer)
        {
            NextFilter = SwitchFilter;
            //return base.DecodePackage(ref buffer);

            //var buff = buffer.ToArray();
            //byte[] data = new byte[buffer.Length + 3];
            //BitConverter.GetBytes((byte)Protocol.JT808).CopyTo(data, 0);
            //BeginMark.CopyTo(data, 1);
            //buff.CopyTo(data, 2);
            //EndMark.CopyTo(data, data.Length - 1);

            return _nbazhGpsSerializer.Deserialize(buffer.ToArray());
        }
    }
}