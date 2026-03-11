namespace PaperTrade.BusinessLogic.Services.Users.Requests
{
    public class UserPostRequest
    {
        public string ObjectIdentifier { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
    }
}