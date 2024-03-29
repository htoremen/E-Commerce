﻿global using Ardalis.GuardClauses;
global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using Core.Events.TodoLists;
global using Core.MessageBrokers.RabbitMQ;
global using Todo.Domain;
global using Todo.Domain.Entities;
global using Todo.Domain.Enums;
global using Todo.Domain.Events;
global using FluentValidation;
global using MassTransit;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;
global using Todo.Application.Abstractions;
global using Todo.Application.Common.Abstractions;
global using Todo.Application.Common.Abstractions.User;
global using Todo.Application.Common.Exceptions;
global using Todo.Application.Common.Models;
global using Todo.Application.Common.Security;
global using Todo.Application.Telemetry;
global using Todo.Application.TodoLists.Commands.CreateTodoList;
global using Core.MessageBrokers;