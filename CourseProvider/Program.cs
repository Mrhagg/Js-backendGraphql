using CourseProvider;
using CourseProvider.Infrastructure.Data.Context;
using CourseProvider.Infrastructure.GrapQl;
using CourseProvider.Infrastructure.GrapQl.Mutations;
using CourseProvider.Infrastructure.GrapQl.ObjectTypes;
using CourseProvider.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;



var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddPooledDbContextFactory<DataContext>(x => 
        {
            x.UseCosmos(Environment.GetEnvironmentVariable("COSMOS_URI")!, Environment.GetEnvironmentVariable("COSMOS_DBNAME")!)
                .UseLazyLoadingProxies();
        }
        );
       
        services.AddScoped<ICourseService, CourseService>();


        services.AddGraphQLFunction()
            .AddQueryType<Query>()
            .AddMutationType<CourseMutation>()
            .AddType<CourseType>();
            


        var sp = services.BuildServiceProvider();
        using var scope = sp.CreateScope();
        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DataContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.EnsureCreated();
    })
      .Build();

host.Run();
