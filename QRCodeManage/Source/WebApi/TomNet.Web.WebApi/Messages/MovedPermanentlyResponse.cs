﻿using System;
using System.Net;


namespace TomNet.Web.WebApi.Messages
{
    public class MovedPermanentlyResponse : ResourceIdentifierBase
    {
        public MovedPermanentlyResponse()
            : base(HttpStatusCode.MovedPermanently)
        {
        }

        public MovedPermanentlyResponse(Uri resource)
            : base(HttpStatusCode.MovedPermanently, resource)
        {
        }
    }
}
