using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace appPDU.Models
{
    public class ContainerModel:IObjectModel
    {
		private IObjectModel _model;
        private ContainerMetaData _metadata;
		public ContainerModel(){}
		public ContainerModel(IObjectModel model)
		{
			AddInternalObject(model);
		}
		public void AddInternalObject(IObjectModel model)
		{
			_model = model;
			if(model.Metadata != null){
				_metadata = JsonConvert.DeserializeObject<ContainerMetaData>(model.Metadata);
			}else{
                _metadata = new ContainerMetaData();
				_metadata.Attributes = new ContainerAttributes();
				_metadata.ChildrenIds = new List<Guid>();
			}
		}
		public ContainerAttributes Attributes 
		{
			get{ return _metadata.Attributes;}
		}
		
		public IList<Guid> ChildrenIds
		{
			get { return _metadata.ChildrenIds;}
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
        
        public string Subtype
        {
            get { return _metadata.SubType; }
        }	
		public int Type { get; set; }
		
		public int ChildTypeMask{get;set;}	
        public string Data { get; set; }

        public string Metadata { get; set; }
		
		public DateTime DateCreate { get; set; }
		
		public DateTime DateClose {get;set;}
		
		public bool Visible{get;set;}

        public IList<ObjectModel> Children { get; set; }
    }

}