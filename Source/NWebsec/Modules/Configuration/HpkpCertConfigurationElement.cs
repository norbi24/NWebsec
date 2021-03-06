﻿// Copyright (c) André N. Klingsheim. See License.txt in the project root for license information.

using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using NWebsec.Core.Helpers.X509;
using NWebsec.Core.HttpHeaders.Configuration;
using NWebsec.Modules.Configuration.Validation;

namespace NWebsec.Modules.Configuration
{
    public class HpkpCertConfigurationElement : ConfigurationElement, IHpkpCertConfiguration
    {
        private string _spkiPin;

        [ConfigurationProperty("thumbprint", IsKey = true, IsRequired = true, DefaultValue = null)]
        [HpkpCertThumbprintValidator]
        public string ThumbPrint
        {
            get
            {
                return (string)this["thumbprint"];
            }
            set
            {
                this["thumbprint"] = value;
            }
        }

        [ConfigurationProperty("storeLocation", IsRequired = false, DefaultValue = StoreLocation.LocalMachine)]
        public StoreLocation StoreLocation
        {
            get
            {
                return (StoreLocation)this["storeLocation"];
            }
            set
            {
                this["storeLocation"] = value;
            }
        }

        [ConfigurationProperty("storeName", IsRequired = false, DefaultValue = StoreName.My)]
        public StoreName Storename
        {
            get
            {
                return (StoreName)this["storeName"];
            }
            set
            {
                this["storeName"] = value;
            }
        }

        //TODO have another look at this
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands")]
        public string SpkiPinValue {
            get
            {
                if (_spkiPin == null)
                {
                    var x509Helper = new X509Helper();
                    var cert = x509Helper.GetCertByThumbprint(ThumbPrint, StoreLocation, Storename);
                    _spkiPin = x509Helper.GetSubjectPublicKeyInfoPinValue(cert);
                    cert.Reset();
                }
                return _spkiPin;
            }
            set { throw new NotImplementedException();}
        }
    }
}