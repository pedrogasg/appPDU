using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace appPDU.Migrations
{
    public partial class Initial : Migration
    {

        public override void Up(MigrationBuilder migration)
        {
            migration.Sql(@"CREATE FUNCTION GetCurrentVersion(@parentId UNIQUEIDENTIFIER)
                            RETURNS INT
                            AS
                            BEGIN

							IF(@parentId IS NULL)
							BEGIN
								RETURN 0;
							END

							Declare @version INT;
							SELECT @version = Count(*)
                            FROM ObjectModel
                            WHERE ParentId = @parentId;
                            RETURN @version
                            END");

            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "ObjectModel",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column(type: "uniqueidentifier", nullable: true),
                    Name = table.Column(type: "nvarchar(254)", nullable: false),
                    Title = table.Column(type: "nvarchar(1024)", nullable: true),
                    TypeName = table.Column(type: "nvarchar(54)", nullable: false),
                    Type = table.Column(type: "int", nullable: false),
                    ChildTypeMask = table.Column(type: "int", nullable: false),
                    Metadata = table.Column(type: "nvarchar(2048)", nullable: true),
                    Data = table.Column(type: "nvarchar(max)", nullable: true),
                    DateCreate = table.Column(type: "datetime2", nullable: false, defaultExpression: "GetDate()"),
                    DateClose = table.Column(type: "datetime2", nullable: true),
                    Order = table.Column(type: "int", nullable: false),
                    Version = table.Column(type: "int", nullable: false)
                        .Annotation("SqlServer:ColumnComputedExpression", "GetCurrentVersion(ParentId)"),
                    Visible = table.Column(type: "bit", nullable: false, defaultValue: true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectModel", x => x.Id);
                });
            migration.CreateIndex(
                name: "IX_ObjectModel_Name",
                table: "ObjectModel",
                column: "Name",
                unique: true);
            migration.CreateIndex(
                name: "IX_ObjectModel_ParentId_Version",
                table: "ObjectModel",
                columns: new[] { "ParentId", "Version" });
        }
        
        public override void Down(MigrationBuilder migration)
        {
            migration.DropSequence("DefaultSequence");
            migration.DropTable("ObjectModel");
        }
    }
}
