using System;
using System.Collections.Generic;


namespace TomNet.Web.WebApi.Extensions
{
    public static class ActionExtensions
    {
        public static Action Chain(this IEnumerable<Action> actions)
        {
            return () =>
            {
                foreach (var action in actions)
                    action();
            };
        }

    }
}
