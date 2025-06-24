using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace trabalho_api.Migrations
{
    /// <inheritdoc />
    public partial class APICpmpleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apelido",
                table: "Cursos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Duracao",
                table: "Cursos",
                type: "varchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "InstituicaoId",
                table: "Cursos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Instituicoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false),
                    Endereco = table.Column<string>(type: "varchar(100)", maxLength: 200, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Cep = table.Column<string>(type: "varchar(100)", maxLength: 10, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instituicoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Termos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InstituicaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Funcionarios_Instituicoes_InstituicaoId",
                        column: x => x.InstituicaoId,
                        principalTable: "Instituicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    TermoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplinas_Termos_TermoId",
                        column: x => x.TermoId,
                        principalTable: "Termos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_InstituicaoId",
                table: "Cursos",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_Nome",
                table: "Cursos",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplinas_TermoId",
                table: "Disciplinas",
                column: "TermoId");

            migrationBuilder.CreateIndex(
                name: "IX_Funcionarios_InstituicaoId",
                table: "Funcionarios",
                column: "InstituicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Termos_CursoId",
                table: "Termos",
                column: "CursoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cursos_Instituicoes_InstituicaoId",
                table: "Cursos",
                column: "InstituicaoId",
                principalTable: "Instituicoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cursos_Instituicoes_InstituicaoId",
                table: "Cursos");

            migrationBuilder.DropTable(
                name: "Disciplinas");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Termos");

            migrationBuilder.DropTable(
                name: "Instituicoes");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_InstituicaoId",
                table: "Cursos");

            migrationBuilder.DropIndex(
                name: "IX_Cursos_Nome",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Apelido",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Cursos");

            migrationBuilder.DropColumn(
                name: "InstituicaoId",
                table: "Cursos");
        }
    }
}
