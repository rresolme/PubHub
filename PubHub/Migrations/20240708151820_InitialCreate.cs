using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PubHub.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "PrintDistributors",
                columns: table => new
                {
                    DistributorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APIEndpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    APIKey = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintDistributors", x => x.DistributorID);
                });

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    PublicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.PublicationID);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressID);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistributorPublications",
                columns: table => new
                {
                    DistributorPublicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistributorID = table.Column<int>(type: "int", nullable: false),
                    PublicationID = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistributorPublications", x => x.DistributorPublicationID);
                    table.ForeignKey(
                        name: "FK_DistributorPublications_PrintDistributors_DistributorID",
                        column: x => x.DistributorID,
                        principalTable: "PrintDistributors",
                        principalColumn: "DistributorID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistributorPublications_Publications_PublicationID",
                        column: x => x.PublicationID,
                        principalTable: "Publications",
                        principalColumn: "PublicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    SubscriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PublicationID = table.Column<int>(type: "int", nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.SubscriptionID);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Publications_PublicationID",
                        column: x => x.PublicationID,
                        principalTable: "Publications",
                        principalColumn: "PublicationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerID",
                table: "Addresses",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorPublications_DistributorID",
                table: "DistributorPublications",
                column: "DistributorID");

            migrationBuilder.CreateIndex(
                name: "IX_DistributorPublications_PublicationID",
                table: "DistributorPublications",
                column: "PublicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_AddressID",
                table: "Subscriptions",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_CustomerID",
                table: "Subscriptions",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_PublicationID",
                table: "Subscriptions",
                column: "PublicationID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistributorPublications");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "PrintDistributors");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
