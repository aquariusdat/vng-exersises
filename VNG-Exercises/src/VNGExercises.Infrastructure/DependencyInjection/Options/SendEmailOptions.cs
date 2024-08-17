namespace VNGExercises.Infrastructure.DependencyInjection.Options
{
    public record SendEmailOptions
    {
        public int NumberOfDaysToRequireUsersToChangePwd { get; set; }
    }
}
