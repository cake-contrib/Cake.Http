using System.Collections.Generic;

namespace Cake.Http
{
    /// <summary>
    /// Http Settings to apply to the request
    /// </summary>
    public class HttpSettings
    {
        /// <summary>
        /// Http Settings to apply to the request
        /// </summary>
        public HttpSettings()
        {
            Headers = new Dictionary<string, string>();
            Cookies = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets or Sets the Body of the Request 
        /// </summary>
        /// <remarks>
        /// This is only valid for http operations such as POST or PUT.
        /// Other Operations such as GET will ignore this setting.
        /// </remarks>
        public byte[] RequestBody { get; set; }

        /// <summary>
        /// Collection of headers to append to the http request
        /// </summary>
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        /// Collection of 'Cookie(s)' to append to the http request.
        /// </summary>
        public IDictionary<string, string> Cookies { get; set; }

        /// <summary>
        /// Gets or sets a value that controls whether default credentials are sent with requests by the handler
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// Gets or Sets whether to throw an exception if the returned response is not a Successful Status Code
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; }
    }
}