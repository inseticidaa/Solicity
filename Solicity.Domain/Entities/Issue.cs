using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solicity.Domain.Entities
{
    public class Issue: BaseEntity
    {
        public Guid TopicId { get; set; }
        public string Code { get; set; }
        public bool IsClosed { get; set; }
        public string Title { get; set; }
    }
}
