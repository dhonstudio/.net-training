using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace traningday2.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedAtField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "ripas",
                table: "Enrollments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                schema: "ripas",
                table: "Courses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "ripas",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                schema: "ripas",
                table: "Courses");
        }
    }
}
