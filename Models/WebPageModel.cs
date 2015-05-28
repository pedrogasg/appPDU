using System;


namespace appPDU.Models
{
    public class WebPageModel : IObjectModel
    {
        private IObjectModel _model;
        public WebPageModel(IObjectModel model)
        {
            _model = model;
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

        public void AddInternalObject(IObjectModel objectModel)
        {
            _model.AddInternalObject(objectModel);
        }
    }

}