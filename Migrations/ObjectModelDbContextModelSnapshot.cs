using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using appPDU.DataLayer;

namespace appPDU.Migrations
{
    [ContextType(typeof(ObjectModelDbContext))]
    partial class ObjectModelDbContextModelSnapshot : ModelSnapshot
    {
        public override IModel Model
        {
            get
            {
                var builder = new BasicModelBuilder()
                    .Annotation("SqlServer:ValueGeneration", "Sequence");
                
                builder.Entity("appPDU.Models.ObjectModel", b =>
                    {
                        b.Property<int>("ChildTypeMask")
                            .Annotation("OriginalValueIndex", 0);
                        b.Property<string>("Data")
                            .Annotation("OriginalValueIndex", 1)
                            .Annotation("SqlServer:ColumnType", "varchar(max)");
                        b.Property<DateTime>("DateClose")
                            .Annotation("OriginalValueIndex", 2);
                        b.Property<DateTime>("DateCreate")
                            .Annotation("OriginalValueIndex", 3)
                            .Annotation("SqlServer:ColumnDefaultExpression", "GetDate()")
                            .Annotation("SqlServer:ColumnType", "datetime2");
                        b.Property<Guid>("Id")
                            .GenerateValueOnAdd()
                            .Annotation("OriginalValueIndex", 4);
                        b.Property<string>("Metadata")
                            .Annotation("OriginalValueIndex", 5)
                            .Annotation("SqlServer:ColumnType", "varchar(2048)");
                        b.Property<string>("Name")
                            .Annotation("MaxLength", 256)
                            .Annotation("OriginalValueIndex", 6);
                        b.Property<int>("Order")
                            .Annotation("OriginalValueIndex", 7);
                        b.Property<Guid?>("ParentId")
                            .Annotation("OriginalValueIndex", 8);
                        b.Property<string>("Title")
                            .Annotation("MaxLength", 1024)
                            .Annotation("OriginalValueIndex", 9);
                        b.Property<int>("Type")
                            .Annotation("OriginalValueIndex", 10);
                        b.Property<string>("TypeName")
                            .Annotation("MaxLength", 54)
                            .Annotation("OriginalValueIndex", 11);
                        b.Property<int>("Version")
                            .Annotation("OriginalValueIndex", 12)
                            .Annotation("SqlServer:ColumnComputedExpression", "GetCurrentVersion(ParentId)");
                        b.Property<bool>("Visible")
                            .Annotation("OriginalValueIndex", 13);
                        b.Key("Id");
                        b.Annotation("SqlServer:TableName", "ObjectModel");
                    });
                
                return builder.Model;
            }
        }
    }
}
