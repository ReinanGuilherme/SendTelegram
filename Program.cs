using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using SendTelegram.EndPoints.SendMessageDocument;
using SendTelegram.EndPoints.SendMessageImg;
using SendTelegram.EndPoints.SendMessageText;
using SendTelegram.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// add service TelegramBot
builder.Services.AddScoped<TelegramBot>();

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapMethods(SendMessageTextPost.Template, SendMessageTextPost.Methods, SendMessageTextPost.Handle);
app.MapMethods(SendMessageImgPost.Template, SendMessageImgPost.Methods, SendMessageImgPost.Handle);
app.MapMethods(SendMessageDocumentPost.Template, SendMessageDocumentPost.Methods, SendMessageDocumentPost.Handle);

app.Run();