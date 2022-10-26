using Cake.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Http
{
    /// <summary>
    /// Contains functionality related to HTTP settings.
    /// </summary>
    public static class HttpSettingsExtensions
    {
        internal const string BoundaryPrefix = "-----6fd9070b8b1b5ba49564b8fff7b7784ea0cdf096";

        /// <summary>
        /// Appends the header to the settings header collection
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="name">name of the header</param>
        /// <param name="value">value to apply to the header</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings AppendHeader(this HttpSettings settings, string name, string value)
        {
            VerifyParameters(settings, name, value);

            settings.Headers[name] = value;

            return settings;
        }

        /// <summary>
        /// Appends the cookie to the settings cookie collection
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="name">name of the cookie</param>
        /// <param name="value">value to cookie</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings AppendCookie(this HttpSettings settings, string name, string value)
        {
            VerifyParameters(settings, name, value);

            settings.Cookies[name] = value;

            return settings;
        }

        /// <summary>
        /// Adds a Authorization Header to the request.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="schema">The scheme to use for authorization.</param>
        /// <param name="parameter">The credentials containing the authentication information of the user agent for</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetAuthorization(this HttpSettings settings, string schema, string parameter)
        {
            if (string.IsNullOrWhiteSpace(schema))
                throw new ArgumentNullException(nameof(schema));

            if (string.IsNullOrWhiteSpace(parameter))
                throw new ArgumentNullException(nameof(parameter));

            return settings.AppendHeader("Authorization", $"{schema} {parameter}");
        }

        /// <summary>
        /// Adds a "Basic" Authorization Header to the request.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="userName">The userName used to authorize to the resource.</param>
        /// <param name="password">The credentials containing the authentication information of the user agent for</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings UseBasicAuthorization(this HttpSettings settings, string userName = "", string password = "") =>
            settings.SetAuthorization("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password)));

        /// <summary>
        /// Adds a "Bearer" Token Authorization Header to the request.
        /// Used when authorization against a resource that uses OAUTH2
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="token">Token to apply to the header</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings UseBearerAuthorization(this HttpSettings settings, string token = "") =>
            settings.SetAuthorization("Bearer", token);

        /// <summary>
        /// Sets the content-type of the request
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="contentType">The MIME type of the body of the request (used with POST and PUT requests).</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetContentType(this HttpSettings settings, string contentType)
        {
            if (string.IsNullOrWhiteSpace(contentType))
                throw new ArgumentNullException(nameof(contentType));

            return settings.AppendHeader("Content-Type", contentType);
        }

        /// <summary>
        /// Sets the accept header of the request.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="accept">Content-Types that are acceptable for the response. See Content negotiation.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetAccept(this HttpSettings settings, string accept)
        {
            if (string.IsNullOrWhiteSpace(accept))
                throw new ArgumentNullException(nameof(accept));

            return settings.AppendHeader("Accept", accept);
        }

        /// <summary>
        /// Sets the "Accept-Language" header of the request.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="acceptLanguage">List of acceptable human languages for response.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetAcceptLanguage(this HttpSettings settings, string acceptLanguage)
        {
            if (string.IsNullOrWhiteSpace(acceptLanguage))
                throw new ArgumentNullException(nameof(acceptLanguage));

            return settings.AppendHeader("Accept-Language", acceptLanguage);
        }

        /// <summary>
        /// Appends a Cache-Control header with no-store
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetNoCache(this HttpSettings settings) => settings.AppendHeader("Cache-Control", "no-store");

        /// <summary>
        /// Sets the origin header to initiate a request for cross-origin resource sharing (asks server for an 'Access-Control-Allow-Origin' response field).
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="url">the url to apply to the "origin" header</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetOrigin(this HttpSettings settings, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return settings.AppendHeader("Origin", url);
        }

        /// <summary>
        /// Sets the "Referer" header of the address of the previous web page from which a link to the currently requested page was followed.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="url">the url to apply to the "refer" header</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetReferer(this HttpSettings settings, string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException(nameof(url));

            return settings.AppendHeader("Referer", url);
        }

        /// <summary>
        /// Sets the request body from a string input
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="requestBody">The string to set as the request body.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetRequestBody(this HttpSettings settings, string requestBody)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(requestBody))
                throw new ArgumentNullException(nameof(requestBody));

            settings.RequestBody = Encoding.UTF8.GetBytes(requestBody);

            return settings;
        }

        /// <summary>
        /// Sets the request body from an object. Serialized as JSON
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">The object to set as the request body. It will be serialized to JSON.</param>
        /// <param name="indentOutput">Option to indent the output of the format</param>
        /// <remarks>
        /// This uses the JavascriptSerializer
        /// </remarks>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetJsonRequestBody<T>(this HttpSettings settings, T data, bool indentOutput = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var requestBody = JsonEncoder.SerializeObject(data);

            settings.RequestBody = Encoding.UTF8.GetBytes(requestBody);
            settings.SetContentType("application/json");
            return settings;
        }

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overridden.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Dictionary of data to url encode and set to the body.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetFormUrlEncodedRequestBody(this HttpSettings settings, IDictionary<string, string> data)
            => SetFormUrlEncodedRequestBody(settings, data?.AsEnumerable());

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overridden.
        /// Accepts multiple parameters with the same key.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Enumerable of <see cref="KeyValuePair{TKey,TValue}"/> of data to url encode and set to the body.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetFormUrlEncodedRequestBody(this HttpSettings settings, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            settings.RequestBody = Task.Run(async () => await new FormUrlEncodedContent(data).ReadAsByteArrayAsync()).ConfigureAwait(false).GetAwaiter().GetResult();
            settings.SetContentType("application/x-www-form-urlencoded");

            return settings;
        }

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overridden.
        /// Accepts multiple parameters with the same key.
        ///This can be used to post files to a remote URL
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Enumerable of <see cref="KeyValuePair{TKey,TValue}"/> of data to url encode and set to the body.</param>
        /// <param name="filePaths">Enumerable of <see cref="FilePath"/> of file paths to post.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetMultipartFormDataRequestBody(this HttpSettings settings, IEnumerable<KeyValuePair<string, string>> data, IEnumerable<FilePath> filePaths = null)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var multipart = new MultipartFormDataContent(HttpSettingsExtensions.BoundaryPrefix);

            foreach (var kvp in data)
                multipart.Add(new StringContent(kvp.Value), kvp.Key);

            if (filePaths != null && filePaths.Any())
            {
                foreach (var filePath in filePaths)
                    if (filePath != null)
                        multipart.Add(new StreamContent(File.OpenRead(filePath.FullPath)), "file", filePath.GetFilename().ToString());
            }

            settings.RequestBody = Task.Run(async () => await multipart.ReadAsByteArrayAsync()).ConfigureAwait(false).GetAwaiter().GetResult();
            settings.SetContentType($"multipart/form-data; boundary={HttpSettingsExtensions.BoundaryPrefix}");

            return settings;
        }

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overridden.
        /// Accepts multiple parameters with the same key.
        /// This can be used to post files to a remote URL
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Enumerable of <see cref="KeyValuePair{TKey,TValue}"/> of data to include in the post</param>
        /// <param name="filePaths">Enumerable of <see cref="KeyValuePair{TKey,TValue}"/> of file paths and associated names.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetMultipartFormDataRequestBody(this HttpSettings settings, IEnumerable<KeyValuePair<string, string>> data, IEnumerable<KeyValuePair<string, FilePath>> filePaths)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            var multipart = new MultipartFormDataContent(HttpSettingsExtensions.BoundaryPrefix);

            foreach (var kvp in data)
                multipart.Add(new StringContent(kvp.Value), kvp.Key);

            if (filePaths != null && filePaths.Any())
            {
                foreach (var filePath in filePaths)
                    if (filePath.Value != null && !string.IsNullOrWhiteSpace(filePath.Key))
                        multipart.Add(new StreamContent(File.OpenRead(filePath.Value.FullPath)), filePath.Key, filePath.Value.GetFilename().ToString());
            }

            settings.RequestBody = Task.Run(async () => await multipart.ReadAsByteArrayAsync()).ConfigureAwait(false).GetAwaiter().GetResult();
            settings.SetContentType($"multipart/form-data; boundary={HttpSettingsExtensions.BoundaryPrefix}");

            return settings;
        }

        /// <summary>
        /// Sets the EnsureSuccessStatusCode to true. This makes the httpclient throw an error if it does not return a 200 range status.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="throwExceptionOnNonSuccessStatusCode">Determines if an exception is thrown on non-success code.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings EnsureSuccessStatusCode(this HttpSettings settings, bool throwExceptionOnNonSuccessStatusCode = true)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            settings.EnsureSuccessStatusCode = true;
            settings.ThrowExceptionOnNonSuccessStatusCode = throwExceptionOnNonSuccessStatusCode;

            return settings;
        }

        /// <summary>
        ///  Adds client certificate(s) to the http handler.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="clientCertificates">Client certificates to include in requests.</param>
        /// <returns></returns>
        public static HttpSettings UseClientCertificates(this HttpSettings settings, params X509Certificate2[] clientCertificates)
        {
            return settings.UseClientCertificates((IEnumerable<X509Certificate2>)clientCertificates);
        }

        /// <summary>
        ///  Adds client certificate(s) to the http handler.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="clientCertificates">Client certificates to include in requests.</param>
        /// <returns></returns>
        public static HttpSettings UseClientCertificates(this HttpSettings settings, IEnumerable<X509Certificate2> clientCertificates)
        {
            if (clientCertificates == null)
                throw new ArgumentNullException(nameof(clientCertificates));

            foreach (var clientCertificate in clientCertificates)
                settings.ClientCertificates.Add(clientCertificate);

            return settings;
        }

        /// <summary>
        /// Sets the timeout for the http request
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="timeout">Timeout to set in the http request</param>
        /// <returns></returns>
        public static HttpSettings SetTimeout(this HttpSettings settings, TimeSpan timeout)
        {
            settings.Timeout = timeout;
            return settings;
        }

        /// <summary>
        /// Supresses logging to the console
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns></returns>
        public static HttpSettings SuppressLogResponseRequestBodyOutput(this HttpSettings settings)
        {
            settings.LogRequestResponseOutput = false;
            return settings;
        }

        private static void VerifyParameters(HttpSettings settings, string name, string value)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}