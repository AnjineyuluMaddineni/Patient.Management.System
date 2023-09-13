using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PMS.WEB.DAL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    AId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AllergyId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Isoforms = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AllergyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllergenSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Allerginicity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => x.AId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new
                {
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.DiagnosisId);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    MId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("3c1692a5-950e-483f-b9de-73b7d04d0c35")),
                    ApplNo = table.Column<int>(type: "int", nullable: false),
                    ProductNo = table.Column<int>(type: "int", nullable: false),
                    Form = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceDrug = table.Column<int>(type: "int", nullable: false),
                    DrugName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActiveIngredient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferenceStandard = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.MId);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeprecated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.ProcedureId);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timing = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.TimeSlotId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HospitalUser",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalUser", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_HospitalUser_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_notifications_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Race = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ethinicity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguagesKnown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyInfoFirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyInfoLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyInfoRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyInfoEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyCountrycode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyHomeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyPatientPortal = table.Column<bool>(type: "bit", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PId);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyMessages",
                columns: table => new
                {
                    ReplyMessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromAppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAppUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyMessages", x => x.ReplyMessageId);
                    table.ForeignKey(
                        name: "FK_ReplyMessages_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "MessageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                columns: table => new
                {
                    AllergyInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsFatal = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClinicalInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.AllergyInfoId);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Allergies_AId",
                        column: x => x.AId,
                        principalTable: "Allergies",
                        principalColumn: "AId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_Patients_PId",
                        column: x => x.PId,
                        principalTable: "Patients",
                        principalColumn: "PId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedulings",
                columns: table => new
                {
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TimeSlotId = table.Column<int>(type: "int", nullable: false),
                    MeetingTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedulings", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedulings_HospitalUser_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "HospitalUser",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulings_Patients_PId",
                        column: x => x.PId,
                        principalTable: "Patients",
                        principalColumn: "PId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedulings_TimeSlot_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TimeSlot",
                        principalColumn: "TimeSlotId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientVisits",
                columns: table => new
                {
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    BloodPressure = table.Column<int>(type: "int", nullable: false),
                    BodyTemperature = table.Column<int>(type: "int", nullable: false),
                    RespirationRate = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientVisits", x => x.VisitId);
                    table.ForeignKey(
                        name: "FK_PatientVisits_Schedulings_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedulings",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SchedulingHistory",
                columns: table => new
                {
                    ScheduleHistoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReasonForEdit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulingHistory", x => x.ScheduleHistoryId);
                    table.ForeignKey(
                        name: "FK_SchedulingHistory_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SchedulingHistory_Schedulings_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedulings",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDiagnoses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DiagnosisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDiagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDiagnoses_Diagnoses_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "Diagnoses",
                        principalColumn: "DiagnosisId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientDiagnoses_PatientVisits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "PatientVisits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientMedications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientMedications_Medications_MId",
                        column: x => x.MId,
                        principalTable: "Medications",
                        principalColumn: "MId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientMedications_PatientVisits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "PatientVisits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientProcedures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientProcedures_PatientVisits_VisitId",
                        column: x => x.VisitId,
                        principalTable: "PatientVisits",
                        principalColumn: "VisitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientProcedures_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "ProcedureId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "CountryCode", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "Password", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RegistrationDate", "SecurityStamp", "Status", "Title", "Token", "TwoFactorEnabled", "UserName", "UserTypeId" },
                values: new object[,]
                {
                    { "c0d82ea7-c286-429a-9d30-37a0dcfb250c", 0, new DateTime(1985, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "89ef8d55-023c-41ec-93cc-cb79ff8fee8a", null, null, false, "David", "Jones", false, null, null, null, null, null, null, false, null, "e74d3943-c822-4dbd-919a-582b12ec7262", 0, null, null, false, "User123", 0 },
                    { "478d4720-53d3-48be-a383-990207507b73", 0, new DateTime(1987, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "3eacfc6e-4baa-4d4e-936e-80d07170cc93", null, null, false, "", "Doe", false, null, null, null, null, null, null, false, new DateTime(2018, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "b42120de-15a7-4b68-9732-bdbed0b87a6c", 0, null, null, false, "User1234", 0 }
                });

            migrationBuilder.InsertData(
                table: "HospitalUser",
                columns: new[] { "EmployeeId", "AppUserId" },
                values: new object[,]
                {
                    { new Guid("228ef8b1-e6db-4163-aeac-c3689a2b1dec"), null },
                    { new Guid("afa8d15c-623f-406b-8c1d-7911e6614cf7"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_AllergyId_Isoforms",
                table: "Allergies",
                columns: new[] { "AllergyId", "Isoforms" },
                unique: true,
                filter: "[AllergyId] IS NOT NULL AND [Isoforms] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_HospitalUser_AppUserId",
                table: "HospitalUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_notifications_AppUserId",
                table: "notifications",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_AId",
                table: "PatientAllergies",
                column: "AId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_PId",
                table: "PatientAllergies",
                column: "PId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnoses_DiagnosisId",
                table: "PatientDiagnoses",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDiagnoses_VisitId",
                table: "PatientDiagnoses",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_MId",
                table: "PatientMedications",
                column: "MId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_VisitId",
                table: "PatientMedications",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProcedures_ProcedureId",
                table: "PatientProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientProcedures_VisitId",
                table: "PatientProcedures",
                column: "VisitId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AppUserId",
                table: "Patients",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVisits_ScheduleId",
                table: "PatientVisits",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyMessages_MessageId",
                table: "ReplyMessages",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingHistory_AppUserId",
                table: "SchedulingHistory",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SchedulingHistory_ScheduleId",
                table: "SchedulingHistory",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_EmployeeId",
                table: "Schedulings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_PId",
                table: "Schedulings",
                column: "PId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedulings_TimeSlotId",
                table: "Schedulings",
                column: "TimeSlotId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "PatientAllergies");

            migrationBuilder.DropTable(
                name: "PatientDiagnoses");

            migrationBuilder.DropTable(
                name: "PatientMedications");

            migrationBuilder.DropTable(
                name: "PatientProcedures");

            migrationBuilder.DropTable(
                name: "ReplyMessages");

            migrationBuilder.DropTable(
                name: "SchedulingHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropTable(
                name: "PatientVisits");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Schedulings");

            migrationBuilder.DropTable(
                name: "HospitalUser");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
