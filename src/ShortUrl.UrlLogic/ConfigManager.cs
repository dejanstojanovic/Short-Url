using System.Configuration;

namespace ShortUrl.Logic
{
    public static class ConfigManager
    {
        public static bool CheckUrlAvailability
        {
            get
            {
                bool check;
                if (!bool.TryParse(ConfigurationManager.AppSettings["CheckUrlAvailability"], out check))
                {
                    check = false;
                }
                return check;
            }
        }

        public static int CheckUrlAvailabilityTimeout
        {
            get
            {
                int timeout;
                if (!int.TryParse(ConfigurationManager.AppSettings["CheckUrlAvailabilityTimeout"], out timeout))
                {
                    timeout = 5;
                }
                return timeout;
            }
        }

        public static int KeyLength
        {
            get
            {
                int keyLength;
                if (!int.TryParse(ConfigurationManager.AppSettings["KeyLength"], out keyLength))
                {
                    keyLength = 6;
                }
                return keyLength;
            }
        }

        public static int CacheTimeout
        {
            get
            {
                int cacheTimeout;
                if (!int.TryParse(ConfigurationManager.AppSettings["CaheTimeout"], out cacheTimeout))
                {
                    cacheTimeout = 5;
                }
                return cacheTimeout;
            }
        }
    }
}
