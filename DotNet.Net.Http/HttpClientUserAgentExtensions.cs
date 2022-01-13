using System.Net.Http.Headers;

namespace System.Net.Http
{
    public static class HttpClientUserAgentExtensions
    {
        public static void Add(this HttpRequestHeaders headers, UserAgent userAgent)
        {
            if (userAgent == null)
                throw new ArgumentNullException(nameof(userAgent));

            headers.Add("User-Agent", userAgent.ToString());
        } 
    }
}