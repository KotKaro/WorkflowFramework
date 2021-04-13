using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Process",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Process", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeMetadata",
                columns: table => new
                {
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeMetadata", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "Step",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Step", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Step_Process_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MemberDescriptor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValueAccessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    OwningType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeMetadataType = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberDescriptor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemberDescriptor_MemberDescriptor_ValueAccessorId",
                        column: x => x.ValueAccessorId,
                        principalTable: "MemberDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MemberDescriptor_TypeMetadata_TypeMetadataType",
                        column: x => x.TypeMetadataType,
                        principalTable: "TypeMetadata",
                        principalColumn: "Type",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessRun",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessRun", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessRun_Process_ProcessId",
                        column: x => x.ProcessId,
                        principalTable: "Process",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessRun_Step_CurrentStepId",
                        column: x => x.CurrentStepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StepNavigator",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StepNavigator", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StepNavigator_Step_StepId",
                        column: x => x.StepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StepNavigator_Step_TargetStepId",
                        column: x => x.TargetStepId,
                        principalTable: "Step",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Argument",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ValueString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberDescriptorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessRunId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Argument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Argument_MemberDescriptor_MemberDescriptorId",
                        column: x => x.MemberDescriptorId,
                        principalTable: "MemberDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Argument_ProcessRun_ProcessRunId",
                        column: x => x.ProcessRunId,
                        principalTable: "ProcessRun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expectation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AndExpectationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrExpectationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StepNavigatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DescribedTypeFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ValueType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BiggerThanExpectation_ValueAccessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EqualExpectation_ValueAccessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ValueAccessorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expectation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expectation_Expectation_AndExpectationId",
                        column: x => x.AndExpectationId,
                        principalTable: "Expectation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expectation_Expectation_OrExpectationId",
                        column: x => x.OrExpectationId,
                        principalTable: "Expectation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expectation_MemberDescriptor_BiggerThanExpectation_ValueAccessorId",
                        column: x => x.BiggerThanExpectation_ValueAccessorId,
                        principalTable: "MemberDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expectation_MemberDescriptor_EqualExpectation_ValueAccessorId",
                        column: x => x.EqualExpectation_ValueAccessorId,
                        principalTable: "MemberDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expectation_MemberDescriptor_ValueAccessorId",
                        column: x => x.ValueAccessorId,
                        principalTable: "MemberDescriptor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Expectation_StepNavigator_StepNavigatorId",
                        column: x => x.StepNavigatorId,
                        principalTable: "StepNavigator",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Argument_MemberDescriptorId",
                table: "Argument",
                column: "MemberDescriptorId");

            migrationBuilder.CreateIndex(
                name: "IX_Argument_ProcessRunId",
                table: "Argument",
                column: "ProcessRunId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_AndExpectationId",
                table: "Expectation",
                column: "AndExpectationId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_BiggerThanExpectation_ValueAccessorId",
                table: "Expectation",
                column: "BiggerThanExpectation_ValueAccessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_EqualExpectation_ValueAccessorId",
                table: "Expectation",
                column: "EqualExpectation_ValueAccessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_OrExpectationId",
                table: "Expectation",
                column: "OrExpectationId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_StepNavigatorId",
                table: "Expectation",
                column: "StepNavigatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expectation_ValueAccessorId",
                table: "Expectation",
                column: "ValueAccessorId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberDescriptor_TypeMetadataType",
                table: "MemberDescriptor",
                column: "TypeMetadataType");

            migrationBuilder.CreateIndex(
                name: "IX_MemberDescriptor_ValueAccessorId",
                table: "MemberDescriptor",
                column: "ValueAccessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessRun_CurrentStepId",
                table: "ProcessRun",
                column: "CurrentStepId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessRun_ProcessId",
                table: "ProcessRun",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_Step_ProcessId",
                table: "Step",
                column: "ProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_StepNavigator_StepId",
                table: "StepNavigator",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_StepNavigator_TargetStepId",
                table: "StepNavigator",
                column: "TargetStepId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Argument");

            migrationBuilder.DropTable(
                name: "Expectation");

            migrationBuilder.DropTable(
                name: "ProcessRun");

            migrationBuilder.DropTable(
                name: "MemberDescriptor");

            migrationBuilder.DropTable(
                name: "StepNavigator");

            migrationBuilder.DropTable(
                name: "TypeMetadata");

            migrationBuilder.DropTable(
                name: "Step");

            migrationBuilder.DropTable(
                name: "Process");
        }
    }
}
