var builder = DistributedApplication.CreateBuilder(args);

//Add services to the container.
var api = builder.AddProject<Projects.EduConnect_API>("api");

builder.Build().Run();
