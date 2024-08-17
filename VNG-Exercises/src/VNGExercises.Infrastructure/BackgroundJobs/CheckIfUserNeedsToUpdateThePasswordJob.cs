using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Quartz;
using VNGExercises.Domain.Entities;
using VNGExercises.Infrastructure.DependencyInjection.Options;
using VNGExercises.Persistence;

namespace VNGExercises.Infrastructure.BackgroundJobs
{
    public class CheckIfUserNeedsToUpdateThePasswordJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SendEmailToUserRequiredToUpdatePasswordJob> _logger;
        private readonly SendEmailOptions _sendEmailOptions;

        public CheckIfUserNeedsToUpdateThePasswordJob(ApplicationDbContext dbContext, ILogger<SendEmailToUserRequiredToUpdatePasswordJob> logger, SendEmailOptions sendEmailOptions)
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
                                                                    !t.IsRequiredUpdatedPwd &&
                                                                    //(t.LastUpdatedPwd.HasValue && (DateTime.Now - t.LastUpdatedPwd.Value).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd)
                                                                    //&& (!t.LastUpdatedPwd.HasValue && (DateTime.Now - t.CreatedAt).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd)
                                                                    (
                                                                        DateTime.Now - (t.LastUpdatedPwd.HasValue ? t.LastUpdatedPwd.Value : t.CreatedAt)
                                                                    ).TotalDays >= _sendEmailOptions.NumberOfDaysToRequireUsersToChangePwd
                                                         )
                                                        .Take(100)
                                                        .ToListAsync();

                if (userHasBeenNotUpdatedPwd.Count == 0) return;

                foreach (var user in userHasBeenNotUpdatedPwd)
                {
                    user.IsRequiredUpdatedPwd = true;
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
