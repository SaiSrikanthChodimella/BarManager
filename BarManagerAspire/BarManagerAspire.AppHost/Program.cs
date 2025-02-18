var builder = DistributedApplication.CreateBuilder(args);

var barManagerAPI = builder.AddProject<Projects.BarManagerAPI>("BarManagerAPI");

builder.AddProject<Projects.BarManager>("barManagerUI")
    .WithReference(barManagerAPI)
    .WithExternalHttpEndpoints()
    .WaitFor(barManagerAPI);

builder.Build().Run();
