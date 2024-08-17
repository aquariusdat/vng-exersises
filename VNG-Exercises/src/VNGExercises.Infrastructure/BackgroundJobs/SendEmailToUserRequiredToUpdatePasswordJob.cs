using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;
using VNGExercises.Contract.Abstractions.Message;
using VNGExercises.Contract.Services.V1.User;
using VNGExercises.Infrastructure.Abstractions;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using VNGExercises.Persistence;
using VNGExercises.Persistence.Outbox;

namespace VNGExercises.Infrastructure.BackgroundJobs
{
    public class SendEmailToUserRequiredToUpdatePasswordJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ConvertUserRequiredUpdatedPwdToOutboxMessages> _logger;
        private readonly SendEmailOptions _sendEmailOptions;
        private readonly ISendMailService _sendMailService;

        public SendEmailToUserRequiredToUpdatePasswordJob(ApplicationDbContext dbContext, ILogger<ConvertUserRequiredUpdatedPwdToOutboxMessages> logger, SendEmailOptions sendEmailOptions, ISendMailService sendMailService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _sendEmailOptions = sendEmailOptions;
            _sendMailService = sendMailService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var outboxMessages = await _dbContext.Set<OutboxMessage>()
                                                        .Where(t =>
                                                                    t.Type == nameof(UserRequiredPwdEvent)
                                                                    && t.ProcessedOnUtc == null
                                                         )
                                                        .Take(100)
                                                        .ToListAsync();

                if (outboxMessages.Count == 0) return;

                foreach (var outboxMessage in outboxMessages)
                {
                    IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(outboxMessage.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                    if (domainEvent is null)
                        continue;

                    var userRequiredUpdatePwdEvent = JsonConvert.DeserializeObject<Contract.Services.V1.User.UserRequiredPwdEvent>(outboxMessage.Content, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                    });

                    await _sendMailService.SendAsync(userRequiredUpdatePwdEvent.Email, "Password update required notification", "Password update required notification");

                    outboxMessage.ProcessedOnUtc = DateTime.Now;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when check if user needs to update the password in background job. [ERROR:::{ex.ToString()}]");
                throw;
            }
        }
    }
}
