using allinoneapi.Data;
using AspNetCoreRateLimit;
using DotNet.RateLimiter.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;



var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
//builder.Services.AddInvestApiClient((_, settings) => settings.AccessToken = "t.q7kZhnxMRXewStSO2COiNqnpkVfSCPlELsrOb7uTKGxzIlrp1xQOA9sUkGwhQKpy6MxN2CiexsaePwlFNSvqsA");

//builder.Services.AddDbContext<allinoneapiContext>(options =>

//options.UseSqlServer(builder.Configuration.GetConnectionString("allinoneapiContext"), op =>
//    op.CommandTimeout(60)
//));
builder.Services.AddDbContext<allinoneapiContext>();
//builder.Services.AddInvestApiClient((_, settings) => context.Configuration.Bind(settings));
//builder.Services.AddHttpsRedirection();
builder.Services.AddInvestApiClient((_, settings) => settings.AccessToken = "t.GtzjTRI4-Ud1DYWg7axnea-AfV3PShPoeTReMctxW7MavZRaxR1FH2sUmAxaLdaNUZ8vBKhqlEDsMnnMCdwtzg");
builder.Services.AddMemoryCache();
builder.Services.AddGraphQLServer().AddQueryType<Query>();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.EnableEndpointRateLimiting = true;
    options.StackBlockedRequests = false;
    options.HttpStatusCode = 429;
    options.RealIpHeader = "X-Real-IP";
    options.ClientIdHeader = "X-ClientId";
    options.GeneralRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Period = "1s",
                Limit = 10,
            }
        };
});
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.AddInMemoryRateLimiting();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    
}
app.UseIpRateLimiting();

app.UseHsts();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

builder.WebHost.UseUrls("http://localhost:5210", "https://localhost:443");
//builder.WebHost.UseUrls("https://localhost:443");
app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();
PathString path;
path = "/graphql";
app.MapGraphQL(path);
app.UseRouting();
app.UseCors(builder => builder
     .AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
     );
var fixedPolicy = "fixed";
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");




await app.RunAsync();
//BenchmarkRunner.Run<BenchmarkAPI>();
