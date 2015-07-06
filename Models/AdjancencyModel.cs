using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appPDU.Models
{
    public class AdjacencyModel
    {
        public ObjectModel Predecessor { get; set; }
        public Guid PredecessorId { get; set; }

        public ObjectModel Successor { get; set; }

        public Guid SuccessorId { get; set; }
    }
}
