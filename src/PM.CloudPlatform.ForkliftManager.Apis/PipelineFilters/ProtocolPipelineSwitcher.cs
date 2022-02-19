using System;
using System.Buffers;
using NbazhGPS.Protocol;
using SuperSocket.ProtoBase;

#nullable disable

namespace PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters
{
    public class ProtocolPipelineSwitcher : PipelineFilterBase<NbazhGpsPackage>
    {
        private IPipelineFilter<NbazhGpsPackage> _filter7878;
        private byte _beginMarkA = 0x78;

        private IPipelineFilter<NbazhGpsPackage> _filter7979;
        private byte _beginMarkB = 0x79;

        public ProtocolPipelineSwitcher()
        {
            _filter7878 = new EV26PipelineFilter7878(this);
            _filter7979 = new EV26PipelineFilter7979(this);
        }

        public override NbazhGpsPackage Filter(ref SequenceReader<byte> reader)
        {
            if (!reader.TryRead(out byte flag))
            {
                throw new ProtocolException(@"flag byte cannot be read"); 
            }

            if (flag == _beginMarkA)
            {
                NextFilter = _filter7878;
            }
            else if (flag == _beginMarkB)
            {
                NextFilter = _filter7979;
            }
            else
            {
                return null;
                //throw new ProtocolException($"首字节未知 {flag}");
            }

            reader.Rewind(1);
            return null;
        }
    }
}