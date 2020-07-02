using Cake.Core;
using Cake.Core.Annotations;
using System;
using System.Net.Http;
using System.Text;

namespace Cake.Http
{
    /// <summary>
    /// <para>
    /// Contains functionality for working with HTTP operations such as GET, PUT, POST, DELETE, PATCH, etc.
    /// </para>
    /// </summary>
    [CakeAliasCategory("HTTP Operations")]
    [CakeNamespaceImport("Cake.Http")]
    public static class HttpClientAliases
    {
        #region Get Methods

        /// <summary>
        /// GETS the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///      byte[] responseBody = HttpGetAsByteArray("https://www.google.com", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static byte[] HttpGetAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            var result = client.GetByteArrayAsync(address).Result;

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
        ///        string responseBody = HttpGet("https://www.google.com", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Get")]
        [CakeMethodAlias]
        public static string HttpGet(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpGetAsByteArray(context, address, settings));
        }

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///        var responseBody = HttpGet("https://www.google.com", settings =>
        ///        {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetNoCeche()
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
        public static string HttpGet(this ICakeContext context, string address, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpGet(context, address, settings);
        }

        /// <summary>
        /// GETS the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///   string responseBody = HttpGet("https://www.google.com");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to GET.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Get")]
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
        ///     btye[] responseBody = HttpPostAsByteArray("https://www.google.com", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
        [CakeMethodAlias]
        public static byte[] HttpPostAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

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
        public static string HttpPost(this ICakeContext context, string address, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpPostAsByteArray(context, address, settings));
        }

        /// <summary>
        /// POST the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         string responseBody = HttpPost("https://www.google.com", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("appliication/json")
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
        ///     string responseBody = HttpPost("https://www.google.com");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to POST.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Post")]
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
        ///     byte[] responseBody = HttpPutAsByteArray("https://www.google.com/1", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static byte[] HttpPutAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

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
        ///        string responseBody = HttpPut("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Put")]
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
        ///         string responseBody = HttpPut("https://www.google.com/1", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("appliication/json")
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
        ///     string responseBody = HttpPut("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PUT.</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Put")]
        [CakeMethodAlias]
        public static string HttpPut(this ICakeContext context, string address)
        {
            return HttpPut(context, address, settings => { });
        }

        #endregion

        #region Patch Methods

        /// <summary>
        /// PATCH the specified resource over over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///     byte[] responseBody = HttpPatchAsByteArray("https://www.google.com/1", new HttpSettings());
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a byte array.</returns>
        [CakeAliasCategory("Patch")]
        [CakeMethodAlias]
        public static byte[] HttpPatchAsByteArray(this ICakeContext context, string address, HttpSettings settings)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentNullException(nameof(address));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            return HttpSendAsByteArray(context, address, "PATCH", settings);
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
        ///        string responseBody = HttpPatch("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <param name="settings">The settings</param>
        /// <returns>Content of the response body as a string.</returns>
        [CakeAliasCategory("Patch")]
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
        ///         string responseBody = HttpPatch("https://www.google.com/1", settings =>
        ///         {
        ///            settings.UseBearerAuthorization("1af538baa9045a84c0e889f672baf83ff24")
        ///                    .SetContentType("appliication/json")
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
        ///     string responseBody = HttpPatch("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to PATCH.</param>
        /// <returns>Content of the response body a string</returns>
        [CakeAliasCategory("Patch")]
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
        ///        HttpDelete("https://www.google.com/1", settings);
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        /// <param name="settings">The settings</param>
        [CakeAliasCategory("Delete")]
        [CakeMethodAlias]
        public static void HttpDelete(this ICakeContext context, string address, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            var client = GetHttpClient(context, settings);
            var response = client.DeleteAsync(address).Result;
        }

        /// <summary>
        /// DELETE the specified resource over HTTP/HTTPS.
        /// </summary>
        /// <example>
        /// <code>
        ///         HttpDelete("https://www.google.com/1", settings =>
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
        ///     HttpDelete("https://www.google.com/1");
        /// </code>
        /// </example>
        /// <param name="context">The context.</param>
        /// <param name="address">The URL of the resource to DELETE.</param>
        [CakeAliasCategory("Delete")]
        [CakeMethodAlias]
        public static void HttpDelete(this ICakeContext context, string address)
        {
            HttpDelete(context, address, settings => { });
        }

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
        public static byte[] HttpSendAsByteArray(this ICakeContext context, string address, string httpMethod, HttpSettings settings)
        {
            VerifyParameters(context, address, settings);

            if (string.IsNullOrWhiteSpace(httpMethod))
                throw new ArgumentNullException(nameof(httpMethod));

            var client = GetHttpClient(context, settings);

            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(httpMethod),
                RequestUri = new Uri(address),
                Content = (settings.RequestBody != null && settings.RequestBody.Length > 0) ?  new ByteArrayContent(settings.RequestBody) : null
            };

            var response = client.SendAsync(request).Result;

            var result = response.Content.ReadAsByteArrayAsync().Result;

            return result;
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
        public static string HttpSend(this ICakeContext context, string address, string httpMethod, HttpSettings settings)
        {
            return Encoding.UTF8.GetString(HttpSendAsByteArray(context, address, httpMethod, settings));
        }

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
        public static string HttpSend(this ICakeContext context, string address, string httpMethod, Action<HttpSettings> configurator)
        {
            if (configurator == null)
                throw new ArgumentNullException(nameof(configurator));

            var settings = new HttpSettings();
            configurator(settings);

            return HttpSend(context, address, httpMethod, settings);
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
        public static string HttpSend(this ICakeContext context, string address, string httpMethod)
        {
            return HttpSend(context, address, httpMethod, settings => { });
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
            {
                httpClient.Timeout = settings.Timeout.Value;
            }
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
