using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace appPDU.Models
{
    public class ContainerModel : IObjectModel
    {
        private IObjectModel _model;
        private ContainerMetadata _metadata;
        public ContainerModel() { }
        public ContainerModel(IObjectModel model)
        {
            AddInternalObject(model);
        }
        public void AddInternalObject(IObjectModel model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            model.TypeName = "Container";
            model.Type = 4;
            if (model.Metadata != null)
            {
                _metadata = JsonConvert.DeserializeObject<ContainerMetadata>(model.Metadata, settings);
            }
            else
            {
                _metadata = new ContainerMetadata();
                _metadata.Attributes = new ContainerAttributes();
            }
            _model = model;
            Children = new List<IObjectModel>();
        }

        public IObjectModel GetPlainModel()
        {
            return _model.GetPlainModel();
        }

        public ContainerAttributes Attributes
        {
            get { return _metadata.Attributes; }
        }

        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; }
        }

        public Guid? ParentId
        {
            get { return _model.ParentId; }
            set { _model.ParentId = value; }
        }

        public string Title
        {
            get { return _model.Title; }
            set { _model.Title = value; }
        }
        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }

        public int Order
        {
            get { return _model.Order; }
            set { _model.Order = value; }
        }

        public string Subtype
        {
            get { return _metadata.SubType; }
        }
        public int Type
        {
            get { return _model.Type; }
            set { _model.Type = value; }
        }
        public int ChildTypeMask
        {
            get { return _model.ChildTypeMask; }
            set { _model.ChildTypeMask = value; }
        }
        public string Data
        {
            get { return _model.Data; }
            set { _model.Data = value; }
        }

        public string Metadata
        {
            get { return _model.Metadata; }
            set { _model.Metadata = value; }
        }

        public DateTime DateCreate
        {
            get { return _model.DateCreate; }
            set { _model.DateCreate = value; }
        }
        public DateTime DateClose
        {
            get { return _model.DateClose; }
            set { _model.DateClose = value; }
        }

        public bool Visible
        {
            get { return _model.Visible; }
            set { _model.Visible = value; }
        }

        public IList<IObjectModel> Children { get; set; }

        public string TypeName
        {
            get
            {
                return _model.TypeName;
            }

            set
            {
                _model.TypeName = value;
            }
        }

        public int Version
        {
            get
            {
                return _model.Version;
            }

            set
            {
                _model.Version = value;
            }
        }

        public ICollection<AdjacencyModel> Predecessors
        {
            get
            {
                return _model.Predecessors;
            }

            set
            {
                _model.Predecessors = value;
            }
        }

        public ICollection<AdjacencyModel> Successors
        {
            get
            {
                return _model.Successors;
            }

            set
            {
                _model.Successors = value;
            }
        }
    }

}