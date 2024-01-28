using Microsoft.Identity.Web;
using Microsoft.Identity.Web.Resource;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddAuthentication()
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services
    .AddAuthorization();

var app = builder.Build();

var scopes = app.Configuration["AzureAd:Scopes"]!;

const string secret = "/secret";

app.MapGet("/", (HttpContext _) => $"Try {secret}");

app.MapGet(secret, (HttpContext ctx) =>
    {
        ctx.VerifyUserHasAnyAcceptedScope(scopes);
        var name = ctx.User.Claims.First(c => c.Type == "name").Value;
        return $"Your name: {name}";
    })
    .RequireAuthorization();

app.Run();