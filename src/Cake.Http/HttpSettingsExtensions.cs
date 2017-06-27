using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Script.Serialization;

namespace Cake.Http
{
    /// <summary>
    /// Contains functionality related to HTTP settings.
    /// </summary>
    public static class HttpSettingsExtensions
    {
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
        public static HttpSettings UseBasicAuthorization(this HttpSettings settings, string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException(nameof(userName));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password));

            settings.SetAuthorization("Basic", credentials);
            return settings;
        }

        /// <summary>
        /// Adds a "Bearer" Token Authorization Header to the request.
        /// Used when authorization against a resource that uses OAUTH2
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="token">Token to apply to the header</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings UseBearerAuthorization(this HttpSettings settings, string token)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentNullException(nameof(token));

            return settings.SetAuthorization("Bearer", token);
        }

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
        public static HttpSettings SetNoCache(this HttpSettings settings)
        {
            return settings.AppendHeader("Cache-Control", "no-store");
        }

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

            var requestBody = new JavaScriptSerializer().Serialize(data);

            if (indentOutput)
                requestBody = FormatJsonOutput(requestBody);

            settings.RequestBody = Encoding.UTF8.GetBytes(requestBody);
            settings.SetContentType("application/json");
            return settings;
        }

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overriden.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Dictionary of data to url encode and set to the body.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetFormUrlEncodedRequestBody(this HttpSettings settings,
            IDictionary<string, string> data)
            => SetFormUrlEncodedRequestBody(settings, data?.AsEnumerable());

        /// <summary>
        /// Sets the request body as form url encoded.
        /// Only valid for Http Methods that allow a request body.
        /// Any existing content in the RequestBody is overriden.
        /// Accepts multiple parameters with the same key.
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <param name="data">Enumerable of <see cref="KeyValuePair{TKey,TValue}"/> of data to url encode and set to the body.</param>
        /// <returns>The same <see cref="HttpSettings"/> instance so that multiple calls can be chained.</returns>
        public static HttpSettings SetFormUrlEncodedRequestBody(this HttpSettings settings,
            IEnumerable<KeyValuePair<string, string>> data)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (data == null)
                throw new ArgumentNullException(nameof(data));

            settings.RequestBody = new FormUrlEncodedContent(data).ReadAsByteArrayAsync().Result;
            settings.SetContentType("application/x-www-form-urlencoded");

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

        private static void VerifyParameters(HttpSettings settings, string name, string value)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Adds indentation and line breaks to output of JavaScriptSerializer
        /// </summary>
        private static string FormatJsonOutput(string data)
        {
            if (string.IsNullOrWhiteSpace(data))
                return data;

            var stringBuilder = new StringBuilder();

            bool escaping = false;
            bool inQuotes = false;
            int indentation = 0;

            foreach (char character in data)
            {
                if (escaping)
                {
                    escaping = false;
                    stringBuilder.Append(character);
                }
                else
                {
                    if (character == '\\')
                    {
                        escaping = true;
                        stringBuilder.Append(character);
                    }
                    else if (character == '\"')
                    {
                        inQuotes = !inQuotes;
                        stringBuilder.Append(character);
                    }
                    else if (!inQuotes)
                    {
                        if (character == ',')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', indentation);
                        }
                        else if (character == '[' || character == '{')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', ++indentation);
                        }
                        else if (character == ']' || character == '}')
                        {
                            stringBuilder.Append("\r\n");
                            stringBuilder.Append('\t', --indentation);
                            stringBuilder.Append(character);
                        }
                        else if (character == ':')
                        {
                            stringBuilder.Append(character);
                            stringBuilder.Append('\t');
                        }
                        else
                            stringBuilder.Append(character);

                    }
                    else
                        stringBuilder.Append(character);

                }
            }
            return stringBuilder.ToString();
        }
    }
}