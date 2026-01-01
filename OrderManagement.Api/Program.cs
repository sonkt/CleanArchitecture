using GbLib.BuildingBlock.Domain.Interfaces;
using GbLib.BuildingBlock.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Application;
using OrderManagement.Domain.Interfaces;
using OrderManagement.Infrastructure.Persistence;
using System.Text.Json.Serialization;
using GbLib.BuildingBlock.Infrastructure.Interceptors;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .AddInterceptors(
                sp.GetRequiredService<AuditInterceptor>(),
                sp.GetRequiredService<TenantInterceptor>()
        )
);

builder.Services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
builder.Services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
builder.Services.AddApplicationServices();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Order Management",
        Version = "v1",
        Description = "API cho hệ thống Emo Clean Architecture",
    });

    // Nếu có xác thực JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập token theo định dạng: Bearer {your JWT token}"
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });


var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.UseHttpsRedirection();

app.Run();