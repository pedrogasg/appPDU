using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace appPDU.Models
{
    public class WebPageModel : IObjectModel
    {
        private IObjectModel _model;
        private WebPageMetadata _metadata;
        [BsonElement("childrenIds")]
        public IList<Guid> ChildrenIds
        {
            get { return _model.ChildrenIds; }
            set { _model.ChildrenIds = value; }
        }
        public WebPageModel()
        {

        }
        public WebPageModel(IObjectModel model)
        {
            AddInternalObject(model);
        }
        [BsonElement("id")]
        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; }
        }
        [BsonElement("parentId")]
        public Guid ParentId
        {
            get { return _model.ParentId; }
            set { _model.ParentId = value; }
        }
        [BsonElement("title")]
        public string Title
        {
            get { return _model.Title; }
            set { _model.Title = value; }
        }
        [BsonElement("name")]
        public string Name
        {
            get { return _model.Name; }
            set { _model.Name = value; }
        }
        [BsonElement("order")]
        public int Order
        {
            get { return _model.Order; }
            set { _model.Order = value; }
        }
        [BsonElement("data")]
        public string Data { get; set; }

        [BsonElement("type")]
        public int Type
        {
            get
            {
                return _model.Type;
            }

            set
            {
                _model.Type = value;
            }
        }
        [BsonElement("typeName")]
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
        [BsonElement("childTypeMask")]
        public int ChildTypeMask
        {
            get
            {
                return _model.ChildTypeMask;
            }

            set
            {
                _model.ChildTypeMask = value;
            }
        }
        [BsonElement("metadata")]
        public string Metadata
        {
            get
            {
                var settings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                _model.Metadata = JsonConvert.SerializeObject(_metadata, settings);
                return _model.Metadata;
            }

            set
            {
                _model.Metadata = value;
            }
        }
        [BsonElement("dateCreate")]
        public DateTime DateCreate
        {
            get
            {
                return _model.DateCreate;
            }

            set
            {
                _model.DateCreate = value;
            }
        }
        [BsonElement("dateClose")]
        public DateTime DateClose
        {
            get
            {
                return _model.DateClose;
            }

            set
            {
                _model.DateClose = value;
            }
        }
        [BsonElement("visible")]
        public bool Visible
        {
            get
            {
                return _model.Visible;
            }

            set
            {
                _model.Visible = value;
            }
        }
        [BsonIgnore]
        public List<ObjectModel> Children { get; set; }
        [BsonIgnore]
        public string Description {
            get { return _metadata.Description; }
            set { _metadata.Description = value; }
        }
        [BsonIgnore]
        public Guid Template
        {
            get
            {
                return _metadata.Template;
            }
            set
            {
                _metadata.Template = value;
            }
        }

        public void AddInternalObject(IObjectModel model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            model.TypeName = "WebPage";
            model.Type = 1;
            if (model.Metadata != null)
            {
                _metadata = JsonConvert.DeserializeObject<WebPageMetadata>(model.Metadata, settings);
            }
            else
            {
                _metadata = new WebPageMetadata();
                _model.ChildrenIds = new List<Guid>();
            }
            Children = new List<ObjectModel>();

            _model = model;
        }

    }

}