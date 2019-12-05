using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Votacion.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mesas",
                columns: table => new
                {
                    MesaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numero = table.Column<long>(nullable: false),
                    CodigoQR = table.Column<string>(nullable: true),
                    estadoMesa = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesas", x => x.MesaID);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    PartidoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sigla = table.Column<string>(nullable: false),
                    Nombre = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.PartidoID);
                });

            migrationBuilder.CreateTable(
                name: "TipoVoto",
                columns: table => new
                {
                    TipoVotoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoVoto", x => x.TipoVotoID);
                });

            migrationBuilder.CreateTable(
                name: "Jurados",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CI = table.Column<string>(nullable: false),
                    Nombres = table.Column<string>(nullable: false),
                    Apellidos = table.Column<string>(nullable: false),
                    Usuario = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    MesaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jurados", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Jurados_Mesas_MesaID",
                        column: x => x.MesaID,
                        principalTable: "Mesas",
                        principalColumn: "MesaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CI = table.Column<string>(nullable: false),
                    Nombres = table.Column<string>(nullable: false),
                    Apellidos = table.Column<string>(nullable: false),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    Edad = table.Column<int>(nullable: false),
                    MesaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Mesas_MesaID",
                        column: x => x.MesaID,
                        principalTable: "Mesas",
                        principalColumn: "MesaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    CandidatoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(nullable: false),
                    Apellidos = table.Column<string>(nullable: false),
                    TipoVotoID = table.Column<int>(nullable: false),
                    PartidoID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.CandidatoID);
                    table.ForeignKey(
                        name: "FK_Candidatos_Partidos_PartidoID",
                        column: x => x.PartidoID,
                        principalTable: "Partidos",
                        principalColumn: "PartidoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidatos_TipoVoto_TipoVotoID",
                        column: x => x.TipoVotoID,
                        principalTable: "TipoVoto",
                        principalColumn: "TipoVotoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votos",
                columns: table => new
                {
                    VotoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidatoID = table.Column<int>(nullable: false),
                    UsuarioID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votos", x => x.VotoID);
                    table.ForeignKey(
                        name: "FK_Votos_Candidatos_CandidatoID",
                        column: x => x.CandidatoID,
                        principalTable: "Candidatos",
                        principalColumn: "CandidatoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_PartidoID",
                table: "Candidatos",
                column: "PartidoID");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_TipoVotoID",
                table: "Candidatos",
                column: "TipoVotoID");

            migrationBuilder.CreateIndex(
                name: "IX_Jurados_MesaID",
                table: "Jurados",
                column: "MesaID");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_MesaID",
                table: "Usuarios",
                column: "MesaID");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_CandidatoID",
                table: "Votos",
                column: "CandidatoID");

            migrationBuilder.CreateIndex(
                name: "IX_Votos_UsuarioID",
                table: "Votos",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jurados");

            migrationBuilder.DropTable(
                name: "Votos");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "TipoVoto");

            migrationBuilder.DropTable(
                name: "Mesas");
        }
    }
}
