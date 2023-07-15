using System;

namespace PaperTrade.BusinessLogic.Services.Briefs.Requests
{
    public class BriefPutRequest
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? Document { get; set; }
        public Guid? Preview { get; set; }
        public Guid? Author { get; set; }
        public string? Description { get; set; }
    }
}
