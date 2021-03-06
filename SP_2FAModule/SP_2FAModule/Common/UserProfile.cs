namespace SP_2FAModule.Common
{
    public class UserProfile
    {
        public string authenticateId { get; set; }

        public string userId { get; set; }

        public string userName { get; set; }

        public string windowsSessionId { get; set; }

        public string twoFaSessionId { get; set; }

        public string startTime { get; set; }

        public string expiryTime { get; set; }

        public string startDate { get; set; }

        public string expiryDate { get; set; }

        public string webUrl { get; set; }

        public string browserType { get; set; }

        public string status { get; set; }

        public string machineInfo { get; set; }

        public string machineIp { get; set; }

        public int isActive { get; set; }
    }
}
