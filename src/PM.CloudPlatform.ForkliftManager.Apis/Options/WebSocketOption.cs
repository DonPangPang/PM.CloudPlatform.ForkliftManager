using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace PM.CloudPlatform.ForkliftManager.Apis.Options
{
    public class WebSocketOption : IOptions<WebSocketOption>
    {
        public WebSocketOption Value => this;

        public string VerifyCode { get; set; } = null!;
    }
}