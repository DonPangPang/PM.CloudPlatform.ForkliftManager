using PM.CloudPlatform.ForkliftManager.Apis.Services;

namespace PM.CloudPlatform.ForkliftManager.Apis.CorrPacket
{
    public class ClientPackage
    {
        public PackageType PackageType { get; set; }

        public string? VerifyCode { get; set; }

        public string ClientId { get; set; } = null!;
        public object? Data { get; set; }
    }
}