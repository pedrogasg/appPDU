using appPDU.Builders;
using System;
namespace appPDU.Models
{
	public interface IObjectModel
	{
		void AddInternalObject(IObjectModel objectModel);
		 Guid Id { get; set; }
         Guid ParentId { get; set; }

         string Title { get; set; }
         string Name { get; set; }
		
		 int Type { get; set; }
		 string TypeName { get; set; }
		 int ChildTypeMask{ get; set; }
		/*
		 int CountryId { get; set; }
		*/
		 int Order{ get; set; }

         string Data { get; set; }

         string Metadata { get; set; }
		
		 DateTime DateCreate { get; set; }
		
		 DateTime DateClose { get; set; }
		
		 bool Visible{ get; set; }
		 
	}
}