using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_TodoItem_Prps_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "LastModifiedDate",
                table: "TodoItems",
                newName: "CreationTime");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "TodoItems",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "TodoItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "TodoItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TodoItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "TodoItems",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TodoItems");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "TodoItems");

            migrationBuilder.RenameColumn(
                name: "CreationTime",
                table: "TodoItems",
                newName: "LastModifiedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "TodoItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
