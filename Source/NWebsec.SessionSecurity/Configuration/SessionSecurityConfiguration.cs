﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System.Web.Configuration;

namespace NWebsec.SessionSecurity.Configuration
{
    internal static class SessionSecurityConfiguration
    {
        private static readonly SessionSecurityConfigurationSection Cfg = (SessionSecurityConfigurationSection)WebConfigurationManager.GetSection("nwebsec/sessionSecurity") ?? new SessionSecurityConfigurationSection();

        internal static SessionSecurityConfigurationSection Configuration
        {
            get { return Cfg; }
        }
    }
}
