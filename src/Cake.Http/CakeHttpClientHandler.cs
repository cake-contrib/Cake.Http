using Cake.Core;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cake.Http
{
    /// <summary>
    /// Custom HTTP Handler to delegate the processing of HTTP requests and extending it.
    /// </summary>

#if net46
    public class CakeHttpClientHandler : WebRequestHandler
#endif
#if NETSTANDARD1_6
    public class CakeHttpClientHandler : HttpClientHandler
#endif
  {
    private readonly HttpSettings _Settings;
    private readonly ICakeContext _Context;

    /// <summary>
    /// Custom HTTP Handler to delegate the processing of HTTP requests and extending it.
    /// </summary>
    /// <param name="context">Cake Context the request is geting </param>
    /// <param name="settings">HttpSettings to apply to the inner handler</param>
    public CakeHttpClientHandler(ICakeContext context, HttpSettings settings)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
            _Settings = settings ?? throw new ArgumentException(nameof(settings));

            UseDefaultCredentials = settings.UseDefaultCredentials;
            UseCookies = false;

        }

        /// <summary>
        ///  Sends an HTTP request to the inner handler to send to the server as an asynchronous  operation.
        /// </summary>
        /// <param name="request"> The HTTP request message to send to the server.</param>
        /// <param name="cancellationToken">A cancellation token to cancel operation.</param>
        /// <returns>Returns System.Threading.Tasks.Task`1. The task object representing the asynchronous</returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var id = $"{ DateTime.UtcNow.Ticks }{ Guid.NewGuid().ToString() }";
            var requestInfo = $"{request.Method} {request.RequestUri}";

            AppendHeaders(request); //<==== Appends Custom Headers to the request.

            //Logs the Request to the Cake Context Logger
            byte[] requestMessage = null;
            if (request?.Content != null)
                requestMessage = await request.Content.ReadAsByteArrayAsync();
            else
                requestMessage = new byte[] { };

            await LogHttpEvent(id, HttpEventType.Request, requestInfo, requestMessage);

            //Gets the actual response
            var response = await base.SendAsync(request, cancellationToken);

            //Logs the Response to the Cake Context Logger
            byte[] responseMessage = null;
            if (response.IsSuccessStatusCode && response.Content != null)
                responseMessage = await response.Content.ReadAsByteArrayAsync();

            else if (response.Content != null)
            {
                var tempMessage = await response.Content.ReadAsStringAsync();
                responseMessage = Encoding.UTF8.GetBytes($"{response.ReasonPhrase} ({ (int)response.StatusCode })\r\n{ string.Concat(Enumerable.Repeat("=", 70))}\r\n{ tempMessage ?? string.Empty }");
            }
            else
                responseMessage = Encoding.UTF8.GetBytes($"{response.ReasonPhrase} ({ (int)response.StatusCode})");

            await LogHttpEvent(id, HttpEventType.Response, requestInfo, responseMessage);

            // Determines whether to ensure Status code
            if (_Settings.EnsureSuccessStatusCode)
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    // Only throw exception on when Non-Success Status Code
                    if (_Settings.ThrowExceptionOnNonSuccessStatusCode)
                        throw ex;
                }
            }

            return response;
        }

        /// <summary>
        /// Appends headers to HttpRequestMessage before sending the 
        /// </summary>
        /// <param name="request"></param>
        private void AppendHeaders(HttpRequestMessage request)
        {
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Cake", _Context.Environment.Runtime.CakeVersion.ToString()));

            if (_Settings.Headers?.Count > 0)
            {
                //Append Non-Content Header Request
                foreach (var header in _Settings.Headers.Where(kvp => !(kvp.Key.IndexOf("content", StringComparison.OrdinalIgnoreCase) >= 0)))
                    request.Headers.Add(header.Key, header.Value);         
                
                //Append Content Headers to Request Body                
                if(request.Content != null)
                {
                    foreach (var header in _Settings.Headers.Where(kvp => kvp.Key.IndexOf("content", StringComparison.OrdinalIgnoreCase) >= 0))
                        request.Content.Headers.Add(header.Key, header.Value);
                }
            }

            if(_Settings.Cookies?.Count > 0)
                request.Headers.Add("Cookie", string.Join<string>(";", _Settings.Cookies.Select(kvp => $"{kvp.Key}={kvp.Value}")));
        }

        /// <summary>
        /// Logs a request/response to the current cake context
        /// </summary>
        /// <param name="id">Unique identifer that can link the request with response</param>
        /// <param name="eventType">Request or Response</param>
        /// <param name="requestInfo">Request info</param>
        /// <param name="message">message to log</param>
        /// <returns>Task</returns>
        private async Task LogHttpEvent(string id, HttpEventType eventType, string requestInfo, byte[] message)
        {
            await Task.Run(() =>
            {
                _Context.Log.Write(Core.Diagnostics.Verbosity.Diagnostic, Core.Diagnostics.LogLevel.Verbose, "{0} - {1}: {2}\r\n{3}", id, eventType, requestInfo, Encoding.UTF8.GetString(message));
            });
        }

        private enum HttpEventType
        {
            Request,
            Response
        }
    }
}
