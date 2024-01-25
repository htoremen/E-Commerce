﻿global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using Identity.Domain;
global using Identity.Domain.Entities;
global using Identity.Domain.Enums;
global using FluentValidation;
global using Identity.Application.Abstractions;
global using Identity.Application.Common.Abstractions;
global using Identity.Application.Common.Abstractions.User;
global using Identity.Application.Common.Exceptions;
global using Identity.Application.Common.Models;
global using Identity.Application.Common.Security;
global using MassTransit;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using System.Reflection;
global using Core.MessageBrokers;
global using Core.Events.Customers;