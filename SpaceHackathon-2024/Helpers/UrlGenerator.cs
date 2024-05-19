namespace SpaceHackathon_2024.Helpers
{
    /// <summary>
    /// helper class to generate the api endpoints
    /// </summary>
    public class UrlGenerator
    {
        public static string VersionRoute()
            => $"{AppConstants.GatewayUrl}/version";

        public static class ExampleService1
        {
            public static string PostRouteExample1()
                => $"{AppConstants.GatewayUrl}/exampleapi/v2/Test/";

            public static string GetRouteExample1(string someId)
                => $"{AppConstants.GatewayUrl}/exampleapi/v2/Test/{someId}";
        }
    }
}
