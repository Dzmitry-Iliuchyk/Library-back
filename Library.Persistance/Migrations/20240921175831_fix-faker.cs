using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fixfaker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessGroupPermission",
                columns: table => new
                {
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    PermissionId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessGroupPermission", x => new { x.GroupId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_AccessGroupPermission_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessGroupPermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ISBN = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Genre = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uuid", nullable: false),
                    BookType = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    TakenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReturnTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Books_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAccessGroup",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAccessGroup", x => new { x.GroupId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserAccessGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAccessGroup_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Birthday", "Country", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), new DateTime(1917, 9, 21, 17, 58, 30, 519, DateTimeKind.Utc).AddTicks(2777), "Morocco", "Vicki", "Kunze" },
                    { new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), new DateTime(2004, 9, 21, 17, 58, 30, 515, DateTimeKind.Utc).AddTicks(2346), "Bhutan", "Kari", "Tillman" },
                    { new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), new DateTime(1977, 9, 21, 17, 58, 30, 517, DateTimeKind.Utc).AddTicks(2924), "Colombia", "Van", "Wilderman" },
                    { new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), new DateTime(1935, 9, 21, 17, 58, 30, 510, DateTimeKind.Utc).AddTicks(8659), "Jersey", "Norman", "Bashirian" },
                    { new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), new DateTime(2004, 9, 21, 17, 58, 30, 512, DateTimeKind.Utc).AddTicks(7542), "New Caledonia", "Lana", "Kessler" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Create" },
                    { 2, "Update" },
                    { 3, "Delete" },
                    { 4, "Read" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserName" },
                values: new object[,]
                {
                    { new Guid("33e353fe-2ea2-447d-b5c3-49e334de061f"), "Antonia86@gmail.com", "AQAAAAIAAYagAAAAEGPkEgSr3TR7xKXMnM+NsO94fg3i3WJsjlAVA8KiRhnBnt+Wsm+4qiqTTI3rK2Mypg==", "Antonia_Dicki" },
                    { new Guid("3a2ba859-2f41-4938-9ab1-439c55cec181"), "Rodolfo22@yahoo.com", "AQAAAAIAAYagAAAAEBbVe+IXyXFKIj27SSNgvvF0Toc/r9ZubY+3Oq1WyzX8UcQt22MSkWuhyksha672qA==", "Rodolfo44" },
                    { new Guid("3c154de9-8af7-4e21-9d25-a604980aa88b"), "Santiago6@hotmail.com", "AQAAAAIAAYagAAAAEF5VCP7IhbRCD8C+gADuQImf8YCn+xu9wMl/3TECckWZ8UX1ET3xXur7MsODglW37w==", "Santiago75" },
                    { new Guid("6334c50b-eb6c-41f1-b752-d232054656cf"), "Ryan69@gmail.com", "AQAAAAIAAYagAAAAEHFXrGk2CxCWgFxJPSOlszpL/WB5SMqVtQXd2g16cPXokMlIzBcFkwTYTIaBjD2qcA==", "Ryan_Bailey" },
                    { new Guid("797b66f0-ee97-4ab3-8ad9-c383cb113bbf"), "admin@admin.com", "AQAAAAIAAYagAAAAEAY0fjAYzO2pKqIlTnJtvk9fOigwyY3gLLFj4zBoURG2e0FIZlUOa4vfDbelHsc7rA==", "admin" },
                    { new Guid("ca0882f0-8bd5-4ff3-839e-0ee4e1c8b878"), "Annette25@hotmail.com", "AQAAAAIAAYagAAAAEOY7IaUpZ2yr2pLM9qraHXku9Bkm7htfsfgjkcIE7P7DGprssdwbglDDKK0cSshzaA==", "Annette_Bayer23" }
                });

            migrationBuilder.InsertData(
                table: "AccessGroupPermission",
                columns: new[] { "GroupId", "PermissionId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 1, 3 },
                    { 1, 4 },
                    { 2, 4 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookType", "ClientId", "Description", "Genre", "ISBN", "ReturnTo", "TakenAt", "Title" },
                values: new object[,]
                {
                    { new Guid("0192be1a-7fa2-49c4-bac3-c88dfa009846"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Doloremque cupiditate enim minus nam minima velit nostrum ut ducimus ut eius placeat alias accusamus pariatur officiis placeat dignissimos et rem in veniam quasi doloribus adipisci velit ut eum labore molestias in nesciunt hic voluptas iste consectetur sequi reiciendis sequi occaecati quas ea vel voluptas atque delectus fugiat libero possimus.", "qui", "9781779722515", null, null, "Laborum molestias ipsum et." },
                    { new Guid("03edb50f-dc67-449f-a7f7-22ab2e05885b"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Quis nulla temporibus quidem quia omnis pariatur cum aut eveniet impedit officiis labore et quae voluptatem suscipit quaerat assumenda quo possimus saepe et modi et esse molestiae aliquam reiciendis neque voluptate ullam debitis neque voluptates similique et dicta reiciendis veniam ab molestiae eum et rerum repudiandae corporis dignissimos rerum qui.", "labore", "9792341655926", null, null, "Recusandae blanditiis sed animi quam non qui aut et." },
                    { new Guid("0b7a6916-8e8c-4845-8fcd-d4ffc3fc2004"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Qui consequatur voluptatem ullam aut aut cupiditate ut suscipit enim minus recusandae voluptatum eligendi nulla et mollitia ducimus reprehenderit dicta sint cupiditate voluptates sit delectus nihil rerum vero hic assumenda quo rem corrupti quia aliquam quia et dolor cupiditate corrupti eos ducimus sint et praesentium porro rerum nihil voluptatem.", "autem", "9797167938222", null, null, "Qui consequatur et dolor aspernatur eum." },
                    { new Guid("0e90bd34-993d-41c1-94b5-4812c80d0364"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Consequatur quia autem ratione molestias dignissimos ut rerum vero laudantium repudiandae ea eligendi sunt magni sint est omnis illo voluptas sed aut necessitatibus temporibus molestias beatae eveniet quasi et et doloribus quidem suscipit voluptatibus aut officiis ut praesentium porro ullam optio et enim eligendi iusto nam dolores itaque fugit aut alias eveniet.", "fuga", "9792144428925", null, null, "Voluptatem consequatur nobis ullam quae ad et." },
                    { new Guid("0f80a27e-d2ed-4222-ab3e-fbb556ab64e4"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Porro explicabo aperiam eaque nam voluptate dicta laudantium est est facere dolor voluptates laborum neque beatae eius ducimus aut laudantium non ut odit quisquam vero reiciendis cupiditate perferendis enim eos et aut nemo optio laudantium in quas possimus corrupti enim ut animi corporis laboriosam laudantium cupiditate deserunt dolorem officia in vel exercitationem possimus vitae tempora.", "impedit", "9782008592404", null, null, "Distinctio iusto sapiente animi molestiae." },
                    { new Guid("17aa0bac-4c90-4012-aed8-4fcfdc399f16"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Maxime voluptatem aut consectetur debitis ut ea sunt deserunt officia harum et quam voluptatem fuga sunt est sunt dolores quas consectetur beatae.", "et", "9789068512229", null, null, "Alias similique sed." },
                    { new Guid("1c84f964-3a82-4ef6-9fb7-989d61a78059"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Rerum sunt nihil magnam enim omnis nihil accusantium quisquam qui quasi voluptatibus dolore qui atque enim nisi natus ut optio rerum dolor eveniet sit sit nostrum quis consequatur quia ab similique eos consectetur sed cumque omnis omnis quidem eum aut officia neque omnis ducimus vitae ad nihil deserunt aut ea at eius eaque natus.", "in", "9796136479544", null, null, "Veniam impedit et accusamus sit." },
                    { new Guid("1db9d341-690f-40ac-a265-365af51c34d9"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Non nesciunt eaque optio quasi nisi vitae fugit quia natus quo accusamus soluta quisquam consequatur quam animi placeat quasi earum consequatur id blanditiis autem at quis id vel laudantium praesentium voluptatem ut quia laboriosam perspiciatis ut earum eum id dignissimos quidem iste in quasi ipsum ab molestias quia quam quibusdam qui repudiandae magnam quisquam omnis qui minus.", "ad", "9794520760094", null, null, "Omnis sapiente est repudiandae." },
                    { new Guid("21ed6038-96ea-40f8-8ae8-6ade4cb03f7d"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Sit ipsum et eos quia aliquid laborum labore eaque voluptates labore harum est quod enim quod asperiores ea ducimus nesciunt consequatur consectetur amet cumque aut eaque officia assumenda impedit a unde maxime at exercitationem sapiente et odit veniam.", "est", "9785339117018", null, null, "Et quas quidem." },
                    { new Guid("250775c8-0fd9-4520-ba88-414e7cd545e1"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Laboriosam dolorem quia laboriosam et accusamus nostrum atque officiis sed rerum minima consequatur nihil quae et et sunt non ut eum doloribus itaque quam quos adipisci unde qui nesciunt possimus ex sapiente qui nam facere accusantium repudiandae est quis eveniet ea accusamus.", "maxime", "9781266472770", null, null, "Praesentium voluptatem labore exercitationem similique nihil nihil in veritatis dolore." },
                    { new Guid("289647c2-c983-4b1c-8705-54e43bd8e6e0"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Facilis soluta dolorem natus aut incidunt eveniet dolores voluptate voluptas nemo sed fuga ut velit quam maxime cumque iusto et delectus aliquam officia quasi rerum quam iste id quis eos sed quia eveniet eos sed quisquam aut vero magnam ex non quas voluptatem magni sint nulla sint quo rerum aut ipsam quae.", "architecto", "9791527707855", null, null, "Similique rerum aperiam velit aut et consequatur dolorem." },
                    { new Guid("34fbc964-ee79-465a-b03a-6e3625e83ad2"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Minima debitis excepturi ex nostrum perspiciatis sunt aspernatur excepturi ut molestias minus quia recusandae ut quasi laborum distinctio ab et veniam deleniti voluptates maxime provident amet sed incidunt voluptatem qui nostrum soluta fuga est sint praesentium voluptas voluptas sunt sed recusandae distinctio sed provident occaecati illum nostrum aperiam culpa voluptate dolores repudiandae distinctio esse et aut aut rem autem quae voluptate consequatur unde ab.", "reiciendis", "9793871774385", null, null, "Dolor eum sed quae autem ut vel exercitationem voluptatum." },
                    { new Guid("397eb421-77d7-44ee-a2d6-8d3787d8bc91"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Ipsam qui eos iusto nulla eaque suscipit ratione dolorem vero voluptatibus libero quo rerum a recusandae commodi harum iste rerum nemo veritatis ut eligendi quo quasi recusandae sit qui vel minus facilis porro consequatur quam voluptate reprehenderit eum consequatur dolor ad voluptatem delectus dolorem et dolorem aut omnis dolorem provident voluptates tempora ab.", "perspiciatis", "9788298404427", null, null, "Dolores aperiam ad nobis omnis impedit neque dolorem." },
                    { new Guid("4110cd1e-daab-45b9-9b9e-81ba2d5b4c1f"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Dolorem nesciunt numquam harum explicabo similique possimus id quaerat consequuntur expedita minima omnis nesciunt voluptatem est qui est repudiandae nemo dolor nihil suscipit.", "culpa", "9787654806103", null, null, "Nostrum quisquam sit vel rem ex ut." },
                    { new Guid("4656ec65-1e30-4977-ad99-52c3ba22dfe3"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Magni iure saepe temporibus vero vel ut magni eum aspernatur similique voluptatem optio minus sunt nulla magnam consequatur quae odit doloribus sit expedita reprehenderit distinctio et ducimus enim veniam.", "dolor", "9783365431658", null, null, "Porro nihil molestiae nemo deleniti." },
                    { new Guid("472fd710-9d7b-48ad-8c21-f10835d3e78f"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Nobis alias aliquam est id provident fugiat id reiciendis adipisci ea iure aut sequi non amet et qui aut est incidunt enim ex et voluptas ea non ab ea sunt non consectetur optio vel ratione impedit corporis dicta quo qui iusto nulla vero et id illo est minima quidem illo voluptas maxime sint sapiente cupiditate molestiae qui.", "rem", "9784351190207", null, null, "Atque eaque inventore tenetur." },
                    { new Guid("4a26985a-614f-42a0-82ff-4046681bd41e"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Eius voluptatem rerum eius minima ut nulla accusantium voluptatem accusamus earum iusto repudiandae qui aut iusto velit molestiae nam distinctio magni perspiciatis qui tenetur voluptas et dolores aperiam tempora officiis consectetur dolor nobis ea sint eveniet dolor ut sit.", "eius", "9784069429170", null, null, "Dolor maiores doloremque tempore." },
                    { new Guid("514e5954-3b9a-46dc-992b-07bffb44ffca"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Aperiam laboriosam deserunt eaque aut fugit sapiente assumenda dolores vel culpa ipsum ipsum sint omnis delectus nihil veritatis amet perferendis deleniti illum magni vel deserunt voluptatem ipsam sit rerum atque atque iure possimus porro odio aspernatur aut reprehenderit odit rerum ut doloremque dolores et adipisci quo eos veritatis assumenda corrupti.", "velit", "9782260355748", null, null, "Eos quisquam qui quod ipsa rem vero explicabo odio." },
                    { new Guid("54f3eeda-84f7-44c2-bb5d-231488137a91"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Eos accusamus et nihil deserunt culpa expedita ut illo accusamus sit architecto nihil molestiae tempora sunt sunt vitae harum sint saepe assumenda voluptatem molestiae modi expedita molestiae qui reiciendis similique voluptates error quia suscipit ut temporibus eum voluptatem tempora fugiat voluptate eligendi ut quidem et voluptatem porro est voluptate porro ducimus repellat exercitationem architecto nobis architecto eaque animi error distinctio assumenda praesentium dolores et recusandae voluptatem.", "totam", "9795949647058", null, null, "Alias ipsam ut odit nostrum sequi illum." },
                    { new Guid("5ceb5b5a-1bda-4fe1-9b90-0f6818573ade"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Tenetur dolore praesentium optio inventore quo perferendis hic necessitatibus voluptatibus laborum quia aliquid dicta totam et omnis nobis magni laborum et deleniti vitae ut quos consequatur et sequi eos non sit fugiat vel omnis sunt necessitatibus dignissimos sit qui qui perferendis labore debitis quo ab quaerat id repellendus aperiam odit nam laudantium id accusantium eligendi id voluptatibus quo quos accusamus.", "quia", "9795937195479", null, null, "Nihil dolor officia expedita rem veritatis molestiae." },
                    { new Guid("64460962-6988-4974-bbbe-ace95fc56c2e"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Officia rerum sed omnis perspiciatis quos eos voluptates et rem ratione cupiditate unde dolor earum unde velit eveniet dicta non impedit et delectus libero tempora autem voluptatem accusamus at reprehenderit aut rerum nam eos qui voluptate dolorum et rerum cum.", "quia", "9797876020621", null, null, "Ut mollitia ut voluptatibus consequatur suscipit sunt sed." },
                    { new Guid("66178617-3bc3-4c06-94f1-38717f75c59c"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Corrupti expedita est at exercitationem optio placeat voluptatum non et autem rerum iusto eum perferendis molestiae dicta veniam quia qui odit deleniti fugiat ipsa.", "nam", "9795832611623", null, null, "Possimus odit esse sint." },
                    { new Guid("6807dab3-5e47-4f0a-bab7-77e6919e746d"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Repudiandae omnis eaque omnis est velit vitae harum quae ullam impedit ut debitis quia laborum odio sit placeat modi dolorem aut delectus repellendus eligendi ut nihil ipsum magni ut facilis ad nisi praesentium aliquam aut quae amet non est odit in labore quaerat quidem rerum accusantium odit aspernatur.", "tenetur", "9790345366572", null, null, "Voluptatem minus maxime aperiam est praesentium." },
                    { new Guid("6bac6908-132f-4acb-a9c7-4b83b883bf9e"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Quia ullam sequi modi ipsa sint dolor quisquam sit iusto corporis reiciendis reprehenderit eum quo quo eum aut voluptatem eaque qui quam error nostrum aspernatur qui asperiores aut tenetur impedit magni ipsam perferendis accusamus voluptate voluptates et rerum fuga temporibus sed cumque assumenda est enim in delectus dolor non consectetur iure reprehenderit culpa in ut tempore a non quaerat est voluptas quaerat explicabo reprehenderit doloribus.", "consequatur", "9780088391238", null, null, "Libero minima doloremque libero enim aperiam." },
                    { new Guid("7169e3df-a1e7-462d-89f5-6309adf6b615"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Cum eligendi voluptatum optio delectus accusantium nisi excepturi quae facilis maiores occaecati veniam harum reiciendis quia expedita cum aut maiores facilis similique omnis doloribus vel quia quae ut eos et consequatur vel quo voluptatem quaerat voluptatem accusamus numquam officia et minus et amet ut molestias eos ad officia consequatur ipsum ad ipsa error voluptates est ullam molestias deserunt consequatur qui modi nemo.", "qui", "9799503295273", null, null, "Molestiae voluptatem id tenetur vel." },
                    { new Guid("74a5f80f-9e1e-4a4d-86b7-c5de24303856"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Eum numquam et quis officiis optio et non ea accusamus voluptatum repudiandae cupiditate quidem ut temporibus ea aspernatur quis tenetur dignissimos sequi velit sint in.", "officia", "9791293638452", null, null, "Delectus quae aspernatur et sit." },
                    { new Guid("74cf362a-aea4-41b8-b7fd-5271ae8ba589"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Sit quasi ad tempore quibusdam voluptatem voluptatum aspernatur dicta consequatur dolore ut corporis aut ut commodi illo dolorem qui corrupti soluta optio ipsa non explicabo dolor minus quasi et.", "eum", "9792370647343", null, null, "Non vel excepturi voluptas odit iste velit." },
                    { new Guid("7ecb1565-a863-4e4c-ae16-7e754053fb45"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Aut molestiae nulla maiores quia recusandae amet sit optio iure est explicabo autem quam beatae dicta quas quisquam illo assumenda non perferendis similique officiis asperiores qui neque voluptas ut qui dolore cumque sapiente alias laboriosam et illum officiis saepe fugit voluptatibus dolor id veniam dolorem omnis optio a sint amet nihil qui sed quo qui cum cupiditate libero nulla dolorem et et quasi harum eaque fugit consequatur sunt eligendi.", "vitae", "9786580400300", null, null, "Vitae voluptate et ad non aperiam iure sit numquam." },
                    { new Guid("8687d3b4-5aed-4958-9710-4c13d2dfde41"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Occaecati sapiente ea dolores unde est porro recusandae quidem sunt quia aliquam debitis autem fuga ipsum recusandae consequuntur qui sit ratione delectus qui distinctio ab magni voluptatem minus aut quia reprehenderit hic praesentium nobis vel eum sit voluptates dolores est reiciendis ex molestiae quo unde mollitia numquam necessitatibus ullam est voluptatem qui amet sit voluptatum iure deserunt reiciendis in.", "sit", "9796776363685", null, null, "Voluptas sit eaque voluptate velit nobis autem modi et." },
                    { new Guid("9db3f32f-1afe-42a4-bae1-0cd1cbffc110"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Sed aliquam sint animi perspiciatis rerum optio aut occaecati architecto dolor explicabo sed qui perspiciatis ut non culpa a sed harum quisquam eaque tempora voluptatem culpa asperiores excepturi beatae similique autem beatae quae aut occaecati qui deleniti iste alias possimus adipisci illum voluptatibus praesentium omnis placeat sit id dicta sint.", "expedita", "9781605142593", null, null, "Itaque atque autem et exercitationem corporis hic." },
                    { new Guid("a003a588-e3fc-46b8-9d20-1817032f0e5d"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Quasi inventore minima placeat error aut et praesentium aperiam eum impedit quaerat earum quam architecto nemo qui sequi aut delectus aliquid aut architecto delectus optio voluptatem est autem et a neque sit qui sint vitae tenetur quo assumenda illo dolore quibusdam sunt exercitationem culpa occaecati dolorem ut facere in molestias voluptatibus laboriosam ut unde minus.", "tenetur", "9786819146733", null, null, "Vitae numquam sed cumque." },
                    { new Guid("a2b6a85c-996e-4e29-a90a-7c554cd7e53d"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Possimus quod ipsam aut amet voluptas perspiciatis voluptate ipsam quasi distinctio accusamus in omnis non dolorem omnis mollitia doloremque vel et accusamus nisi facere cupiditate libero quibusdam perferendis fugit reprehenderit eius quaerat sed aut alias ut aperiam ut labore optio occaecati quia et totam voluptas qui quia autem et excepturi omnis quibusdam officia voluptas nihil nostrum facilis repellat itaque dolores et temporibus tenetur alias veniam architecto eum optio qui molestiae.", "et", "9786460027122", null, null, "A dolor architecto eligendi." },
                    { new Guid("a3f3d438-f93e-43af-9e37-dffd9c9f98f6"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Iure hic quaerat magni labore doloribus tempore harum et nostrum quos sunt harum tenetur dolor facilis sunt in et tempora consequatur eligendi architecto eveniet sapiente dolore ipsam impedit quam mollitia dolores rerum est maxime veritatis commodi libero totam et consequuntur non quia id commodi veritatis repellat dicta officiis distinctio facilis corporis ab saepe voluptates laborum asperiores voluptatibus fugit provident.", "et", "9798104083852", null, null, "Odio aut autem aut facere velit velit quia." },
                    { new Guid("a6b35bcd-be1b-4341-a82c-2713f70e19f9"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Non iure placeat consequuntur et sed ut nam officia autem minus fugit enim vitae quidem dolorem hic aut quia itaque.", "eos", "9796625473428", null, null, "Saepe dolorem aut velit quo explicabo assumenda et aliquam quia." },
                    { new Guid("a763ee55-2093-4dfa-b499-c7fc3c908a28"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Ea perspiciatis veniam exercitationem qui quisquam et eaque suscipit harum cum corporis iste velit qui repudiandae optio quaerat ut laboriosam nemo at libero libero cupiditate rerum et cum beatae ut repudiandae voluptatum dolor officia neque mollitia qui similique ipsa nemo tempora rem in optio nostrum architecto ipsam totam est blanditiis dignissimos magni voluptas et ad.", "error", "9794570747793", null, null, "Expedita reprehenderit corporis tempore reprehenderit eum possimus quam et." },
                    { new Guid("ae187c60-8278-473b-a799-71b2959b434f"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Repellendus nihil totam enim minima quis est sunt beatae vel est et blanditiis rerum dolorem quia accusantium est quo molestiae hic perferendis consequatur quos quis fugiat esse consequatur et debitis qui impedit soluta et consequatur voluptas sint veniam repellat.", "voluptatem", "9784393258354", null, null, "Et enim ullam maxime non dolor consequatur possimus accusantium." },
                    { new Guid("b10b74cf-5de0-4c13-8bbd-72fbdddaf0b3"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Aliquid assumenda alias quos aut voluptate qui at quisquam sequi assumenda quaerat qui tenetur dolor et sequi ratione autem non delectus expedita architecto minus et.", "sit", "9786999765960", null, null, "Velit dolore necessitatibus blanditiis amet expedita nisi nam quod." },
                    { new Guid("b99b46b2-70a1-4900-ab18-d9eef87edaa9"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Blanditiis enim odit deserunt voluptatem non unde rem et laudantium ipsam quas sed laboriosam corrupti facere sit qui cumque culpa nesciunt molestiae vel est culpa qui debitis voluptatem quia tempora doloremque molestiae nostrum atque.", "temporibus", "9798920273994", null, null, "Necessitatibus similique dolorem." },
                    { new Guid("c6700723-a989-4d32-9835-35134ea73ddd"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Sequi ea id qui sint nesciunt consequatur eum aspernatur et odio possimus architecto laborum iusto est quia qui cumque officiis sint repellat beatae ut adipisci voluptates voluptatibus assumenda adipisci eos et provident nostrum quis porro dolorem necessitatibus pariatur soluta provident.", "autem", "9792539524317", null, null, "Molestiae est aut praesentium corporis ipsum inventore laboriosam autem quia." },
                    { new Guid("c7a8568b-8cc6-47a8-ba3b-ceb62be5e6ee"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Et sunt unde et cum quo ipsam natus harum sit autem esse voluptas aut omnis atque facilis rem inventore veritatis voluptatem quod aliquid quam tempore aut vel omnis ab aut mollitia eum aut quia earum consequuntur quos ea fuga et velit necessitatibus placeat similique est a molestiae sequi.", "accusantium", "9783846479070", null, null, "Aut sint ad incidunt." },
                    { new Guid("cdbfae59-52bd-4576-a6a7-af0814f989f1"), new Guid("c23364a6-b1f4-4bdb-a743-67820ba1f66b"), 0, null, "Nostrum esse pariatur odio labore nihil aspernatur amet illo ea harum veniam minus dolore aut eum voluptas molestias veniam sed vel quo in vel est sed consequatur deleniti quisquam expedita voluptas qui odio dolores ut illo vel velit consectetur quos facilis quo sed omnis totam enim perspiciatis sit sed incidunt aut.", "odit", "9787652535562", null, null, "Non inventore est aliquid sint." },
                    { new Guid("cec8125e-4096-4b99-9879-a2e3c9a22a65"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Ad quia ratione aut facere modi quidem quod minima fugit qui nihil corporis labore iusto est libero officia officiis quia est iusto est consequatur omnis at ut officiis illo voluptatem animi neque aliquid doloremque qui iusto minima est nihil commodi quibusdam dolorem quaerat quia molestiae expedita repellendus eaque odit quia voluptatibus quo nobis facilis.", "eum", "9787914205509", null, null, "A labore natus aut ad." },
                    { new Guid("d6c40ee1-b4f3-44f5-81ff-94b55bba1ae0"), new Guid("d25e0bc3-c9a6-45f5-9f54-5bc61569d7e5"), 0, null, "Libero veniam eaque quia mollitia sint ut sit non rerum et commodi illum voluptatibus enim quis iusto dolorem voluptatem accusamus quia quibusdam voluptatibus aut est veniam ut animi enim officiis quis eum et sit nihil soluta in sequi quia et ducimus molestiae officia atque id molestiae molestiae in qui aut nisi exercitationem ut est ullam.", "reprehenderit", "9798254585992", null, null, "Id doloribus nam corrupti et cumque." },
                    { new Guid("d98ed224-0c02-4522-a626-c1d09f43f396"), new Guid("e0a3724e-c4fe-4f99-95cf-48f359541fb0"), 0, null, "Omnis accusantium possimus ea qui qui nam veniam eaque sapiente at ipsa explicabo saepe nostrum nihil tempora tempore possimus repellendus ipsa placeat unde blanditiis et minus est ipsam provident unde ea qui quos provident consequatur est.", "nostrum", "9794712064702", null, null, "Tenetur placeat dolores debitis eum qui." },
                    { new Guid("de15580f-492c-4ef1-ae23-94700d2824ad"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Sed aut reprehenderit reprehenderit eos possimus veniam voluptatem eum corrupti nemo nostrum iste minus laboriosam aut rerum nihil reprehenderit dolorum porro eos nisi quidem voluptates enim reprehenderit nemo qui quis rerum praesentium consequatur nulla sapiente cumque nihil cum eaque pariatur at voluptas ut fugit eum ducimus quas consequatur repudiandae expedita inventore dolore aut possimus et pariatur pariatur et quia omnis illum placeat facilis quis odio.", "harum", "9794079819304", null, null, "Nesciunt maiores itaque." },
                    { new Guid("e4a8cc01-7737-4074-bd63-695a9eb7e574"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Sit dolorem magnam nobis molestias dolore praesentium voluptatem quia sapiente suscipit velit amet eos quam sed quo facilis earum amet et id molestiae mollitia est in dicta soluta sapiente voluptatem omnis expedita adipisci doloremque reiciendis autem qui sunt assumenda iusto facilis quos officia reprehenderit ut facilis similique impedit voluptatem nihil deleniti rerum et autem ad debitis voluptas eum nisi inventore ratione sint.", "aut", "9783483676603", null, null, "Doloribus architecto error eligendi." },
                    { new Guid("ea4516e2-3eda-49aa-8d52-2bed98344d3e"), new Guid("ff826d19-91e0-46c3-89a0-80f1c93cef28"), 0, null, "Est mollitia cupiditate in qui rerum sit ut voluptatem et eaque illo tempore nulla eum asperiores et ut ipsum dolorum at modi accusantium explicabo commodi aut assumenda est.", "tempore", "9781777677749", null, null, "Sit ut id doloremque aliquid quisquam." },
                    { new Guid("ee7b1118-3dc1-4de0-8614-6f9af9439770"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Recusandae expedita harum deserunt labore accusantium expedita iusto optio reprehenderit et eum aliquam natus repellat aperiam sint aut eum et nemo aperiam et excepturi fuga aspernatur quasi saepe laboriosam expedita libero quam vitae aut qui vel odit at aut est ut praesentium repellat mollitia corporis cum saepe ut vel voluptatibus rem sed unde earum ab enim laudantium deserunt est est.", "vero", "9794943800421", null, null, "Ea asperiores aut similique." },
                    { new Guid("f635eccf-c9c5-47ff-b113-454bfe09876f"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Rerum rerum rerum quibusdam aut impedit ut omnis distinctio porro est at doloribus velit ad ab et neque sint vero repellendus aspernatur iusto et ducimus nisi quaerat neque sed perferendis quisquam ratione deserunt velit sed minus nihil et.", "aut", "9798342132268", null, null, "Repudiandae beatae velit rerum fugiat culpa aut dolorem." },
                    { new Guid("f8900264-36b0-4311-93a6-599443e21921"), new Guid("41e16b8b-4b44-4bb2-accb-165a8b1ddcab"), 0, null, "Quia corrupti aperiam nesciunt quas fugiat et sunt deserunt repellat ex velit aut aut placeat exercitationem voluptatem quisquam sit consequatur distinctio iure nesciunt odio ipsa.", "ipsa", "9798965806621", null, null, "Sequi aperiam excepturi ad sint." }
                });

            migrationBuilder.InsertData(
                table: "UserAccessGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("797b66f0-ee97-4ab3-8ad9-c383cb113bbf") },
                    { 2, new Guid("33e353fe-2ea2-447d-b5c3-49e334de061f") },
                    { 2, new Guid("3a2ba859-2f41-4938-9ab1-439c55cec181") },
                    { 2, new Guid("3c154de9-8af7-4e21-9d25-a604980aa88b") },
                    { 2, new Guid("6334c50b-eb6c-41f1-b752-d232054656cf") },
                    { 2, new Guid("797b66f0-ee97-4ab3-8ad9-c383cb113bbf") },
                    { 2, new Guid("ca0882f0-8bd5-4ff3-839e-0ee4e1c8b878") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessGroupPermission_PermissionId",
                table: "AccessGroupPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ClientId",
                table: "Books",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAccessGroup_UserId",
                table: "UserAccessGroup",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessGroupPermission");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserAccessGroup");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
