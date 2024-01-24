global using Application.Common.Abstractions.User;
global using WebApi.Services;
global using Ardalis.GuardClauses;
global using Application.Common.Exceptions;

global using Application.Parameters;
global using Application.Parameters.Command.AddParameters;
global using Domain;

global using Application;
global using Application.Common.Models;
global using Infrastructure;
global using Persistence;
global using WebApi;

global using WebApi.Infrastructure;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using Application.Consumers;
global using Core.MessageBrokers;
global using Core.MessageBrokers.Enums;