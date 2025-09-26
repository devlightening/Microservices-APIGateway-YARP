using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// Authorization Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Authenticated", policy => policy.RequireAuthenticatedUser());
});

// Token Service’i DI container’a ekliyoruz
builder.Services.AddSingleton<TokennService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

// JWT korumalı endpoint
app.MapGet("/api1", () => " API 1 accessed successfully with a valid JWT.")
   .RequireAuthorization("Authenticated");

app.MapGet("/generate-token", (TokennService tokenService) =>
{
    var token = tokenService.GenerateToken("testuser");

    var html = $@"
    <html>
    <head>
        <style>
            body {{
                font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
                background-color: #f4f4f9;
                display: flex;
                justify-content: center;
                align-items: center;
                height: 100vh;
            }}
            .container {{
                background: white;
                padding: 20px;
                border-radius: 10px;
                box-shadow: 0px 4px 10px rgba(0,0,0,0.15);
                width: 80%;
                max-width: 800px;
            }}
            h2 {{
                color: #333;
                text-align: center;
            }}
            pre {{
                background: #1e1e1e;
                color: #d4d4d4;
                padding: 15px;
                border-radius: 8px;
                overflow-x: auto;
                white-space: pre-wrap;
                word-wrap: break-word;
                font-size: 14px;
            }}
            .btn {{
                display: block;
                margin: 15px auto;
                padding: 10px 20px;
                background: #0078d7;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                font-size: 14px;
            }}
            .btn:hover {{
                background: #005a9e;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <h2> Generated JWT Token</h2>
            <pre>{token}</pre>
            <a href='#' class='btn' onclick='copyToken()'> Copy Token</a>
        </div>
        <script>
            function copyToken() {{
                navigator.clipboard.writeText(`{token}`);
                alert('Token copied to clipboard!');
            }}
        </script>
    </body>
    </html>";

    return Results.Content(html, "text/html");
});

app.Run();
