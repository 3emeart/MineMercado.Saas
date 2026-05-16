using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniMercadoSaas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class estoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Produtos_ProdutoId",
                table: "ItemVenda");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVenda_Vendas_VendaId",
                table: "ItemVenda");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemVenda",
                table: "ItemVenda");

            migrationBuilder.RenameTable(
                name: "ItemVenda",
                newName: "ItemVendas");

            migrationBuilder.RenameColumn(
                name: "SubTotal",
                table: "ItemVendas",
                newName: "Subtotal");

            migrationBuilder.RenameIndex(
                name: "IX_ItemVenda_VendaId",
                table: "ItemVendas",
                newName: "IX_ItemVendas_VendaId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemVenda_ProdutoId",
                table: "ItemVendas",
                newName: "IX_ItemVendas_ProdutoId");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Vendas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "EstoqueMinimo",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemVendas",
                table: "ItemVendas",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MovimentacoesEstoque",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    VendaId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UsuarioId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Observacao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CriadoEm = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacoesEstoque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MovimentacoesEstoque_Vendas_VendaId",
                        column: x => x.VendaId,
                        principalTable: "Vendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_ProdutoId",
                table: "MovimentacoesEstoque",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacoesEstoque_VendaId",
                table: "MovimentacoesEstoque",
                column: "VendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVendas_Produtos_ProdutoId",
                table: "ItemVendas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVendas_Vendas_VendaId",
                table: "ItemVendas",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemVendas_Produtos_ProdutoId",
                table: "ItemVendas");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemVendas_Vendas_VendaId",
                table: "ItemVendas");

            migrationBuilder.DropTable(
                name: "MovimentacoesEstoque");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemVendas",
                table: "ItemVendas");

            migrationBuilder.DropColumn(
                name: "EstoqueMinimo",
                table: "Produtos");

            migrationBuilder.RenameTable(
                name: "ItemVendas",
                newName: "ItemVenda");

            migrationBuilder.RenameColumn(
                name: "Subtotal",
                table: "ItemVenda",
                newName: "SubTotal");

            migrationBuilder.RenameIndex(
                name: "IX_ItemVendas_VendaId",
                table: "ItemVenda",
                newName: "IX_ItemVenda_VendaId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemVendas_ProdutoId",
                table: "ItemVenda",
                newName: "IX_ItemVenda_ProdutoId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Vendas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "FormaPagamento",
                table: "Vendas",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemVenda",
                table: "ItemVenda",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Produtos_ProdutoId",
                table: "ItemVenda",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVenda_Vendas_VendaId",
                table: "ItemVenda",
                column: "VendaId",
                principalTable: "Vendas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
