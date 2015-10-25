using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProcessClaimService
{
    public class LogRequestAndResponseHandler : DelegatingHandler
    {
        protected static readonly log4net.ILog log = log4net.LogManager.GetLogger("RequestResponse");

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //logging request body
            StringBuilder builder = new StringBuilder();
            string requestBody = await request.Content.ReadAsStringAsync();
            builder.AppendLine("Request Details: ");
            builder.AppendLine(BuildLogEntry(request, requestBody));
            //let other handlers process the request
            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    //once response is ready, log it
                    
                    builder.AppendLine("Response Details: ");
                    builder.AppendLine(BuildLogEntry(task.Result));
                    log.Info(builder.ToString());
                    return task.Result;
                });
        }
        /// <summary>
        /// Builds log data about the request.
        /// </summary>
        /// <param name="actionContext">Data associated with the call</param>
        private  string BuildLogEntry(HttpRequestMessage request,string content)
        {
            string route = request.GetRouteData().Route.RouteTemplate;
            string method = request.Method.Method;
            string url = request.RequestUri.AbsoluteUri;
            string requestBody = content;

            return string.Format("URL: {0} Method: {1}, Route: {2}, RequestContent: {3}",url, method , route,content);
        }
        /// <summary>
        /// Builds log data about the request.
        /// </summary>
        /// <param name="actionContext">Data associated with the call</param>
        private string BuildLogEntry(HttpResponseMessage response)
        {
            string stratusCode =  response.StatusCode.ToString();
            string responseBody = response.Content != null? response.Content.ReadAsStringAsync().Result:"";


            return string.Format("StratusCode: {0} ResponseBody: {1}", stratusCode, responseBody);
        }
        private  string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            //if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            //{
            //    RemoteEndpointMessageProperty prop;
            //    prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
            //    return prop.Address;
            //}

            return "";
        }
    }
}