using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Auth
{
    public class ReadableBodyStreamAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            Guid id = new Guid(context.HttpContext.User.Claims.First(claim=>claim.Type == "Id").Value);
            var userName = context.HttpContext.User.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
            new ClaimContext(id, userName);
        }

        public class ClaimContext
        {
            private static Guid _id;
            private static string? _userName;

            public ClaimContext(Guid id, string userName)
            {
                _id = id;
                _userName = userName;
            }
            public static Guid Id() 
            {
                return _id;
            }
            public static string UserName()
            {
                return _userName;
            }
        }
    }
}
