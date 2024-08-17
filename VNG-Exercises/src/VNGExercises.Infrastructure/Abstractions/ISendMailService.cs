namespace VNGExercises.Infrastructure.Abstractions
{
    public interface ISendMailService
    {
        Task<bool> SendAsync(string ToEmailId, string ToEmailName, string EmailSubject, string EmailBody);
    }
}
