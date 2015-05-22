using System;


namespace appPDU.Models
{
    public class ObjectModel
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
		
		public int Type { get; set; }
		/*
		public int CountryId { get; set; }
		*/

        public string Data { get; set; }

        public string MetaData { get; set; }
		
		public DateTime DateCreate { get; set; }
		
		public DateTime DateClose {get;set;}

    }

}
