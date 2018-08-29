using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TomNet.Web.Mvc.UI;

namespace TomNet.Web.WebApi.Messages
{
    public class TokenResult<T> : IHttpActionResult where T : class
    {
        private T result;
        private string token;
        private HttpRequestMessage request;

        public TokenResult(T result, string token, HttpRequestMessage request)
        {
            this.result = result;
            this.token = token;
            this.request = request;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new ObjectContent<T>(result, new JsonMediaTypeFormatter());
            response.RequestMessage = request;
            response.Headers.Add("Token", token);
            return Task.FromResult(response);
        }
    }
    public class TokenResult : TokenResult<AjaxResult>
    {
        public TokenResult(AjaxResult result, string token, HttpRequestMessage request)
            : base(result, token, request)
        {
        }
    }

}
