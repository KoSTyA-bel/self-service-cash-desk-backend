﻿using Fedorakin.CashDesk.Web.Interfaces.Utils;
using Fedorakin.CashDesk.Web.Utils;

namespace Fedorakin.CashDesk.Web.Middlewares;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJWTUtils jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        var admin = jwtUtils.ValidateToken(token);

        if (admin is not null)
        {
            context.Items["AdminName"] = admin.Name;
        }

        await _next(context);
    }
}