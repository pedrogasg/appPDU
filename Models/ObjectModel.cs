using System;


namespace appPDU.Models
{
    public class ObjectModel:IObjectModel
    {
		public void AddInternalObject(IObjectModel model)
		{
			
		}

        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
		
		public int Type { get; set; }
		
		public int ChildTypeMask{get;set;}
		/*
		public int CountryId { get; set; }
		*/
		public int Order{get;set;}

        public string Data { get; set; }

        public string Metadata { get; set; }
		
		public DateTime DateCreate { get; set; }
		
		public DateTime DateClose {get;set;}
		
		public bool Visible{get;set;}

    }

}
