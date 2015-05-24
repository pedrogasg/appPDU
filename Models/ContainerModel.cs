using System;
using Newtonsoft.Json;

namespace appPDU.Models
{
    public class ContainerModel
    {
		private ObjectModel _model;
		private ContainerAttributes _containerAttributes;
		public ContainerModel(ObjectModel model)
		{
			_model = model;
			_containerAttributes = JsonConvert.DeserializeObject<ContainerAttributes>(model.MetaData);
		}
		public ContainerAttributes Attributes 
		{
			get{ return _containerAttributes;}
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

    }

}