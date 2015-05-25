using System;


namespace appPDU.Models
{
    public class WebPageModel
    {
		private ObjectModel _model;
		public WebPageModel(ObjectModel model)
		{
			_model = model;
		}
        public Guid Id 
		{
			get{ return _model.Id;} 
			set{ _model.Id = value;} 
		}
		
        public Guid ParentId
		{
			get{ return _model.ParentId;} 
			set{ _model.ParentId = value;} 
		}

        public string Title
		{
			get{ return _model.Title;} 
			set{ _model.Title = value;} 
		}
        public string Name
		{
			get{ return _model.Name;} 
			set{ _model.Name = value;} 
		}
		
		public int Order
		{
			get{ return _model.Order;} 
			set{ _model.Order = value;} 
		}
        public string Data { get; set; }

        public string MetaData { get; set; }
		

    }

}