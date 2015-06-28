using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace appPDU.Models
{
    public class ObjectModel : IObjectModel
    {
        public void AddInternalObject(IObjectModel model) { }

        public IObjectModel GetPlainModel()
        {
            return this;
        }

        [BsonElement("id")]
        public Guid Id { get; set; }
        [BsonElement("parentId")]
        public Guid? ParentId { get; set; }
        [BsonElement("title")]
        public string Title { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("type")]
        public int Type { get; set; }
        [BsonElement("typeName")]
        public string TypeName { get; set; }
        [BsonElement("childTypeMask")]
        public int ChildTypeMask { get; set; }
        /*
		public int CountryId { get; set; }
		*/
        [BsonElement("order")]
        public int Order { get; set; }
        [BsonElement("data")]
        public string Data { get; set; }
        [BsonElement("metadata")]
        public string Metadata { get; set; }
        [BsonElement("dateCreate")]
        public DateTime DateCreate { get; set; }
        [BsonElement("dateClose")]
        public DateTime DateClose { get; set; }
        [BsonElement("visible")]
        public bool Visible { get; set; }
        public int Version { get; set; }
        [BsonElement("childrenIds")]
        public IList<Guid> ChildrenIds { get; set; }
    }

}
