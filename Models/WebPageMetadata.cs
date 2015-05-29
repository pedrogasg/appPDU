using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appPDU.Models
{
    public class WebPageMetadata
    {
        public string Description { get; set; }
        public IList<Guid> ChildrenIds { get; set; }
        public string Template { get; set; }
        public ContainerAttributes Attributes { get; set; }

    }
}
