using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AdessoWorldLeague.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrawRecords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ruler_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ruler_Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrawResultGroup",
                columns: table => new
                {
                    DrawResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrawResultGroup", x => new { x.DrawResultId, x.GroupsId });
                    table.ForeignKey(
                        name: "FK_DrawResultGroup_DrawRecords_DrawResultId",
                        column: x => x.DrawResultId,
                        principalTable: "DrawRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrawResultGroup_Group_GroupsId",
                        column: x => x.GroupsId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeam",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TeamsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeam", x => new { x.GroupId, x.TeamsId });
                    table.ForeignKey(
                        name: "FK_GroupTeam_Group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeam_Teams_TeamsId",
                        column: x => x.TeamsId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Country",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("02fd27d4-3d1e-42e3-b68d-7523909edd6a"), "Turkey" },
                    { new Guid("0794140c-e524-404a-b3f2-528c3e679a96"), "Italy" },
                    { new Guid("156d9e12-9dbd-4990-a57e-c7f145b8e0eb"), "Belgium" },
                    { new Guid("533060a2-f88f-46ed-95a7-49b99441f6b5"), "Portugal" },
                    { new Guid("63a54ea0-4da1-429f-87b9-31ff6adaf662"), "Spain" },
                    { new Guid("ae1ba129-93d7-4bb3-916d-ddc0db6e0b67"), "Netherlands" },
                    { new Guid("d516f99d-2f8a-4ecf-bec8-79a407744bf0"), "France" },
                    { new Guid("f655f39f-c844-4973-bb7a-e675641f6981"), "Germany" }
                });

            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { new Guid("1f5500c4-5ff3-4269-96fb-1874debcb55c"), new Guid("533060a2-f88f-46ed-95a7-49b99441f6b5"), "Adesso Coimbra" },
                    { new Guid("2602d1bf-f26b-41e6-b2c9-838de6d058c6"), new Guid("533060a2-f88f-46ed-95a7-49b99441f6b5"), "Adesso Porto" },
                    { new Guid("362b0fce-709e-4a1c-9f45-e4a52c39a15b"), new Guid("f655f39f-c844-4973-bb7a-e675641f6981"), "Adesso Berlin" },
                    { new Guid("3ab10003-9b18-4124-8d9e-897cdfc9e9b8"), new Guid("0794140c-e524-404a-b3f2-528c3e679a96"), "Adesso Napoli" },
                    { new Guid("3b202592-399e-4dfa-8916-f56d50c72a12"), new Guid("ae1ba129-93d7-4bb3-916d-ddc0db6e0b67"), "Adesso Amsterdam" },
                    { new Guid("404af8b7-b2c8-45a6-8d9d-1c71ac819de2"), new Guid("156d9e12-9dbd-4990-a57e-c7f145b8e0eb"), "Adesso Anvers" },
                    { new Guid("4d70e9a1-dfb5-4b68-a9d9-0c58b59df50f"), new Guid("63a54ea0-4da1-429f-87b9-31ff6adaf662"), "Adesso Barcelona" },
                    { new Guid("5c59b52f-033d-46db-82be-0e994a54111b"), new Guid("533060a2-f88f-46ed-95a7-49b99441f6b5"), "Adesso Lisbon" },
                    { new Guid("62f8551e-ac4a-4908-be70-3afc7824541c"), new Guid("02fd27d4-3d1e-42e3-b68d-7523909edd6a"), "Adesso Antalya" },
                    { new Guid("64fb783e-bdcc-4aca-b897-02ad68bd1dde"), new Guid("0794140c-e524-404a-b3f2-528c3e679a96"), "Adesso Rome" },
                    { new Guid("6e461e04-5827-47c5-96f8-ddad6db8b1f0"), new Guid("533060a2-f88f-46ed-95a7-49b99441f6b5"), "Adesso Braga" },
                    { new Guid("6fd8ef6a-4fb2-4b45-a7fd-b603ef57880e"), new Guid("f655f39f-c844-4973-bb7a-e675641f6981"), "Adesso Dortmund" },
                    { new Guid("81c6667b-7a8f-4a22-a634-1d3ed0f6bf94"), new Guid("02fd27d4-3d1e-42e3-b68d-7523909edd6a"), "Adesso Istanbul" },
                    { new Guid("8314826f-cc9f-4681-bf43-32fca8a9531b"), new Guid("63a54ea0-4da1-429f-87b9-31ff6adaf662"), "Adesso Sevilla" },
                    { new Guid("87507cba-8764-4cec-836e-ab662d8a00e4"), new Guid("d516f99d-2f8a-4ecf-bec8-79a407744bf0"), "Adesso Marseille" },
                    { new Guid("8fd80894-d022-4c6b-9c7d-c53a3909b77f"), new Guid("156d9e12-9dbd-4990-a57e-c7f145b8e0eb"), "Adesso Brussels" },
                    { new Guid("915e8c30-8997-4f93-b87b-76fa6a051b63"), new Guid("f655f39f-c844-4973-bb7a-e675641f6981"), "Adesso Munich" },
                    { new Guid("97fc80ad-49ea-4074-8814-79157def68fb"), new Guid("ae1ba129-93d7-4bb3-916d-ddc0db6e0b67"), "Adesso Lahey" },
                    { new Guid("a280a7bb-bb04-4ab3-bf88-6a26129a16bd"), new Guid("ae1ba129-93d7-4bb3-916d-ddc0db6e0b67"), "Adesso Eindhoven" },
                    { new Guid("b01f492b-73ac-4c7e-9941-54ac8bfb5b5d"), new Guid("156d9e12-9dbd-4990-a57e-c7f145b8e0eb"), "Adesso Ghent" },
                    { new Guid("b52498f3-29eb-414f-b31c-c8991a1bc88a"), new Guid("63a54ea0-4da1-429f-87b9-31ff6adaf662"), "Adesso Madrid" },
                    { new Guid("c7534f74-6ebb-4ba9-9e0b-79816fce7f61"), new Guid("02fd27d4-3d1e-42e3-b68d-7523909edd6a"), "Adesso Ankara" },
                    { new Guid("c8681e78-17a9-4150-bac7-4a21f0377ecb"), new Guid("d516f99d-2f8a-4ecf-bec8-79a407744bf0"), "Adesso Paris" },
                    { new Guid("d0455711-cf9e-4701-9d91-01d6e83c5af1"), new Guid("0794140c-e524-404a-b3f2-528c3e679a96"), "Adesso Milano" },
                    { new Guid("d65ea1d7-8718-490d-93da-bae1c642df2e"), new Guid("63a54ea0-4da1-429f-87b9-31ff6adaf662"), "Adesso Granada" },
                    { new Guid("d6c8cad7-a06a-46d7-83ad-bbd125e633f7"), new Guid("f655f39f-c844-4973-bb7a-e675641f6981"), "Adesso Frankfurth" },
                    { new Guid("d9bd68f9-b08f-4e23-accf-91486e7804f6"), new Guid("ae1ba129-93d7-4bb3-916d-ddc0db6e0b67"), "Adesso Rotterdam" },
                    { new Guid("de21d655-3265-4582-8e46-d358c5032534"), new Guid("156d9e12-9dbd-4990-a57e-c7f145b8e0eb"), "Adesso Brugge" },
                    { new Guid("df160af5-2dd4-43e1-973e-94220d6b26af"), new Guid("0794140c-e524-404a-b3f2-528c3e679a96"), "Adesso Venice" },
                    { new Guid("dfd29cb7-1665-47e4-bdb6-ee6e6747b360"), new Guid("02fd27d4-3d1e-42e3-b68d-7523909edd6a"), "Adesso İzmir" },
                    { new Guid("e41998d9-a41c-48bc-90b4-0204a10628a9"), new Guid("d516f99d-2f8a-4ecf-bec8-79a407744bf0"), "Adesso Lyon" },
                    { new Guid("ed652bdb-bb5b-430e-9ba7-927d268c1f85"), new Guid("d516f99d-2f8a-4ecf-bec8-79a407744bf0"), "Adesso Nice" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DrawResultGroup_GroupsId",
                table: "DrawResultGroup",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeam_TeamsId",
                table: "GroupTeam",
                column: "TeamsId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CountryId",
                table: "Teams",
                column: "CountryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrawResultGroup");

            migrationBuilder.DropTable(
                name: "GroupTeam");

            migrationBuilder.DropTable(
                name: "DrawRecords");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
