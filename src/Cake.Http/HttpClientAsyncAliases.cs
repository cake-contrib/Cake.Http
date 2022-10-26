using Cake.Core;
using Cake.Core.Annotations;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cake.Http
{
    /// <summary>
    /// <para>
    /// Contains functionality for working with HTTP operations such as GET, PUT, POST, DELETE, PATCH, etc.
    /// </para>
    /// </summary>
    [CakeAliasCategory("HTTP Operations")]
    [CakeNamespaceImport("Cake.Http")]
    public static class HttpClientAsyncAliases
    {
        #region Get Methods

        /// <summary>
        /// GETS the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///      byte[] responseBody = await HttpGetAsByteArrayAsync("https://www.google.com", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static async Task<byte[]> HttpGetAsByteArrayAsync(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            var result = await client.GetByteArrayAsync(address).ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// GETS the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var settings = new HttpSettings
        ///        {
        ///           Headers = new Dictionary&lt;string, string&gt;
        ///            {
        ///              { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
        ///                { "Cache-Control", "no-store" },
        ///                { "Connection", "keep-alive" }
        ///            },
        ///            UseDefaultCredentials = true,
        ///            EnsureSuccessStatusCode = false
        ///        };
        ///
        ///        string responseBody = await HttpGetAsync("https://www.google.com", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static async Task<string> HttpGetAsync(this ICakeContext context, string address, HttpSettings settings)
            => Encoding.UTF8.GetString(await HttpGetAsByteArrayAsync(context, address, settings));

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var responseBody = await HttpGetAsync("https://www.google.com", settings =>
        ///        {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetNoCache()
        ///                    .AppendHeader("Connection", "keep-alive");
        ///        });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the request as a string.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static async Task<string> HttpGetAsync(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return await HttpGetAsync(context, address, settings);
        }

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///   string responseBody = await HttpGetAsync("https://www.google.com");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static async Task<string> HttpGetAsync(this ICakeContext context, string address)
            => await HttpGetAsync(context, address, settings => { });

        #endregion

        #region Post Methods

        /// <summary>
        /// POST the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     btye[] responseBody = await HttpPostAsByteArrayAsync("https://www.google.com", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
        [CakeMethodAlias]
        public static async Task<byte[]> HttpPostAsByteArrayAsync(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            var response = client.PostAsync(address, new ByteArrayContent(settings.RequestBody)).GetAwaiter().GetResult();

            var result = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);

            return result;
        }

        /// <summary>
        /// POST the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var settings = new HttpSettings
        ///        {
        ///           Headers = new Dictionary&lt;string, string&gt;
        ///            {
        ///              { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
        ///                { "Cache-Control", "no-store" },
        ///                { "Connection", "keep-alive" }
        ///            },
        ///            UseDefaultCredentials = true,
        ///            EnsureSuccessStatusCode = false
        ///        };
        ///
        ///        string responseBody = HttpPost("https://www.google.com", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
        [CakeMethodAlias]
        public static async Task<string> HttpPostAsync(this ICakeContext context, string address, HttpSettings settings)
            => Encoding.UTF8.GetString(await HttpPostAsByteArrayAsync(context, address, settings));

        /// <summary>
        /// POST the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         string responseBody = await HttpPostAsync("https://www.google.com", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("application/json")
        ///                    .SetRequestBody("{ \"id\": 123, \"name\": \"Test Test\" }");
        ///         });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
        [CakeMethodAlias]
        public static async Task<string> HttpPostAsync(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return await HttpPostAsync(context, address, settings);
        }

        /// <summary>
        /// POST the specified resource over HTTP.
        /// </summary>
        /// <example>
        /// <code>
        ///     string responseBody = await HttpPostAsync("https://www.google.com");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
        [CakeMethodAlias]
        public static async Task<string> HttpPostAsync(this ICakeContext context, string address)
            => await HttpPostAsync(context, address, settings => { });

        #endregion

        #region Put Methods

        /// <summary>
        /// PUT the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     byte[] responseBody = await HttpPutAsByteArrayAsync("https://www.google.com/1", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static async Task<byte[]> HttpPutAsByteArrayAsync(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            var response = await client.PutAsync(address, new ByteArrayContent(settings.RequestBody)).ConfigureAwait(false);

            var result = await response.Content.ReadAsByteArrayAsync();

            return result;
        }

        /// <summary>
        /// PUT the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var settings = new HttpSettings
        ///        {
        ///           Headers = new Dictionary&lt;string, string&gt;
        ///            {
        ///              { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
        ///                { "Cache-Control", "no-store" },
        ///                { "Connection", "keep-alive" }
        ///            },
        ///            UseDefaultCredentials = true,
        ///            EnsureSuccessStatusCode = false
        ///        };
        ///
        ///        string responseBody = await HttpPutAsync("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static async Task<string> HttpPutAsync(this ICakeContext context, string address, HttpSettings settings)
            => Encoding.UTF8.GetString(await HttpPutAsByteArrayAsync(context, address, settings));

        /// <summary>
        /// PUT the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         string responseBody = await HttpPutAsync("https://www.google.com/1", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("application/json")
        ///                    .SetRequestBody("{ \"id\": 123, \"name\": \"Test Test\" }");
        ///         });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static async Task<string> HttpPutAsync(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return await HttpPutAsync(context, address, settings);
        }

        /// <summary>
        /// PUT the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     string responseBody = HttpPut("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static async Task<string> HttpPutAsync(this ICakeContext context, string address)
            => await HttpPutAsync(context, address, settings => { });

        #endregion

        #region Patch Methods

        /// <summary>
        /// PATCH the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     byte[] responseBody = await HttpPatchAsByteArrayAsync("https://www.google.com/1", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Patch")]
        [CakeMethodAlias]
        public static async Task<byte[]> HttpPatchAsByteArrayAsync(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            return await HttpSendAsByteArrayAsync(context, address, "PATCH", settings);
        }

        /// <summary>
        /// PATCH the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var settings = new HttpSettings
        ///        {
        ///           Headers = new Dictionary&lt;string, string&gt;
        ///            {
        ///              { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
        ///                { "Cache-Control", "no-store" },
        ///                { "Connection", "keep-alive" }
        ///            },
        ///            UseDefaultCredentials = true,
        ///            EnsureSuccessStatusCode = false
        ///        };
        ///
        ///        string responseBody = await HttpPatchAsync("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Patch")]
        [CakeMethodAlias]
        public static async Task<string> HttpPatchAsync(this ICakeContext context, string address, HttpSettings settings)
            => Encoding.UTF8.GetString(await HttpPatchAsByteArrayAsync(context, address, settings));

        /// <summary>
        /// PATCH the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         string responseBody = await HttpPatchAsync("https://www.google.com/1", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("application/json")
        ///                    .SetRequestBody("{ \"id\": 123, \"name\": \"Test Test\" }");
        ///         });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Patch")]
        [CakeMethodAlias]
        public static async Task<string> HttpPatchAsync(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return await HttpPatchAsync(context, address, settings);
        }

        /// <summary>
        /// PATCH the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     string responseBody = await HttpPatchAsync("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <returns>Content of the response body a string</returns>
        [CakeAliasCategory("Patch")]
        [CakeMethodAlias]
        public static async Task<string> HttpPatchAsync(this ICakeContext context, string address)
            => await HttpPatchAsync(context, address, settings => { });

        #endregion

        #region Delete Methods

        /// <summary>
        /// DELETE the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var settings = new HttpSettings
        ///        {
        ///           Headers = new Dictionary&lt;string, string&gt;
        ///            {
        ///              { "Authorization", "Bearer 1af538baa9045a84c0e889f672baf83ff24" },
        ///                { "Cache-Control", "no-store" },
        ///                { "Connection", "keep-alive" }
        ///            },
        ///            UseDefaultCredentials = true,
        ///            EnsureSuccessStatusCode = true
        ///        };
        ///
        ///        await HttpDeleteAsync("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <param name="settings">The settings</param>
        [CakeAliasCategory("Delete")]
        [CakeMethodAlias]
        public static async Task HttpDeleteAsync(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            await client.DeleteAsync(address).ConfigureAwait(false);
        }

        /// <summary>
        /// DELETE the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         await HttpDeleteAsync("https://www.google.com/1", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24");
        ///         });
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <param name="configurator">The settings configurator.</param>
        [CakeAliasCategory("Delete")]
        [CakeMethodAlias]
        public static async Task HttpDeleteAsync(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            await HttpDeleteAsync(context, address, settings);
        }

        /// <summary>
        /// DELETE the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     HttpDelete("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        [CakeAliasCategory("Delete")]
        [CakeMethodAlias]
        public static async Task HttpDeleteAsync(this ICakeContext context, string address)
            => await HttpDeleteAsync(context, address, settings => { });

        #endregion

        #region Send Method

        /// <summary>
        /// Sends the HTTP Request using the generic HttpClient Send Method.
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource.</param>
        /// <param name="httpMethod">Http Method used to: POST, PUT, GET, DELETE, PATCH, etc.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Send")]
        [CakeMethodAlias]
        public static async Task<byte[]> HttpSendAsByteArrayAsync(this ICakeContext context, string address, string httpMethod, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            if (string.IsNullOrWhiteSpace(httpMethod))
                throw new ArgumentNullException(nameof(httpMethod));

            var client = GetHttpClient(context, settings);

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                RequestUri = new Uri(address),
                Content = (settings.RequestBody != null && settings.RequestBody.Length > 0) ? new ByteArrayContent(settings.RequestBody) : null
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);

            return await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the HTTP Request using the generic HttpClient Send Method.
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource.</param>
        /// <param name="httpMethod">Http Method used to: POST, PUT, GET, DELETE, PATCH, etc.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Send")]
        [CakeMethodAlias]
        public static async Task<string> HttpSendAsync(this ICakeContext context, string address, string httpMethod, HttpSettings settings)
            => Encoding.UTF8.GetString(await HttpSendAsByteArrayAsync(context, address, httpMethod, settings));

        /// <summary>
        /// Sends the HTTP Request using the generic HttpClient Send Method.
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource.</param>
        /// <param name="httpMethod">Http Method used to: POST, PUT, GET, DELETE, PATCH, etc.</param>
        /// <param name="configurator">The settings configurator.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Send")]
        [CakeMethodAlias]
        public static async Task<string> HttpSendAsync(this ICakeContext context, string address, string httpMethod, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return await HttpSendAsync(context, address, httpMethod, settings);
        }

        /// <summary>
        /// Sends the HTTP Request using the generic HttpClient Send Method.
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource.</param>
        /// <param name="httpMethod">Http Method used to: POST, PUT, GET, DELETE, PATCH, etc.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Send")]
        [CakeMethodAlias]
        public static async Task<string> HttpSendAsync(this ICakeContext context, string address, string httpMethod)
            => await HttpSendAsync(context, address, httpMethod, settings => { });

        /// <summary>
        /// Sends the HTTP Request using the generic HttpClient Send Method using the HttpRequestMessage Object
        /// </summary>
        /// <example>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="request">Raw HttpRequest Message with full access to underlying request object.</param>
        /// <returns>HttpResponseMessage</returns>
        [CakeAliasCategory("Send")]
        [CakeMethodAlias]
        public static async Task<HttpResponseMessage> HttpSendAsync(this ICakeContext context, HttpRequestMessage request)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (request == null) throw new ArgumentNullException(nameof(request));

            var client = GetHttpClient(context, new HttpSettings { });

            return await client.SendAsync(request).ConfigureAwait(false);
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
            var httpClient = new HttpClient(new CakeHttpClientHandler(context, settings));
            SetHttpClientBasedSettings(settings, httpClient);
            return httpClient;
        }

        private static void SetHttpClientBasedSettings(HttpSettings settings, HttpClient httpClient)
        {
            if (settings.Timeout.HasValue)
                httpClient.Timeout = settings.Timeout.Value;
        }

        private static void VerifyParameters(ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));
        }
    }
}