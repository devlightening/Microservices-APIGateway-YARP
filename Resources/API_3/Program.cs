var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("/api3", () => "API 3");

//For ÝnMemory
app.MapGet("/api3", () => "API 3 (Configured via InMemory YARP)");

app.Run();