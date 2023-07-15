namespace PaperTrade.BusinessLogic.Services.Briefs.Requests
{
    public class BriefPostRequest
    {
        public string Name { get; set; }
        public Guid DocumentId { get; set; }
        public Guid ImageId { get; set; }
        public Guid AuthorId { get; set; }
        public string Description { get; set; }
    }
}
