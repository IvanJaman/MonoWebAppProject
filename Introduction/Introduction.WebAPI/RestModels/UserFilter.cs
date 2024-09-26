namespace Introduction.WebAPI.RestModels
{
    public class UserFilter
    {
        public string? SearchQuery { get; set; }
        public DateTime? DateOfBirthFrom { get; set; }
        public DateTime? DateOfBirthTo { get; set; }
        public char? Sex { get; set; }
    }
}
