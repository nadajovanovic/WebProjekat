using Microsoft.EntityFrameworkCore.Migrations;

namespace Projekat.Migrations
{
    public partial class V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lige",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lige", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Pozicije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pozicije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Timovi",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trener = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Predsednik = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LigaTimaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timovi", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Timovi_Lige_LigaTimaID",
                        column: x => x.LigaTimaID,
                        principalTable: "Lige",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utakmice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LigaID = table.Column<int>(type: "int", nullable: true),
                    DatumVreme = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KonacanRezultat = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utakmice_Lige_LigaID",
                        column: x => x.LigaID,
                        principalTable: "Lige",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Godina = table.Column<int>(type: "int", nullable: false),
                    PozicijaIgracaID = table.Column<int>(type: "int", nullable: true),
                    TimIgracaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igraci_Pozicije_PozicijaIgracaID",
                        column: x => x.PozicijaIgracaID,
                        principalTable: "Pozicije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Igraci_Timovi_TimIgracaID",
                        column: x => x.TimIgracaID,
                        principalTable: "Timovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Statistike",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimID = table.Column<int>(type: "int", nullable: true),
                    UtakmicaID = table.Column<int>(type: "int", nullable: true),
                    Golovi = table.Column<int>(type: "int", nullable: false),
                    UkupnoSuteva = table.Column<int>(type: "int", nullable: false),
                    SuteviUOkrvi = table.Column<int>(type: "int", nullable: false),
                    SuteviVanOkvrira = table.Column<int>(type: "int", nullable: false),
                    SlobodniUdarci = table.Column<int>(type: "int", nullable: false),
                    Korneri = table.Column<int>(type: "int", nullable: false),
                    Ofsajdi = table.Column<int>(type: "int", nullable: false),
                    OdbraneGolmana = table.Column<int>(type: "int", nullable: false),
                    Prekrsaji = table.Column<int>(type: "int", nullable: false),
                    ZutiKartoni = table.Column<int>(type: "int", nullable: false),
                    CrveniKartoni = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistike", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Statistike_Timovi_TimID",
                        column: x => x.TimID,
                        principalTable: "Timovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Statistike_Utakmice_UtakmicaID",
                        column: x => x.UtakmicaID,
                        principalTable: "Utakmice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TimUtakmica",
                columns: table => new
                {
                    TimoviID = table.Column<int>(type: "int", nullable: false),
                    UtakmiceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimUtakmica", x => new { x.TimoviID, x.UtakmiceID });
                    table.ForeignKey(
                        name: "FK_TimUtakmica_Timovi_TimoviID",
                        column: x => x.TimoviID,
                        principalTable: "Timovi",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimUtakmica_Utakmice_UtakmiceID",
                        column: x => x.UtakmiceID,
                        principalTable: "Utakmice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_PozicijaIgracaID",
                table: "Igraci",
                column: "PozicijaIgracaID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_TimIgracaID",
                table: "Igraci",
                column: "TimIgracaID");

            migrationBuilder.CreateIndex(
                name: "IX_Statistike_TimID",
                table: "Statistike",
                column: "TimID");

            migrationBuilder.CreateIndex(
                name: "IX_Statistike_UtakmicaID",
                table: "Statistike",
                column: "UtakmicaID");

            migrationBuilder.CreateIndex(
                name: "IX_Timovi_LigaTimaID",
                table: "Timovi",
                column: "LigaTimaID");

            migrationBuilder.CreateIndex(
                name: "IX_TimUtakmica_UtakmiceID",
                table: "TimUtakmica",
                column: "UtakmiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_LigaID",
                table: "Utakmice",
                column: "LigaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Statistike");

            migrationBuilder.DropTable(
                name: "TimUtakmica");

            migrationBuilder.DropTable(
                name: "Pozicije");

            migrationBuilder.DropTable(
                name: "Timovi");

            migrationBuilder.DropTable(
                name: "Utakmice");

            migrationBuilder.DropTable(
                name: "Lige");
        }
    }
}
