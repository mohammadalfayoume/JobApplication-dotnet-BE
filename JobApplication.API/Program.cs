using JobApplication.API.Extensions;
using JobApplication.API.Middlewares;
using JobApplication.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200")  // Add your Angular app URL
            .AllowAnyHeader()
            .AllowAnyMethod());
});
// Register Services
builder.Services.AddDbContextService(builder.Configuration);
builder.Services.AddJobApplicationServices();
builder.Services.AddAuthenticationService(builder.Configuration);

var app = builder.Build();
MappingConfiguration.ConfigureMapster();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();
app.MapControllers();

app.Run();
