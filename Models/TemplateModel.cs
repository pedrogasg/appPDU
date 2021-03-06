﻿using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appPDU.Models
{
    public class TemplateModel:IObjectModel
    {
        private IObjectModel _model;
        private TemplateMetadata _metadata;
        public TemplateModel(IObjectModel model)
        {
            AddInternalObject(model);
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

        public string Data
        {
            get
            {
                return _model.Data;
            }

            set
            {
                _model.Data = value;
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

        public Guid Id
        {
            get
            {
                return _model.Id;
            }

            set
            {
                _model.Id = value;
            }
        }

        public string Metadata
        {
            get
            {
                return _model.Metadata;
            }

            set
            {
                _model.Metadata = value;
            }
        }

        public string Name
        {
            get
            {
                return _model.Name;
            }

            set
            {
                _model.Name = value;
            }
        }

        public int Order
        {
            get
            {
                return _model.Order;
            }

            set
            {
                _model.Order = value;
            }
        }

        public Guid? ParentId
        {
            get
            {
                return _model.ParentId;
            }

            set
            {
                _model.ParentId = value;
            }
        }

        public string Title
        {
            get
            {
                return _model.Title;
            }

            set
            {
                _model.Title = value;
            }
        }

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

        public void AddInternalObject(IObjectModel model)
        {
            if (model.Metadata != null)
            {
                _metadata = JsonConvert.DeserializeObject<TemplateMetadata>(model.Metadata);
            }else
            {
                _metadata = new TemplateMetadata();
            }
            _model = model;
        }

        public IObjectModel GetPlainModel()
        {
            return _model.GetPlainModel();
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

        public IObjectModel GetPlainCopy()
        {
            return _model.GetPlainCopy();
        }
    }
}
