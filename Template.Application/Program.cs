using Microsoft.AspNetCore.Builder;
using Template.Application.Configurations;
using Template.DataAccess.Configurations;
using Template.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

builder.Services.AddSettingConfiguration(builder.Configuration);

builder.Services.AddContextConfiguration(builder.Configuration);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddServiceConfiguration();

builder.Services.AddIntegrationConfiguration();

builder.Services.AddHealthConfiguration();

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddCorsPolicyConfiguration();

var app = builder.Build();

app.UseApiConfiguration(builder.Configuration);

app.UseHealthConfiguration();

app.UseSwaggerConfiguration(builder.Configuration, builder.Environment);

app.UseCorsPolicyConfiguration();

app.Run();
