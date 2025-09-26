var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

//app.MapGet("/api2", () => "API 2");

//For ÝnMemory
app.MapGet("/api2", () => "API 2 (Configured via InMemory YARP)");

app.Run();