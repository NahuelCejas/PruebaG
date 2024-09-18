using Application.Interfaces.Command;
using Application.Interfaces.Query;
using Application.Interfaces.Service;
using Application.UseCases;
using Infrastructure.Command;
using Infrastructure.Persistence;
using Infrastructure.Querys;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo 
        { 
            Title = "Marketing CRM", 
            Version = "1.0" 
        });
        options.EnableAnnotations();
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    });

    var connectionString = builder.Configuration["ConnectionString"];
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

        //Custom de patron de Dependency Injection
        builder.Services.AddScoped<IClientCommand, ClientCommand>();
        builder.Services.AddScoped<IProjectCommand, ProjectCommand>();

        builder.Services.AddScoped<ICampaignTypeQuery, CampaignTypeQuery>();
        builder.Services.AddScoped<IClientQuery, ClientQuery>();
        builder.Services.AddScoped<IInteractionTypeQuery, InteractionTypeQuery>();
        builder.Services.AddScoped<IProjectQuery, ProjectQuery>();
        builder.Services.AddScoped<ITaskQuery, TaskQuery>();
        builder.Services.AddScoped<ITaskStatusQuery, TaskStatusQuery>();
        builder.Services.AddScoped<IUserQuery, UserQuery>();

        builder.Services.AddScoped<ICampaignTypeServices, CampaignTypeServices>();
        builder.Services.AddScoped<IClientServices, ClientServices>();
        builder.Services.AddScoped<IInteractionTypeServices, InteractionTypeServices>();
        builder.Services.AddScoped<IProjectServices, ProjectServices>();
        builder.Services.AddScoped<ITaskStatusServices, TaskStatusServices>();
        builder.Services.AddScoped<IUserServices, UserServices>();

   var app = builder.Build();
   // Configure the HTTP request pipeline.
   if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(config => { config.SwaggerEndpoint("/swagger/v1/swagger.json", "Marketing CRM API v1.0"); });
        }

   app.UseHttpsRedirection();
   app.UseAuthorization();
   app.MapControllers();
   app.Run();
}