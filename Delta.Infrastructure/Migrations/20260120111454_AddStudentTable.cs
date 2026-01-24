using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOB = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Income = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApaarID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AadharNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PresentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerPIN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityID = table.Column<int>(type: "int", nullable: false),
                    DistID = table.Column<int>(type: "int", nullable: false),
                    ReligionID = table.Column<int>(type: "int", nullable: false),
                    QuotaID = table.Column<int>(type: "int", nullable: false),
                    PH = table.Column<int>(type: "int", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AuthAdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthLstEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddOnDt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "tblUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterID = table.Column<int>(type: "int", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    IsAdmin = table.Column<int>(type: "int", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LoginAttempts = table.Column<int>(type: "int", nullable: true),
                    IsLocked = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthAdd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthLstEdt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AuthDel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EditOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelOnDt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DelStatus = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUser", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "tblUser");
        }
    }
}
