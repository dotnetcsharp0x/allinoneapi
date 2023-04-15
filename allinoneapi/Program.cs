using allinoneapi;
using allinoneapi.Controllers;
using allinoneapi.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json.Serialization;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Hosting;
using api.allinoneapi.InvestApi.Sample;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<allinoneapiContext>(options =>

//options.UseSqlServer(builder.Configuration.GetConnectionString("allinoneapiContext"), op =>
//    op.CommandTimeout(60)
//));
builder.Services.AddDbContext<allinoneapiContext>();
//builder.Services.AddInvestApiClient((_, settings) => context.Configuration.Bind(settings));
//builder.Services.AddHttpsRedirection();
builder.Services.AddInvestApiClient((_, settings) => settings.AccessToken = "t.5vC9A1M_UoeJ4yr_7eczcs9gI-X7YJJtdqsWnyMXcrky_LqzueblUJhVYzmcMOPmz7ZqbANp8_9r4qma5D9UBA");
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    
}
app.UseHsts();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

//builder.WebHost.UseUrls("http://localhost:5210", "https://localhost:443");
//builder.WebHost.UseUrls("https://localhost:443");
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
app.UseRouting();
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");



await app.RunAsync();
//BenchmarkRunner.Run<BenchmarkAPI>();
