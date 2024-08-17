using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using VNGExercises.Infrastructure.Abstractions;
using VNGExercises.Infrastructure.BackgroundJobs;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using VNGExercises.Infrastructure.Services;

namespace VNGExercises.Infrastructure.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBackgroundJobInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var sendEmailOptions = configuration.GetSection("SendEmailOptions").Get<SendEmailOptions>();
            services.AddSingleton(sendEmailOptions);
            var mailSettings = configuration.GetSection("MailSettings").Get<MailSettings>();
            services.AddSingleton(mailSettings);

            services.AddTransient<ISendMailService, SendMailService>();
        }


        public static void AddQuartzInfrastructure(this IServiceCollection services)
        {
            services.AddQuartz(config =>
            {
                var sendMailToUserRequiredToUpdatePasswordJobKey = new JobKey(nameof(SendEmailToUserRequiredToUpdatePasswordJob));

                config.AddJob<SendEmailToUserRequiredToUpdatePasswordJob>(sendMailToUserRequiredToUpdatePasswordJobKey)
                .AddTrigger(trigger =>
                            trigger.ForJob(sendMailToUserRequiredToUpdatePasswordJobKey)
                                    .WithSimpleSchedule(schedule =>
                                                        schedule.WithInterval(TimeSpan.FromSeconds(100))
                                                        .RepeatForever()));


                var checkIfUserNeedsToUpdateThePasswordJobKey = new JobKey(nameof(CheckIfUserNeedsToUpdateThePasswordJob));

                config.AddJob<CheckIfUserNeedsToUpdateThePasswordJob>(checkIfUserNeedsToUpdateThePasswordJobKey)
                .AddTrigger(trigger =>
                            trigger.ForJob(checkIfUserNeedsToUpdateThePasswordJobKey)
                                    .WithSimpleSchedule(schedule =>
                                                        schedule.WithInterval(TimeSpan.FromMicroseconds(100))
                                                        .RepeatForever()));

                config.UseMicrosoftDependencyInjectionJobFactory();
            });

            services.AddQuartzHostedService();
        }
    }
}
