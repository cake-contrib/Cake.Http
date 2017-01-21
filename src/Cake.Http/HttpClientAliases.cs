using Cake.Core;
using Cake.Core.Annotations;
using System;
using System.Net.Http;
using System.Text;

namespace Cake.Http
{
    /// <summary>
    /// <para>
    /// Contains functionality for working with the HTTP operations such as GET, PUT, POST, DELETE, etc.
    /// </para>
    /// </summary>
    [CakeAliasCategory("Http")]
    [CakeNamespaceImport("Cake.Http")]
    public static class HttpClientAliases
    {
        #region Get Methods

        /// <summary>
        /// GETS the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the request as a byte array.</returns>
        [CakeMethodAlias]
        public static byte[] HttpGetAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var client = GetHttpClient(context, settings);
            var result = client.GetByteArrayAsync(address).Result;

            return result;
        }

        /// <summary>
        /// GETS the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the request as a string.</returns>
        [CakeMethodAlias]
        public static string HttpGet(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpGetAsByteArray(context, address, settings));
        }

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS..
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static string HttpGet(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)            
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpGet(context, address, settings);
        }

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS..
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static string HttpGet(this ICakeContext context, string address)
        {
            return HttpGet(context, address, settings => { });
        }

        #endregion

        #region Post Methods

        /// <summary>
        /// POST the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the request as a byte array.</returns>
        [CakeMethodAlias]
        public static byte[] HttpPostAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var client = GetHttpClient(context, settings);
            var response = client.PostAsync(address, new ByteArrayContent(settings.RequestBody)).Result;

            var result = response.Content.ReadAsByteArrayAsync().Result;

            return result;
        }

        /// <summary>
        /// POST the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the request as a string.</returns>
        [CakeMethodAlias]
        public static string HttpPost(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpPostAsByteArray(context, address, settings));
        }

        /// <summary>
        /// POST the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static string HttpPost(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpPost(context, address, settings);
        }

        /// <summary>
        /// POST the specified resource over HTTP.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <returns></returns>
        [CakeMethodAlias]
        public static string HttpPost(this ICakeContext context, string address)
        {
            return HttpPost(context, address, settings => { });
        }

        #endregion

        #region Put Methods

        /// <summary>
        /// PUT the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response as a byte array.</returns>
        [CakeMethodAlias]
        public static byte[] HttpPutAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var client = GetHttpClient(context, settings);
            var response = client.PutAsync(address, new ByteArrayContent(settings.RequestBody)).Result;

            var result = response.Content.ReadAsByteArrayAsync().Result;

            return result;
        }

        /// <summary>
        /// PUT the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static string HttpPut(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpPutAsByteArray(context, address, settings));
        }

        /// <summary>
        /// PUT the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static string HttpPut(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpPut(context, address, settings);
        }

        /// <summary>
        /// PUT the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <returns>Content of the response a string</returns>
        [CakeMethodAlias]
        public static string HttpPut(this ICakeContext context, string address)
        {
            return HttpPut(context, address, settings => { });
        }

        #endregion

        #region Put Methods

        /// <summary>
        /// PATCH the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response as a byte array.</returns>
        [CakeMethodAlias]
        public static byte[] HttpPatchAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), address)
            {
                Content = new ByteArrayContent(settings.RequestBody)                 
            };

            var client = GetHttpClient(context, settings);

            var response = client.SendAsync(request).Result;

            var result = response.Content.ReadAsByteArrayAsync().Result;

            return result;
        }

        /// <summary>
        /// PATCH the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static string HttpPatch(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpPatchAsByteArray(context, address, settings));
        }

        /// <summary>
        /// PATCH the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static string HttpPatch(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpPatch(context, address, settings);
        }

        /// <summary>
        /// PATCH the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <returns>Content of the response a string</returns>
        [CakeMethodAlias]
        public static string HttpPatch(this ICakeContext context, string address)
        {
            return HttpPatch(context, address, settings => { });
        }

        #endregion

        #region Delete Methods

        /// <summary>
        /// DELETE the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static void HttpDelete(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            var client = GetHttpClient(context, settings);
            var response = client.DeleteAsync(address).Result;
        }

        /// <summary>
        /// DELETE the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response as a string.</returns>
        [CakeMethodAlias]
        public static void HttpDelete(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            HttpDelete(context, address, settings);
        }

        /// <summary>
        /// DELETE the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        /// 
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <returns>Content of the response a string</returns>
        [CakeMethodAlias]
        public static void HttpDelete(this ICakeContext context, string address)
        {
            HttpDelete(context, address, settings => { });
        }

        #endregion
        
        /// <summary>
        /// Gets an <see cref="HttpClient"/> pre-populated with the correct default/
        /// The returned client should be disposed of by the caller.
        /// </summary>
        /// <param name="context">The current Cake context.</param>
        /// <param name="settings">HttpSettings to apply to the HttpClient.</param>
        /// <returns>A <see cref="HttpClient"/> instance.</returns>
        private static HttpClient GetHttpClient(ICakeContext context, HttpSettings settings)
        {
            return new HttpClient(new CakeHttpClientHandler(context, settings));
        }
    }
}
