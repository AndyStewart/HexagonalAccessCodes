var builder = DistributedApplication.CreateBuilder(args);

var sql = builder.AddSqlServer("sql").AddDatabase("AccessCodes");
var api = builder.AddProject<Projects.AccessCodes_Api>("Api").WithReference(sql);

builder.Build().Run();