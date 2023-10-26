using Microsoft.AspNetCore.Builder;
using Template.DataAccess.Configurations;
using Template.Application.Configurations;
using Template.Integration.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiConfiguration();

builder.Services.AddCorsPolicyConfiguration();

builder.Services.AddHealthConfiguration();

builder.Services.AddSettingConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddAutoMapperConfiguration();

builder.Services.AddContextConfiguration(builder.Configuration);

builder.Services.AddServiceConfiguration();

builder.Services.AddIntegrationConfiguration();

var app = builder.Build();

app.UseApiConfiguration(builder.Configuration);

app.UseCorsPolicyConfiguration();

app.UseHealthConfiguration();

app.UseSwaggerConfiguration(builder.Configuration, builder.Environment);

app.Run();
