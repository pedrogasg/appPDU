using System;
using System.Collections.Generic;
namespace appPDU.Models
{
    public class ContainerMetaData
    {
        public ContainerAttributes Attributes { get; set; }
        public IList<Guid> ChildrenIds { get; set; }
        public string SubType { get; set; }
    }
}