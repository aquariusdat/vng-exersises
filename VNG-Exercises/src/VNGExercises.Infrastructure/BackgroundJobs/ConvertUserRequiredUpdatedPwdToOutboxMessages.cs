using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using VNGExercises.Contract.Services.V1.User;
using VNGExercises.Domain.Abstractions.Entities;
using VNGExercises.Domain.Entities;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using VNGExercises.Persistence;
using VNGExercises.Persistence.Outbox;

namespace VNGExercises.Infrastructure.BackgroundJobs
{
    public class ConvertUserRequiredUpdatedPwdToOutboxMessages : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ConvertUserRequiredUpdatedPwdToOutboxMessages> _logger;
        private readonly SendEmailOptions _sendEmailOptions;

        public ConvertUserRequiredUpdatedPwdToOutboxMessages(ApplicationDbContext dbContext, ILogger<ConvertUserRequiredUpdatedPwdToOutboxMessages> logger, SendEmailOptions sendEmailOptions)
        {
            _dbContext = dbContext;
            _logger = logger;
            _sendEmailOptions = sendEmailOptions;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var userHasBeenNotUpdatedPwd = await _dbContext.Set<User>()
                                                        .Where(t =>
                                                                    //(t.LastUpdatedPwd.HasValue && (DateTime.Now - t.LastUpdatedPwd.Value).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd)
                                                                    //&& (!t.LastUpdatedPwd.HasValue && (DateTime.Now - t.CreatedAt).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd)
                                                                    (
                                                                        DateTime.Now - (t.LastUpdatedPwd.HasValue ? t.LastUpdatedPwd.Value : t.CreatedAt)
                                                                    ).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd
                                                                    && t.Status != "REQUIRE_CHANGE_PWD"
                                                         )
                                                        .Take(20)
                                                        .ToListAsync();

                if (userHasBeenNotUpdatedPwd.Count == 0) return;

                userHasBeenNotUpdatedPwd.ForEach(t => t.Status = "REQUIRE_CHANGE_PWD");

                var outboxMessages = userHasBeenNotUpdatedPwd
                               .Select(t => new UserRequiredPwdEvent()
                               {
                                   EventId = Guid.NewGuid(),
                                   Id = t.Id,
                                   Email = t.Email,
                                   FirstName = t.FirstName,
                                   LastName = t.LastName,
                                   FullName = t.FullName,
                                   LastUpdatedPwd = t.LastUpdatedPwd,
                                   Status = t.Status,
                                   TimeStamp = DateTime.Now
                               })
                               .Select(domainEvent => new OutboxMessage
                               {
                                   Id = Guid.NewGuid(),
                                   OccurredOnUtc = DateTime.Now,
                                   Type = domainEvent.GetType().Name,
                                   Content = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                                   {
                                       TypeNameHandling = TypeNameHandling.All,
                                   }),
                               })
                               .ToList();

                _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when add the user needs to update the password in background job. [ERROR:::{ex.ToString()}]");
                throw;
            }
        }
    }
}
