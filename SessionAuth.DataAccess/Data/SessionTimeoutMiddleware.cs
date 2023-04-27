using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SessionAuth.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SessionAuth.DataAccess.Data
{
    //THIS MIDDLEWARE IS NOT NEEDED ANYMORE BECAUSE I HANDELED IT IN PROGRAM.CS FILE


    public class SessionTimeoutMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SessionOptions _sessionOptions;

        public SessionTimeoutMiddleware(RequestDelegate next, IOptions<SessionOptions> sessionOptions)
        {
            _next = next;
            _sessionOptions = sessionOptions.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            var session = context.Session;
            if (session.TryGetValue("User", out byte[] value))
            {
                var creationTime = DateTime.FromBinary(BitConverter.ToInt64(value, 0));
                var idleTimeout = _sessionOptions.IdleTimeout;

                if (creationTime + idleTimeout < DateTime.UtcNow)
                {
                    // session has expired, log out the user
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    context.Response.Redirect("/Account/Login");
                    return;
                }
            }
            else
            {
                var user = session.GetObject<UserInfo>("User");
                if (user == null)
                {
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    //context.Response.Redirect("/Account/Login");
                    //return;
                }
            }

            await _next.Invoke(context);
        }
    }

}
