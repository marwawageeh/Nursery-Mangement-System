using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc_project.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manger = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "course",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    Min_Degree = table.Column<int>(type: "int", nullable: false),
                    dept_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_course", x => x.ID);
                    table.ForeignKey(
                        name: "FK_course_department_dept_id",
                        column: x => x.dept_id,
                        principalTable: "department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "trainee",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dept_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_trainee_department_dept_id",
                        column: x => x.dept_id,
                        principalTable: "department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "instructor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    crs_id = table.Column<int>(type: "int", nullable: false),
                    dept_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_instructor_course_crs_id",
                        column: x => x.crs_id,
                        principalTable: "course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_instructor_department_dept_id",
                        column: x => x.dept_id,
                        principalTable: "department",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "crsResult",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Degree = table.Column<int>(type: "int", nullable: false),
                    trainee_id = table.Column<int>(type: "int", nullable: false),
                    crs_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_crsResult", x => x.ID);
                    table.ForeignKey(
                        name: "FK_crsResult_course_crs_id",
                        column: x => x.crs_id,
                        principalTable: "course",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_crsResult_trainee_trainee_id",
                        column: x => x.trainee_id,
                        principalTable: "trainee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_course_dept_id",
                table: "course",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "IX_crsResult_crs_id",
                table: "crsResult",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_crsResult_trainee_id",
                table: "crsResult",
                column: "trainee_id");

            migrationBuilder.CreateIndex(
                name: "IX_instructor_crs_id",
                table: "instructor",
                column: "crs_id");

            migrationBuilder.CreateIndex(
                name: "IX_instructor_dept_id",
                table: "instructor",
                column: "dept_id");

            migrationBuilder.CreateIndex(
                name: "IX_trainee_dept_id",
                table: "trainee",
                column: "dept_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "crsResult");

            migrationBuilder.DropTable(
                name: "instructor");

            migrationBuilder.DropTable(
                name: "trainee");

            migrationBuilder.DropTable(
                name: "course");

            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
