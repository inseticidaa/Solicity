using Solicity.Domain.Enums;

namespace Solicity.Domain.Entities
{
    public class Issue : BaseEntity
    {
        public Guid TopicId { get; set; }
        public User Author { get; set; }
        public Topic Topic { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public IssueStatusEnum Status { get; set; }
    }
}
