using System;
using System.Collections.Generic;
namespace appPDU.Models
{
	public class ContainerMetaData
	{
		public ContainerAttributes Attributes { get; set; }
		public IList<Guid> Children{get;set;}
		
	}
}