using appPDU.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration;

namespace appPDU.DataLayer
{
    public class ObjectModelDbContext : DbContext
    {
        public DbSet<IObjectModel> ObjectModels { get; set; }

        public ObjectModelDbContext():base(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"))
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer();
        //}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Configurations.Add
            //var relational = modelBuilder.Entity<IObjectModel>().
            //    ForRelational(builder => builder.Table("ObjectModel"));
            //relational.Property(o => o.Id).GenerateValueOnAdd(true).Required(true);
            //relational.Property(o => o.DateCreate).GenerateValueOnAdd(true).Required(true);
            //relational.Property(o=>o.Name).MaxLength(256).Annotation("Index",new IndexAnnotation())
        }
    }
}
