using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using VNGExercises.Domain.Entities;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using VNGExercises.Persistence;

namespace VNGExercises.Infrastructure.BackgroundJobs
{
    public class SendEmailToUserRequiredToUpdatePasswordJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SendEmailToUserRequiredToUpdatePasswordJob> _logger;
        private readonly SendEmailOptions _sendEmailOptions;

        public SendEmailToUserRequiredToUpdatePasswordJob(ApplicationDbContext dbContext, ILogger<SendEmailToUserRequiredToUpdatePasswordJob> logger, SendEmailOptions sendEmailOptions)
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
                                                                    t.IsRequiredUpdatedPwd
                                                         )
                                                        .Take(20)
                                                        .ToListAsync();

                if (userHasBeenNotUpdatedPwd.Count == 0) return;

                foreach (var user in userHasBeenNotUpdatedPwd)
                {

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when send email in background job. [ERROR:::{ex.ToString()}]");
                throw;
            }
        }
    }
}
