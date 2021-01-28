namespace Corp.Resources.Infrastructure
{
    public static class Endpoints
    {
        public static class Applications
        {
            public const int FloodingAlerter = 2000;
        }

        public static class Services
        {
            public const int DataAccessServicePort = 3000;
            public const int HubPort = 3002;
            public const int FloodingAlerterWorkflowPort = 3004;
        }
    }
}