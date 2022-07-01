namespace Solicity.Domain.Entities
{
    public class Topic : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public bool Enabled { get; set; }
    }
}
