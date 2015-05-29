using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;

namespace appPDU.Models
{
    public class WebPageModel : IObjectModel
    {
        private IObjectModel _model;
        private WebPageMetadata _metadata;
        public WebPageModel()
        {

        }
        public WebPageModel(IObjectModel model)
        {
            AddInternalObject(model);
        }
        public Guid Id
        {
            get { return _model.Id; }
            set { _model.Id = value; }
        }

        public Guid ParentId
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
        public string Data { get; set; }

        public string MetaData { get; set; }

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
        public List<ObjectModel> Children { get; set; }

        public string Description {
            get { return _metadata.Description; }
            set { _metadata.Description = value; }
        }

        public void AddInternalObject(IObjectModel model)
        {
            var settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            model.TypeName = "webpage";
            model.Type = 1;
            if (model.Metadata != null)
            {
                _metadata = JsonConvert.DeserializeObject<WebPageMetadata>(model.Metadata, settings);
            }
            else
            {
                _metadata = new WebPageMetadata();
                _metadata.ChildrenIds = new List<Guid>();
            }
            Children = new List<ObjectModel>();

            _model = model;
        }
    }

}