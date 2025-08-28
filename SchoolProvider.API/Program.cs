using Microsoft.OpenApi.Models;
using SchoolProvider.Business.Mapping;
using SchoolProvider.Business.Student.Handlers;
using SchoolProvider.Database.Context;
using SchoolProvider.Database.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SupportNonNullableReferenceTypes();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SchoolProvider API",
        Version = "v1",
        Description = "API for managing students, classrooms, and school data",
        Contact = new OpenApiContact
        {
            Name = "Team Name",
            Email = "a@b.com"
        }
    });
});
// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// DI setup
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMongoDbContext, MongoDbContext>();

// MediatR
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(CreateStudentCommandHandler).Assembly));

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SchoolProvider API V1");
    options.RoutePrefix = "swagger"; // swagger UI at /swagger
});

app.MapControllers();

app.MapGet("/",
        context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        }).AllowAnonymous();

app.Run();
