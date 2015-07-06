using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations;
using Microsoft.Data.Entity.Relational.Migrations.Builders;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace appPDU.Migrations
{
    public partial class initial : Migration
    {
        public override void Up(MigrationBuilder migration)
        {
            migration.CreateSequence(
                name: "DefaultSequence",
                type: "bigint",
                startWith: 1L,
                incrementBy: 10);
            migration.CreateTable(
                name: "ObjectModel",
                columns: table => new
                {
                    Id = table.Column(type: "uniqueidentifier", nullable: false, defaultExpression: "NEWSEQUENTIALID()"),
                    ParentId = table.Column(type: "uniqueidentifier", nullable: true),
                    Name = table.Column(type: "nvarchar(254)", nullable: false),
                    Title = table.Column(type: "nvarchar(1024)", nullable: true),
                    TypeName = table.Column(type: "nvarchar(54)", nullable: false),
                    Type = table.Column(type: "int", nullable: false),
                    ChildTypeMask = table.Column(type: "int", nullable: false),
                    Metadata = table.Column(type: "nvarchar(2048)", nullable: true),
                    Data = table.Column(type: "nvarchar(max)", nullable: true),
                    DateCreate = table.Column(type: "datetime2", nullable: false, defaultExpression: "GETDATE()"),
                    DateClose = table.Column(type: "datetime2", nullable: true),
                    Order = table.Column(type: "int", nullable: false),
                    Version = table.Column(type: "int", nullable: false, defaultValue: 1),
                    Visible = table.Column(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectModel", x => x.Id);
                });
            migration.CreateTable(
                name: "AdjacencyModel",
                columns: table => new
                {
                    PredecessorId = table.Column(type: "uniqueidentifier", nullable: false),
                    SuccessorId = table.Column(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdjacencyModel", x => new { x.PredecessorId, x.SuccessorId });
                    table.ForeignKey(
                        name: "FK_AdjacencyModel_ObjectModel_PredecessorId",
                        columns: x => x.PredecessorId,
                        referencedTable: "ObjectModel",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdjacencyModel_ObjectModel_SuccessorId",
                        columns: x => x.SuccessorId,
                        referencedTable: "ObjectModel",
                        referencedColumn: "Id");
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
            migration.DropTable("AdjacencyModel");
            migration.DropTable("ObjectModel");
        }
    }
}
