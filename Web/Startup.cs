using Application;
using Domain;
using Infrastructure;
using Infrastructure.Errors;
using Infrastructure.Services.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Persistense;
using System;
using System.Diagnostics;
using System.Text.Json;
using Web.Filters.ProgramCode;

namespace Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, ProgramCodeAuthorizationHandler>();
        services.AddApplicationInsightsTelemetry();
        services.AddApplication();
        services.AddInfrastructure(Configuration);
        services.AddPersistence(Configuration);

        services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("*").WithHeaders("*").WithMethods("POST", "PUT", "GET", "DELETE").WithExposedHeaders("Content-Disposition")));

        services.AddControllers(options =>
        {
            options.ModelBinderProviders.RemoveType<DateTimeModelBinderProvider>();
            AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().AddRequirements(new ProgramCodeRequirement()).Build();
            options.Filters.Add(new AuthorizeFilter(policy));
        }).AddJsonOptions(options =>
        {
            options.UseDateOnlyTimeOnlyStringConverters();
            options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
            options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
           {
               options.Authority = Configuration.GetValue<string>("Configuration:IdentityUrl");
               options.RequireHttpsMetadata = false;
               options.Audience = "ccs";
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = TimeSpan.FromMinutes(3)
               };
           });

        services.AddSpaStaticFiles(configuration =>
        {
            configuration.RootPath = "ClientApp/dist/rx";
        });
        services.AddSignalR();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors();

        string[] supportedCultures = new[] { "en-US" };

        RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0]).AddSupportedCultures(supportedCultures).AddSupportedUICultures(supportedCultures);

        app.UseRequestLocalization(localizationOptions);

        ServiceActivator.Configure(app.ApplicationServices);

        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseAuthentication();
        app.UseStaticFiles();

        if (!env.IsDevelopment() || !Debugger.IsAttached) app.UseSpaStaticFiles();

        app.UseRouting();
        app.UseAuthorization();

        string pattern = env.IsDevelopment() && Debugger.IsAttached ? "{controller=Empty}/{action=Index}" : "{controller}/{action=Index}/{id?}";

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(name: "default", pattern: pattern);
            endpoints.MapHub<JobHub>("/job");
        });

        app.UseSpa(spa => spa.Options.SourcePath = "ClientApp");
    }
}
