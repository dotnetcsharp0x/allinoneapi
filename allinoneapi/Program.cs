using allinoneapi.Controllers;
using allinoneapi.Data;
using allinoneapi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json.Serialization;

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
//builder.Services.AddHttpsRedirection();
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
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
