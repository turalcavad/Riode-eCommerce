using Microsoft.EntityFrameworkCore.Migrations;

namespace Riode.Data.Migrations
{
    public partial class CreatedByDeletedByRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_CreatedById",
                table: "Subscribes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Subscribes_DeletedById",
                table: "Subscribes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_CreatedById",
                table: "Specifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Specifications_DeletedById",
                table: "Specifications",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_CreatedById",
                table: "Sizes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Sizes_DeletedById",
                table: "Sizes",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedById",
                table: "Products",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPricing_CreatedById",
                table: "ProductPricing",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPricing_DeletedById",
                table: "ProductPricing",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_CreatedById",
                table: "ProductImages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_DeletedById",
                table: "ProductImages",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_CreatedById",
                table: "PostTags",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_DeletedById",
                table: "PostTags",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_CreatedById",
                table: "Faqs",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Faqs_DeletedById",
                table: "Faqs",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContactComments_CreatedById",
                table: "ContactComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContactComments_DeletedById",
                table: "ContactComments",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_CreatedById",
                table: "Colors",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Colors_DeletedById",
                table: "Colors",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_CreatedById",
                table: "Brands",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Brands_DeletedById",
                table: "Brands",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_CreatedById",
                table: "BlogPosts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_DeletedById",
                table: "BlogPosts",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_CreatedById",
                table: "BlogPosts",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Users_DeletedById",
                table: "BlogPosts",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Users_CreatedById",
                table: "Brands",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Brands_Users_DeletedById",
                table: "Brands",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Users_DeletedById",
                table: "Categories",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Users_CreatedById",
                table: "Colors",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Colors_Users_DeletedById",
                table: "Colors",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactComments_Users_CreatedById",
                table: "ContactComments",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactComments_Users_DeletedById",
                table: "ContactComments",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_CreatedById",
                table: "Faqs",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faqs_Users_DeletedById",
                table: "Faqs",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Users_CreatedById",
                table: "PostTags",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Users_DeletedById",
                table: "PostTags",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_CreatedById",
                table: "ProductImages",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Users_DeletedById",
                table: "ProductImages",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPricing_Users_CreatedById",
                table: "ProductPricing",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPricing_Users_DeletedById",
                table: "ProductPricing",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_DeletedById",
                table: "Products",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Users_CreatedById",
                table: "Sizes",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sizes_Users_DeletedById",
                table: "Sizes",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Users_CreatedById",
                table: "Specifications",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Specifications_Users_DeletedById",
                table: "Specifications",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_CreatedById",
                table: "Subscribes",
                column: "CreatedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscribes_Users_DeletedById",
                table: "Subscribes",
                column: "DeletedById",
                principalSchema: "Membership",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_CreatedById",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Users_DeletedById",
                table: "BlogPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Users_CreatedById",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Brands_Users_DeletedById",
                table: "Brands");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Users_DeletedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Users_CreatedById",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_Colors_Users_DeletedById",
                table: "Colors");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactComments_Users_CreatedById",
                table: "ContactComments");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactComments_Users_DeletedById",
                table: "ContactComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_CreatedById",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_Faqs_Users_DeletedById",
                table: "Faqs");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Users_CreatedById",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Users_DeletedById",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_CreatedById",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Users_DeletedById",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPricing_Users_CreatedById",
                table: "ProductPricing");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPricing_Users_DeletedById",
                table: "ProductPricing");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Users_DeletedById",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Users_CreatedById",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Sizes_Users_DeletedById",
                table: "Sizes");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Users_CreatedById",
                table: "Specifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Specifications_Users_DeletedById",
                table: "Specifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_CreatedById",
                table: "Subscribes");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscribes_Users_DeletedById",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_CreatedById",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Subscribes_DeletedById",
                table: "Subscribes");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_CreatedById",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Specifications_DeletedById",
                table: "Specifications");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_CreatedById",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Sizes_DeletedById",
                table: "Sizes");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedById",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedById",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductPricing_CreatedById",
                table: "ProductPricing");

            migrationBuilder.DropIndex(
                name: "IX_ProductPricing_DeletedById",
                table: "ProductPricing");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_CreatedById",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_DeletedById",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_CreatedById",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_PostTags_DeletedById",
                table: "PostTags");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_CreatedById",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_Faqs_DeletedById",
                table: "Faqs");

            migrationBuilder.DropIndex(
                name: "IX_ContactComments_CreatedById",
                table: "ContactComments");

            migrationBuilder.DropIndex(
                name: "IX_ContactComments_DeletedById",
                table: "ContactComments");

            migrationBuilder.DropIndex(
                name: "IX_Colors_CreatedById",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Colors_DeletedById",
                table: "Colors");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Brands_CreatedById",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_Brands_DeletedById",
                table: "Brands");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_CreatedById",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_DeletedById",
                table: "BlogPosts");
        }
    }
}
