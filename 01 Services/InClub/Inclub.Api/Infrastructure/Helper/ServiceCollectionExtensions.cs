using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using OpenTracing;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing.Util;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;
using System.Net.Http;

namespace Inclub.Infrastucture.Extensions
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration, string appName = null, string version = "v1", string descripcion = "Documentación de los métodos u operaciones del servicio.")
        {
            string customtTitle = "API ejecutandose en [" + Dns.GetHostName() + "]";
            if (!string.IsNullOrWhiteSpace(appName))
            {
                customtTitle = appName + " ejecutandose en [" + Dns.GetHostName() + "]";
            }

            services.AddSwaggerGen(delegate (SwaggerGenOptions options)
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = customtTitle,
                    Version = version,
                    Description = descripcion,
                    TermsOfService = null
                });
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Description = "Encabezado de autorización estándar utilizando el esquema de bearer (portador). Ejemplo: \"bearer {token}\""
                });
                //options.OperationFilter<AuthorizeCheckOperationFilter>(Array.Empty<object>());
            });
            return services;
        }

        public static IServiceCollection AddCustomVersioning(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(delegate (ApiVersioningOptions o)
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            return services;
        }

        public static IServiceCollection AddIntegrationTracerServices(this IServiceCollection services, IConfiguration configuration, string appName)
        {
            services.AddOpenTracing(delegate (IOpenTracingBuilder builder)
            {
                builder.ConfigureAspNetCore(delegate (AspNetCoreDiagnosticOptions options)
                {
                    options.Hosting.IgnorePatterns.Add((HttpContext x) => x.Request.Path == "/hc");
                    options.Hosting.IgnorePatterns.Add((HttpContext x) => x.Request.Path == "/liveness");
                });
            });
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new Exception("[JAEGER] No se declarado el nombre de servicio");
            }

            if (configuration.GetValue<bool>("Jaeger:Enable"))
            {
                services.AddSingleton((Func<IServiceProvider, ITracer>)delegate (IServiceProvider serviceProvider)
                {
                    string serviceName = appName;
                    ILoggerFactory requiredService = serviceProvider.GetRequiredService<ILoggerFactory>();
                    RemoteReporter reporter = new RemoteReporter.Builder().WithSender(new UdpSender(configuration["Jaeger:udpHost"], configuration.GetValue<int>("Jaeger:udpPort"), configuration.GetValue<int>("Jaeger:maxPackageSize"))).WithLoggerFactory(requiredService).Build();
                    ISampler sampler = new ConstSampler(sample: true);
                    Tracer tracer = new Tracer.Builder(serviceName).WithLoggerFactory(requiredService).WithReporter(reporter).WithSampler(sampler)
                        .Build();
                    GlobalTracer.Register(tracer);
                    return tracer;
                });
                services.Configure(delegate (HttpHandlerDiagnosticOptions options)
                {
                    options.IgnorePatterns.Add((HttpRequestMessage request) => new Uri(string.Format("http://{0}:{1}/api/traces", configuration["Jaeger:udpHost"], configuration.GetValue<int>("Jaeger:udpPort"))).IsBaseOf(request.RequestUri));
                });
            }

            return services;
        }
    }
}