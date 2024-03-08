using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Auth
{
    public class ContextAccessor
    {
        public Guid Id { get; set; }
        public ContextAccessor(IHttpContextAccessor contextAccessor)
        {
            Id = new Guid(contextAccessor.HttpContext.User.FindFirstValue("id"));
        }
    }
}
