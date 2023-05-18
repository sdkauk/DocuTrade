using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaperTrade.Common.Models
{
    public class BasicBrief
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public BasicBrief()
        {

        }

        public BasicBrief(Brief brief)
        {
            Id = brief.Id;
            Name = brief.Name;
        }
    }
}
