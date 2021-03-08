using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using TestCQRS.Models;
using TestCQRS.Validators;
using TestCQRS.Providers;
using TestCQRS.Providers.Implementations;
using TestCQRS.Lifecycle;
using TestCQRS.Types;
using TestCQRS.Services;
using TestCQRS.Services.Implementations;

namespace TestCQRS
{
    public class Startup
    {
        public delegate ISalaryCalculator SalaryCalculatorDelegate(DeveloperLevel developerLevel);

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));

            services.AddSingleton<FakeDataStore>();

            services.AddControllers().AddFluentValidation();
            services.AddTransient<IValidator<MakeOrderRequestModel>, MakeOrderRequestModelValidators>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TestCQRS", Version = "v1" });
            });

            services.AddTransient<ITransientService, TransientService>();
            services.AddScoped<IScopedService, ScopedService>();
            services.AddSingleton<ISingletonService, SingletonService>();

            services.AddScoped<ITranscriptionService, AzureTranscriptionService>();
            services.AddScoped<ITranscriptionService, SpeechmaticTranscriptionService>();

            var emailConfiguration = Configuration.GetValue<string>("EmailConfiguration");
            services.AddSingleton((Func<IServiceProvider, IEmailProvider>)(serviceProvider =>
            {
                if (emailConfiguration.Equals("SendGrid"))
                {
                    return new SendGridProvider();
                }
                else
                {
                    return new SmtpProvider();
                }
            }));

            services.AddTransient<SeniorDevSalaryCalculator>();
            services.AddTransient<JuniorDevSalaryCalculator>();
            services.AddTransient<SalaryCalculatorDelegate>(provider => developerLevel =>
                developerLevel switch
                {
                    DeveloperLevel.Senior => provider.GetService<SeniorDevSalaryCalculator>(),
                    DeveloperLevel.Junior => provider.GetService<JuniorDevSalaryCalculator>(),
                    _ => throw new NotImplementedException()
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestCQRS v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
