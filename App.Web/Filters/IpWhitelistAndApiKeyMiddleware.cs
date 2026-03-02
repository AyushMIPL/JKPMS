using DocumentFormat.OpenXml.Office2016.Excel;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

public class IpWhitelistAndApiKeyFilter : ActionFilterAttribute
{
    private readonly IEnumerable<string> _allowedIpAddresses;
    private readonly List<string> _excludedPaths;
    private const string APIKEY_HEADER = "X-API-KEY";
    private readonly string _apiKey;
    private readonly string _isVerifyIP;

    public IpWhitelistAndApiKeyFilter()
    {
        var allowedIpAddresses = ConfigurationManager.AppSettings["Allowd_Ip_Address"];
        _allowedIpAddresses = allowedIpAddresses?.Split(',') == null ? new List<string>() : allowedIpAddresses?.Split(',').ToList();
        _excludedPaths = new List<string> { "/api/BeneficiariesDetails/GetIpAddress" };
        // Api Key
        _apiKey = ConfigurationManager.AppSettings["ApiKey"];
        _isVerifyIP = ConfigurationManager.AppSettings["IsVerifyIP"];
    }

    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        var requestPath = actionContext.Request.RequestUri.AbsolutePath;
        // Skip IP whitelist validation for excluded paths
        if (_excludedPaths.Any(path => requestPath.StartsWith(path, System.StringComparison.OrdinalIgnoreCase)))
        {
            base.OnActionExecuting(actionContext);
            return;
        }

        #region validate Ip Address

        if (!string.IsNullOrEmpty(_isVerifyIP) && _isVerifyIP == "1")
        {

            // Retrieve the X-Forwarded-For header
            IEnumerable<string> forwardedForValues;
            var xForwardedFor = actionContext.Request.Headers.TryGetValues("X-Forwarded-For", out forwardedForValues);
            string remoteIp = forwardedForValues == null ? string.Empty : forwardedForValues.FirstOrDefault();

            if (string.IsNullOrEmpty(remoteIp))
            {
                // Access the HTTP context
                var httpContext = (HttpContextBase)actionContext.Request.Properties["MS_HttpContext"];

                // Retrieve the remote IP address
                remoteIp = httpContext?.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (!_allowedIpAddresses.Contains(remoteIp))
            {
                //actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Forbidden: IP address not allowed.");
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, "Forbidden: You Are Not Authorized To Access Requested API.");
                return;
            }
        }

        #endregion  validate Ip Address

        var extractedApiKeys = actionContext.Request.Headers.TryGetValues(APIKEY_HEADER, out var extractedApiKey);
        string apiKey = (extractedApiKey != null && extractedApiKey.Count() > 0) ? extractedApiKey.ToArray()[0] : string.Empty;
        if (string.IsNullOrEmpty(apiKey))
        {
            actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized: API Key not found.");
            return;
        }

        if (!string.Equals(_apiKey, apiKey, System.StringComparison.Ordinal))
        {
            actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized: Invalid API Key.");
            return;
        }

        base.OnActionExecuting(actionContext);
    }
}
