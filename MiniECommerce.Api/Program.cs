using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniECommerce.Core.Entities;
using MiniECommerce.DAL.Abstracts;
using MiniECommerce.DAL.Concretes;
using MiniECommerce.DAL.Contexts;
using MiniECommerce.Services;
using MiniECommerce.Services.Abstracts;
using MiniECommerce.Services.Concrates;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
