using System;


namespace TomNet.Web.WebApi.Messages
{
    public class ResourceLocation
    {
        public Uri Location { get; private set; }

        public void Set(Uri location)
        {
            Location = location;
        }

    }
}
