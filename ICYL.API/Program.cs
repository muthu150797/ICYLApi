using Stripe;

var builder = WebApplication.CreateBuilder(args);
var config = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false).Build();
var stripeSecretKey = config.GetSection("StripeSecretKey").Value;
// Add services to the container.
//StripeConfiguration.ApiKey = stripeSecretKey;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Enable cors policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
     {
         options.AddPolicy(name: MyAllowSpecificOrigins,
                           builder =>
                           {
                               builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                           });
     }); var app = builder.Build();
//cons.Routes.MapHttpRoute(
//               name: "DefaultApi",
//               routeTemplate: "api/{controller}/{action}/{id}",
//               defaults: new { id = RouteParameter.Optional }
//           );
 app.UseCors(MyAllowSpecificOrigins);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "ICYL.Api");
       // c.RoutePrefix = String.Empty;
    });
    app.UseDeveloperExceptionPage();
}
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new
     List<string> { "swagger/index.html" }
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
