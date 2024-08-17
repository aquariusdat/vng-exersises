namespace VNGExercises.Infrastructure.Abstractions
{
    public interface ISendMailService
    {
        Task<bool> SendAsync(string EmailTo, string EmailSubject, string EmailBody);
    }
}
