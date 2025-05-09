namespace CoreEmailProject.Entities
{
    public class EmailMessage
    {
        public int Id { get; set; }
        public string SenderEmailAddress { get; set; }
        public string RecipientEmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool IsRead { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime SentAt { get; set; }
    }
}