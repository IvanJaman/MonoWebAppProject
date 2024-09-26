namespace Introduction.Model
{
    public class FacebookPost
    {
        public Guid Id { get; set; }
        public string? Caption { get; set; }
        public Guid UserId { get; set; }
        public DateTime PostedAt = DateTime.UtcNow;
    }
}
