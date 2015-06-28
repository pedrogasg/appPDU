using appPDU.Models;
using Microsoft.Data.Entity;

namespace appPDU.DataLayer
{
    public class ObjectModelDbContext : DbContext
    {
        public DbSet<ObjectModel> ObjectModels { get; set; }

        public ObjectModelDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
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
                .ForSqlServer()
                .Table("ObjectModel");
            objectModelBuilder
                .Property(o => o.Id)
                .GenerateValueOnAdd()
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
                .ForSqlServer()
                .DefaultExpression("GetDate()")
                .ColumnType("datetime2");

            objectModelBuilder
                .Property(o => o.Version)
                .Required().ForSqlServer().ComputedExpression("GetCurrentVersion(ParentId)");

            objectModelBuilder
                .Property(o => o.Visible)
                .Required()
                .UseStoreDefault();

            objectModelBuilder
                .Property(o => o.Metadata)
                .ForSqlServer()
                .ColumnType("varchar(2048)");

            objectModelBuilder
                .Property(o => o.Data)
                .ForSqlServer()
                .ColumnType("varchar(max)");

        }
    }
}
