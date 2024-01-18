using Core.Events.Parameters;
using Core.MessageBrokers;
using Core.MessageBrokers.Enums;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Saga.Application;
using System.Reflection;

namespace Saga.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInPersistence(this IServiceCollection services)
    {
        return services;
    }

}