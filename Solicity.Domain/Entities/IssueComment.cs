namespace Solicity.Domain.Entities
{
    public class IssueComment : BaseEntity
    {
        public Guid IssueId { get; set; }
        public string Comment { get; set; }
        public User Author { get; set; }
    }
}
