var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("/api3", () => "API 3");

//For �nMemory
app.MapGet("/api3", () => "API 3 (Configured via InMemory YARP)");

app.Run();