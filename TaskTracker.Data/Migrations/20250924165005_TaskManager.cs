using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaskTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class TaskManager : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tsk",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParentTaskId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tsk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tsk_Tsk_ParentTaskId",
                        column: x => x.ParentTaskId,
                        principalTable: "Tsk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TskRelationship",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SourceTaskId = table.Column<int>(type: "integer", nullable: false),
                    TargetTaskId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TskRelationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TskRelationship_Tsk_SourceTaskId",
                        column: x => x.SourceTaskId,
                        principalTable: "Tsk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TskRelationship_Tsk_TargetTaskId",
                        column: x => x.TargetTaskId,
                        principalTable: "Tsk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tsk_ParentTaskId",
                table: "Tsk",
                column: "ParentTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TskRelationship_SourceTaskId",
                table: "TskRelationship",
                column: "SourceTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TskRelationship_TargetTaskId",
                table: "TskRelationship",
                column: "TargetTaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TskRelationship");

            migrationBuilder.DropTable(
                name: "Tsk");
        }
    }
}
