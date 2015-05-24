using System;


namespace appPDU.Models
{
    public class ElementModel
    {
		private ObjectModel _model;
		public ElementModel(ObjectModel model)
		{
			_model = model;
		}
        public Guid Id 
		{
			get{ return _model.Id;} 
			set{ _model.Id = value;} 
		}
		
        public Guid ParentId { get; set; }

        public string Title { get; set; }
        public string Name { get; set; }
		
		public int Order{get;set;}

        public string Data { get; set; }

        public string MetaData { get; set; }
		

    }

}