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
                });
            migration.CreateTable(
                name: "ObjectModel_Relations",
                columns: table => new
                {
                    ParentId = table.Column(type: "uniqueidentifier", nullable: false),
                    ChildId = table.Column(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Parent_ObjectModel_Id",
                        columns: x => x.ParentId,
                        referencedTable: "ObjectModel",
                        referencedColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Child_ObjectModel_Id",
                        columns: x => x.ChildId,
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
            migration.DropTable("ObjectModel");
        }
    }
}
