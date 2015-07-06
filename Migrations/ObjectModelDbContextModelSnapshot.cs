using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using appPDU.DataLayer;

namespace appPDU.Migrations
{
    [ContextType(typeof(ObjectModelDbContext))]
    partial class ObjectModelDbContextModelSnapshot : ModelSnapshot
    {
        public override void BuildModel(ModelBuilder builder)
        {
            builder
                .Annotation("SqlServer:DefaultSequenceName", "DefaultSequence")
                .Annotation("SqlServer:Sequence:.DefaultSequence", "'DefaultSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .Annotation("SqlServer:ValueGeneration", "Sequence");
            
            builder.Entity("appPDU.Models.AdjacencyModel", b =>
                {
                    b.Property<Guid>("PredecessorId");
                    
                    b.Property<Guid>("SuccessorId");
                    
                    b.Key("PredecessorId", "SuccessorId");
                });
            
            builder.Entity("appPDU.Models.ObjectModel", b =>
                {
                    b.Property<Guid>("Id")
                        .GenerateValueOnAdd()
                        .Annotation("Relational:ColumnDefaultExpression", "NEWSEQUENTIALID()");
                    
                    b.Property<int>("ChildTypeMask");
                    
                    b.Property<string>("Data")
                        .Annotation("Relational:ColumnType", "varchar(max)");
                    
                    b.Property<DateTime>("DateClose");
                    
                    b.Property<DateTime>("DateCreate")
                        .Annotation("Relational:ColumnDefaultExpression", "GETDATE()")
                        .Annotation("Relational:ColumnType", "datetime2");
                    
                    b.Property<string>("Metadata")
                        .Annotation("Relational:ColumnType", "varchar(2048)");
                    
                    b.Property<string>("Name")
                        .Required()
                        .Annotation("MaxLength", 256);
                    
                    b.Property<int>("Order");
                    
                    b.Property<Guid?>("ParentId");
                    
                    b.Property<string>("Title")
                        .Annotation("MaxLength", 1024);
                    
                    b.Property<int>("Type");
                    
                    b.Property<string>("TypeName")
                        .Required()
                        .Annotation("MaxLength", 54);
                    
                    b.Property<int>("Version")
                        .Annotation("Relational:ColumnDefaultValue", "1")
                        .Annotation("Relational:ColumnDefaultValueType", "System.Int32");
                    
                    b.Property<bool>("Visible")
                        .Annotation("Relational:ColumnDefaultValue", "True")
                        .Annotation("Relational:ColumnDefaultValueType", "System.Boolean");
                    
                    b.Key("Id");
                    
                    b.Index("Name")
                        .Unique();
                    
                    b.Index("ParentId", "Version");
                    
                    b.Annotation("Relational:TableName", "ObjectModel");
                });
            
            builder.Entity("appPDU.Models.AdjacencyModel", b =>
                {
                    b.Reference("appPDU.Models.ObjectModel")
                        .InverseCollection()
                        .ForeignKey("PredecessorId");
                    
                    b.Reference("appPDU.Models.ObjectModel")
                        .InverseCollection()
                        .ForeignKey("SuccessorId");
                });
        }
    }
}
