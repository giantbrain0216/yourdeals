#region

using System.Collections.Generic;

#endregion

namespace NPComplet.YourDeals.Server.Infrastructure.Services.ConfigurationSettings
{
    public class CysberSorurceFiananceGateWaySetting
    {
        private readonly Dictionary<string, string> _configurationDictionary = new();
        public string CyberSourceApiUrl { get; set; }


        public string CyberSourceApiurlAction { get; set; }

        public string CyberSourceMerchantID { get; set; }


        /// <summary>
        ///     example yBJxy6LjM2TmcPGu+GaJrHtkke25fPpUX+UY6/L/1tE=
        /// </summary>
        public string CyberSourceMerchantSecretKey { get; set; }

        public string CyberSourceMerchantKeyId { get; set; }
        public string CyberSourceAuthenticationType { get; set; }
        public string CyberSourceKeysDirectory { get; set; }
        public string CyberSourceKeyFilename { get; set; }
        public string CyberSourceRunEnvironment { get; set; }

        public string CyberSourceKeyAlias { get; set; }
        public string CyberSourcekeyPass { get; set; }
        public string CyberSourceEnableLog { get; set; }

        public string CyberSourceLogDirectory { get; set; } = string.Empty;

        public string CyberSourceLogFileName { get; set; } = string.Empty;

        public string CyberSourceLogFileMaxSize { get; set; } = "5242880";
        public string CyberSourceTimeout { get; set; } = "1000";
        public string CyberSourceProxyAddress { get; set; } = string.Empty;
        public string CyberSourceProxyPort { get; set; } = string.Empty;

        public Dictionary<string, string> GetConfiguration()
        {
            _configurationDictionary.Add("merchantID", CyberSourceMerchantID);
            _configurationDictionary.Add("merchantsecretKey", CyberSourceMerchantSecretKey);
            _configurationDictionary.Add("merchantKeyId", CyberSourceMerchantKeyId);
            _configurationDictionary.Add("authenticationType", CyberSourceAuthenticationType);
            _configurationDictionary.Add("keysDirectory", CyberSourceKeysDirectory);
            _configurationDictionary.Add("keyFilename", CyberSourceKeyFilename);
            _configurationDictionary.Add("runEnvironment", CyberSourceRunEnvironment);
            _configurationDictionary.Add("keyAlias", CyberSourceKeyAlias);
            _configurationDictionary.Add("keyPass", CyberSourcekeyPass);
            _configurationDictionary.Add("enableLog", CyberSourceEnableLog);
            _configurationDictionary.Add("logDirectory", CyberSourceLogDirectory);
            _configurationDictionary.Add("logFileName", CyberSourceLogFileName);
            _configurationDictionary.Add("logFileMaxSize", CyberSourceLogFileMaxSize);
            _configurationDictionary.Add("timeout", CyberSourceTimeout);
            _configurationDictionary.Add("proxyAddress", CyberSourceProxyAddress);
            _configurationDictionary.Add("proxyPort", CyberSourceProxyPort);

            return _configurationDictionary;
        }
    }
}