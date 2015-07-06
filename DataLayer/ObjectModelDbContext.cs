using appPDU.Models;
using JetBrains.Annotations;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;

namespace appPDU.DataLayer
{
    public class ObjectModelDbContext : DbContext
    {
        public DbSet<ObjectModel> ObjectModels { get; set; }

        public ObjectModelDbContext() : base()
        {

        }
        protected override void OnConfiguring(EntityOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            var objectModelBuilder = modelBuilder.Entity<ObjectModel>();

            objectModelBuilder
                .Key(o => o.Id);

            objectModelBuilder
                .Index(o => o.Name)
                .Unique();

            objectModelBuilder
                .Index(o => new { o.ParentId, o.Version });

            objectModelBuilder
                .Table("ObjectModel");

            objectModelBuilder
                .Property(o => o.Id)
                .DefaultExpression("NEWSEQUENTIALID()")
                .Required();

            objectModelBuilder
                .Property(o => o.ParentId)
                .Required(false);

            objectModelBuilder
                .Property(o => o.Name)
                .MaxLength(256)
                .Required();

            objectModelBuilder
                .Property(o => o.Title)
                .MaxLength(1024);

            objectModelBuilder
                .Property(o => o.Type)
                .Required();

            objectModelBuilder
                .Property(o => o.TypeName)
                .MaxLength(54)
                .Required();

            objectModelBuilder
                .Property(o => o.DateCreate)
                .Required()
                .DefaultExpression("GETDATE()")
                .ColumnType("datetime2");

            objectModelBuilder
                .Property(o => o.Version)
                .Required()
                .DefaultValue(1);

            objectModelBuilder
                .Property(o => o.Visible)
                .Required()
                .DefaultValue(true);

            objectModelBuilder
                .Property(o => o.Metadata)
                .ColumnType("varchar(2048)");

            objectModelBuilder
                .Property(o => o.Data)
                .ColumnType("varchar(max)");

            var adjacencyModelBuilder = modelBuilder.Entity<AdjacencyModel>();
            adjacencyModelBuilder
                .Key(c => new { c.PredecessorId, c.SuccessorId });

            adjacencyModelBuilder
                .Reference(o => o.Predecessor)
                .InverseCollection(o => o.Predecessors)
                .ForeignKey(o => o.PredecessorId);

            adjacencyModelBuilder
                .Reference(o => o.Successor)
                .InverseCollection(o => o.Successors)
                .ForeignKey(o => o.SuccessorId);



        }
    }
}
 