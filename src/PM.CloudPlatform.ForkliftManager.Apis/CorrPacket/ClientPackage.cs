using PM.CloudPlatform.ForkliftManager.Apis.Services;

namespace PM.CloudPlatform.ForkliftManager.Apis.CorrPacket
{
    public class ClientPackage
    {
        /// <summary>
        /// 包类型
        /// </summary>
        public PackageType PackageType { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string? VerifyCode { get; set; }

        /// <summary>
        /// 客户端Id(用户Id)
        /// </summary>
        public string ClientId { get; set; } = null!;

        /// <summary>
        /// 数据
        /// </summary>
        public object? Data { get; set; }
    }
}