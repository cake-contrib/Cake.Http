using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

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
            ClientCertificates = new List<X509Certificate2>();
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

        /// <summary>
        /// Gets or Sets where an exception is thrown on non success code.
        /// This is used in conjunction with EnsureSuccessStatusCode.
        /// </summary>
        public bool ThrowExceptionOnNonSuccessStatusCode { get; set; }

        /// <summary>
        /// List of Client Certificates to be enclosed with the http request.
        /// </summary>
        public IList<X509Certificate2> ClientCertificates { get; set; }

        /// <summary>
        /// Time out for http request, default is 100 seconds reference https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclient.timeout?view=netcore-3.1
        /// </summary>
        public TimeSpan? Timeout { get; set; }
    }
}
