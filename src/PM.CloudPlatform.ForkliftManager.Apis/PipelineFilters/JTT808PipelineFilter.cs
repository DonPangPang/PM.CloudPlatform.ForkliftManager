using SuperSocket.ProtoBase;
using System.Buffers;

namespace PM.CloudPlatform.ForkliftManager.Apis.PipelineFilters
{
    ///// <summary>
    /////
    ///// </summary>
    //public class JTT808PipelineFilter : BeginEndMarkPipelineFilter<byte[]>
    //{
    //    private static readonly byte[] BeginMark = new byte[] { 0x7E };
    //    private static readonly byte[] EndMark = new byte[] { 0x7E };

    //    /// <summary>
    //    /// JT808序列化器
    //    /// </summary>
    //    public JT808Serializer Jt808Serializer;

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    public JTT808PipelineFilter() : base(BeginMark, EndMark)
    //    {
    //        Jt808Serializer = new JT808Serializer();
    //    }

    //    /// <summary>
    //    ///
    //    /// </summary>
    //    /// <param name="buffer"></param>
    //    /// <returns></returns>
    //    protected override byte[] DecodePackage(ref ReadOnlySequence<byte> buffer)
    //    {
    //        var buff = buffer.ToArray();
    //        byte[] data = new byte[buffer.Length + 2];
    //        BeginMark.CopyTo(data, 0);
    //        buff.CopyTo(data, 2);
    //        EndMark.CopyTo(data, data.Length - 1);

    //        return data;
    //    }
    //}
}