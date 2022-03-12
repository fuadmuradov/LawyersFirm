using Microsoft.EntityFrameworkCore.Migrations;

namespace LawyersFirm.Migrations
{
    public partial class AttorneysandAllDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attorneys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Jobname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    Education = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    SummarySentence = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attorneys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttorneyAwards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttorneyAwards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttorneyAwards_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttorneyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Facebook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Twitter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Linkedin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttorneyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttorneyContacts_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttorneyPractices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    AttorneyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttorneyPractices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttorneyPractices_Attorneys_AttorneyId",
                        column: x => x.AttorneyId,
                        principalTable: "Attorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttorneyAwards_AttorneyId",
                table: "AttorneyAwards",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttorneyContacts_AttorneyId",
                table: "AttorneyContacts",
                column: "AttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_AttorneyPractices_AttorneyId",
                table: "AttorneyPractices",
                column: "AttorneyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttorneyAwards");

            migrationBuilder.DropTable(
                name: "AttorneyContacts");

            migrationBuilder.DropTable(
                name: "AttorneyPractices");

            migrationBuilder.DropTable(
                name: "Attorneys");
        }
    }
}
