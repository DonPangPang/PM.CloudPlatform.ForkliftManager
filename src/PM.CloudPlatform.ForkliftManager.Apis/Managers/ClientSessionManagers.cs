﻿using PM.CloudPlatform.ForkliftManager.Apis.Sessions;

namespace PM.CloudPlatform.ForkliftManager.Apis.Managers
{
    public class ClientSessionManagers : SessionManager<ClientSession>
    {
        public bool IsTrace{get; set;} = false;
        public string TraceTerminalId{get; set;} = "";
    }
}