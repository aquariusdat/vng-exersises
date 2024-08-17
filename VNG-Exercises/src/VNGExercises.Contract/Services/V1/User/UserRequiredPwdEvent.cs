using VNGExercises.Contract.Abstractions.Message;

namespace VNGExercises.Contract.Services.V1.User
{
    public class UserRequiredPwdEvent : IDomainEvent
    {
        public Guid EventId { get; set; }
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime? LastUpdatedPwd { get; set; }
    }
}
