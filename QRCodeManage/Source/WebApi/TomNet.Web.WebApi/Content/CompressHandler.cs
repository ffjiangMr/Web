﻿using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace TomNet.Web.WebApi.Content
{
    public class CompressHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                HttpResponseMessage response = task.Result;

                if (response.RequestMessage.Headers.AcceptEncoding != null)
                {
                    string encodingType = response.RequestMessage.Headers.AcceptEncoding.First().Value;

                    response.Content = new CompressedContent(response.Content, encodingType);
                }

                return response;
            },
                TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}