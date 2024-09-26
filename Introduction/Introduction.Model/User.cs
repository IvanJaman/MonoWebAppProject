namespace Introduction.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public char? Sex { get; set; }
        public List<FacebookPost>? Posts { get; set; }
    }
}
