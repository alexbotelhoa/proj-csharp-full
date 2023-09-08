using System;
using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Template.DataAccess.Commands;
using Template.DataAccess.Repositories;
using Template.DataAccess.CommandHandlers;
using Template.DataAccess.Repositories.Interfaces;

namespace Template.DataAccess.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddTransient<IMediator, Mediator>();

            services.AddScoped<IMapper, Mapper>();

            services.AddScoped<IRequestHandler<GetAllUsersCommand, IActionResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<GetByIdUserCommand, IActionResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, IActionResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, IActionResult>, UserCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, IActionResult>, UserCommandHandler>();

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
