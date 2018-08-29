using System.Linq;
using System.Security.Claims;


namespace TomNet.Core.Extensions
{
    /// <summary>
    /// <see cref="ClaimsIdentity"/>扩展操作类
    /// </summary>
    public static class ClaimsIdentityExtensions
    {
        /// <summary>
        /// 获取指定类型的Claim值
        /// </summary>
        public static string GetClaimValueFirstOrDefault(this ClaimsIdentity identity, string type)
        {
            Claim claim = identity.Claims.FirstOrDefault(m => m.Type == type);
            return claim == null ? null : claim.Value;
        }

        /// <summary>
        /// 获取指定类型的所有Claim值
        /// </summary>
        public static string[] GetClaimValues(this ClaimsIdentity identity, string type)
        {
            return identity.Claims.Where(m => m.Type == type).Select(m => m.Value).ToArray();
        }
    }
}