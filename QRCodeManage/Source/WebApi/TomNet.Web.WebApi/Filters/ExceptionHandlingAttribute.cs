using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Filters;

using TomNet.Utility.Extensions;
using TomNet.Utility.Logging;
using TomNet.Web.WebApi.Extensions;

namespace TomNet.Web.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        private static readonly ILogger Logger = LogManager.GetLogger(typeof(ExceptionHandlingAttribute));

        static ExceptionHandlingAttribute()
        {
            Mappings = new Dictionary<Type, HttpStatusCode>
            {
                { typeof(ArgumentNullException), HttpStatusCode.BadRequest },
                { typeof(ArgumentException), HttpStatusCode.BadRequest }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<Type, HttpStatusCode> Mappings { get; private set; }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                return;
            }
            HttpRequestMessage request = actionExecutedContext.Request;
            Exception exception = actionExecutedContext.Exception;
            string ip = actionExecutedContext.ActionContext.Request.GetClientIpAddress();
#if NET45
            string user = actionExecutedContext.ActionContext.RequestContext.Principal.Identity.Name;
#else
            string user = Thread.CurrentPrincipal.Identity.Name;
#endif
            string msg = "User:{0}，IP:{1}，Message:{2}".FormatWith(user, ip, exception.Message);
            Logger.Error(msg, exception);

            /*if (actionExecutedContext.Exception is HttpException)
            {
                HttpException httpException = (HttpException)exception;
                actionExecutedContext.Response =
                    request.CreateResponse((HttpStatusCode)httpException.GetHttpCode(), new Error { Message = exception.Message });
            }
            else*/ if (Mappings.ContainsKey(exception.GetType()))
            {
                HttpStatusCode httpStatusCode = Mappings[exception.GetType()];
                actionExecutedContext.Response =
                    request.CreateResponse(httpStatusCode, new Error { Message = exception.Message });
            }
            else
            {
                actionExecutedContext.Response =
                    actionExecutedContext.Request.CreateResponse(HttpStatusCode.InternalServerError, new Error { Message = exception.Message });
            }
        }
    }


    public class Error
    {
        public string Name { get; set; }

        public string Message { get; set; }
    }
}