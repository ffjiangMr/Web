using System.Net.Http.Headers;


namespace TomNet.Web.WebApi.Internal
{
    public static class MediaTypeConstants
    {
        /// <summary>
        /// 类型为 application/json 的MediaType
        /// </summary>
        public static MediaTypeHeaderValue ApplicationJson
        {
            get
            {
                return new MediaTypeHeaderValue("application/json") { CharSet = "UTF-8" };
            }
        }
    }
}
