using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
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
                    { new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), new DateTime(2005, 9, 17, 10, 28, 25, 210, DateTimeKind.Utc).AddTicks(1186), "Belarus", "name4", "LastName4" },
                    { new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), new DateTime(1943, 9, 17, 10, 28, 25, 210, DateTimeKind.Utc).AddTicks(1112), "Belarus", "name0", "LastName0" },
                    { new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), new DateTime(1934, 9, 17, 10, 28, 25, 210, DateTimeKind.Utc).AddTicks(1154), "Belarus", "name2", "LastName2" },
                    { new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), new DateTime(2002, 9, 17, 10, 28, 25, 210, DateTimeKind.Utc).AddTicks(1150), "Belarus", "name1", "LastName1" },
                    { new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), new DateTime(1959, 9, 17, 10, 28, 25, 210, DateTimeKind.Utc).AddTicks(1158), "Belarus", "name3", "LastName3" }
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
                    { new Guid("05b64e93-2e93-4b35-bd7d-b7dddf3c786d"), "05b64e93-2e93-4b35-bd7d-b7dddf3c786d@test.test", "AQAAAAIAAYagAAAAEFBrdPd/+2tsUMsEFKeM0p4EaPMeeoyQi7tuxBGadvvkWBdGdgvPoP0knGU82qc3HQ==", "user2" },
                    { new Guid("13587099-6e91-4925-aa81-0b51b703cb08"), "admin@admin.com", "AQAAAAIAAYagAAAAEHKvhHQrDZqG+NJNLCoaKbOEKw0C7/s7b1C1weSU5ySJKgiBFjNrwizZeP9aCJYKHQ==", "admin" },
                    { new Guid("1b622585-cf17-423a-bb0d-d21ba892c730"), "1b622585-cf17-423a-bb0d-d21ba892c730@test.test", "AQAAAAIAAYagAAAAEJYXai2Mre/W46KoOuupWtdPQYsiO4DKK6yU8IMP2WNpIWEUES+MC0dLZhjYLvo/TQ==", "user3" },
                    { new Guid("28e19f4d-ac58-43c7-90df-9c20d904609b"), "28e19f4d-ac58-43c7-90df-9c20d904609b@test.test", "AQAAAAIAAYagAAAAEGmaVji0RmIDBHYmBycanpgVi0bD+hDqO/7kViSPOkHXxF0IQZRd4990lHBEBgIOvw==", "user1" },
                    { new Guid("659ad5a4-943e-48d6-bf02-e09a3ab2c53e"), "659ad5a4-943e-48d6-bf02-e09a3ab2c53e@test.test", "AQAAAAIAAYagAAAAEOqnvccYT3taSPVKPHd41rzPytn28VvZcvLHvxU27wcASAt2dpksbWBArIDNug8fwA==", "user4" },
                    { new Guid("9300db8a-fe2f-4044-b10e-51d4c4a592c3"), "9300db8a-fe2f-4044-b10e-51d4c4a592c3@test.test", "AQAAAAIAAYagAAAAEHoxV1Hw/BC/5MA+cshPJ6Uocl0zqgLDzdp3S2PjJQBJaWOmuHvXuL1LFua5wAcp3Q==", "user0" }
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
                    { new Guid("050dee5d-eac6-4116-8e6e-c8366f406475"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "050dee5d-eac6-4116-8e6e-c8366f406475", "8", "9790103737118", null, null, "050dee5d-eac6-4116-8e6e-c8366f406475" },
                    { new Guid("0f873442-c170-4928-8811-fd62c3569e11"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "0f873442-c170-4928-8811-fd62c3569e11", "7", "9790212317904", null, null, "0f873442-c170-4928-8811-fd62c3569e11" },
                    { new Guid("0fe2a2e7-3a04-464d-a433-fb478a95fdab"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "0fe2a2e7-3a04-464d-a433-fb478a95fdab", "1", "9795252752005", null, null, "0fe2a2e7-3a04-464d-a433-fb478a95fdab" },
                    { new Guid("12844e36-97f0-40b3-9b5c-426597b22b12"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "12844e36-97f0-40b3-9b5c-426597b22b12", "0", "9796097252453", null, null, "12844e36-97f0-40b3-9b5c-426597b22b12" },
                    { new Guid("218a3563-60dd-4b02-93da-d5c560a82587"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "218a3563-60dd-4b02-93da-d5c560a82587", "6", "9787142714392", null, null, "218a3563-60dd-4b02-93da-d5c560a82587" },
                    { new Guid("2411f385-19df-4cef-9118-70ca66e9045b"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "2411f385-19df-4cef-9118-70ca66e9045b", "5", "9791973880447", null, null, "2411f385-19df-4cef-9118-70ca66e9045b" },
                    { new Guid("2e5efc69-de19-4b37-ae8e-e9d72bd92998"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "2e5efc69-de19-4b37-ae8e-e9d72bd92998", "2", "9787167032549", null, null, "2e5efc69-de19-4b37-ae8e-e9d72bd92998" },
                    { new Guid("3023fcf2-e2b6-4d33-b391-1c5f35d2b06d"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "3023fcf2-e2b6-4d33-b391-1c5f35d2b06d", "3", "9785870447322", null, null, "3023fcf2-e2b6-4d33-b391-1c5f35d2b06d" },
                    { new Guid("34b4aeef-1857-4d0b-b464-7107a0d7bda9"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "34b4aeef-1857-4d0b-b464-7107a0d7bda9", "6", "9791298987395", null, null, "34b4aeef-1857-4d0b-b464-7107a0d7bda9" },
                    { new Guid("3cba0e69-adf1-4c87-b95f-c192db04d323"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "3cba0e69-adf1-4c87-b95f-c192db04d323", "0", "9789740640578", null, null, "3cba0e69-adf1-4c87-b95f-c192db04d323" },
                    { new Guid("457b5b8c-82e7-4aef-9652-a26b475a46bf"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "457b5b8c-82e7-4aef-9652-a26b475a46bf", "3", "9798081367662", null, null, "457b5b8c-82e7-4aef-9652-a26b475a46bf" },
                    { new Guid("48ce5717-9e30-4577-99b5-8318b25bf51a"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "48ce5717-9e30-4577-99b5-8318b25bf51a", "9", "9796714051643", null, null, "48ce5717-9e30-4577-99b5-8318b25bf51a" },
                    { new Guid("55fc435b-0ff4-42a0-8f2c-669c34ec3119"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "55fc435b-0ff4-42a0-8f2c-669c34ec3119", "9", "9790875063354", null, null, "55fc435b-0ff4-42a0-8f2c-669c34ec3119" },
                    { new Guid("5954a06e-b5ff-41e0-9822-7a69ed8f63ad"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "5954a06e-b5ff-41e0-9822-7a69ed8f63ad", "6", "9784013048495", null, null, "5954a06e-b5ff-41e0-9822-7a69ed8f63ad" },
                    { new Guid("59df2174-4eeb-4a91-976b-dcc0ba6bf638"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "59df2174-4eeb-4a91-976b-dcc0ba6bf638", "9", "9788096939404", null, null, "59df2174-4eeb-4a91-976b-dcc0ba6bf638" },
                    { new Guid("5b2b2451-f39f-4d8e-9829-5e30b2958bb7"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "5b2b2451-f39f-4d8e-9829-5e30b2958bb7", "0", "9799137879115", null, null, "5b2b2451-f39f-4d8e-9829-5e30b2958bb7" },
                    { new Guid("6402877c-4c6e-4c79-825a-d43ac60a6b14"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "6402877c-4c6e-4c79-825a-d43ac60a6b14", "9", "9784565740892", null, null, "6402877c-4c6e-4c79-825a-d43ac60a6b14" },
                    { new Guid("66f8d39a-2ea5-4b70-94f6-60c9e3223291"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "66f8d39a-2ea5-4b70-94f6-60c9e3223291", "7", "9781846239557", null, null, "66f8d39a-2ea5-4b70-94f6-60c9e3223291" },
                    { new Guid("6bebcea5-dd7c-4025-95cc-27d6d14980fd"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "6bebcea5-dd7c-4025-95cc-27d6d14980fd", "5", "9785700247689", null, null, "6bebcea5-dd7c-4025-95cc-27d6d14980fd" },
                    { new Guid("6efa8a67-bce1-4379-a4ea-e3e68c6b1372"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "6efa8a67-bce1-4379-a4ea-e3e68c6b1372", "1", "9793678148501", null, null, "6efa8a67-bce1-4379-a4ea-e3e68c6b1372" },
                    { new Guid("6f3ddc7c-1030-4648-89a6-802e6fb29b16"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "6f3ddc7c-1030-4648-89a6-802e6fb29b16", "8", "9790948992123", null, null, "6f3ddc7c-1030-4648-89a6-802e6fb29b16" },
                    { new Guid("6ff9edb7-6cc6-4112-a1f7-4b96f363432e"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "6ff9edb7-6cc6-4112-a1f7-4b96f363432e", "4", "9788665140712", null, null, "6ff9edb7-6cc6-4112-a1f7-4b96f363432e" },
                    { new Guid("70acad60-1a5a-4c31-bf39-e64f42aa44ef"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "70acad60-1a5a-4c31-bf39-e64f42aa44ef", "3", "9796560349871", null, null, "70acad60-1a5a-4c31-bf39-e64f42aa44ef" },
                    { new Guid("71cdf849-4417-423d-af40-cc851e1b090c"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "71cdf849-4417-423d-af40-cc851e1b090c", "6", "9783083550198", null, null, "71cdf849-4417-423d-af40-cc851e1b090c" },
                    { new Guid("73750d59-9374-4f05-8697-fe842fc693f8"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "73750d59-9374-4f05-8697-fe842fc693f8", "3", "9794084632066", null, null, "73750d59-9374-4f05-8697-fe842fc693f8" },
                    { new Guid("75a0d61f-7b67-43e8-9e88-043ba7c74170"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "75a0d61f-7b67-43e8-9e88-043ba7c74170", "0", "9783541696659", null, null, "75a0d61f-7b67-43e8-9e88-043ba7c74170" },
                    { new Guid("769ecdc9-dd7d-453b-b472-19a47d5631e1"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "769ecdc9-dd7d-453b-b472-19a47d5631e1", "1", "9782668394653", null, null, "769ecdc9-dd7d-453b-b472-19a47d5631e1" },
                    { new Guid("78ba5769-9d1c-49a1-b30a-bbffe0e44017"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "78ba5769-9d1c-49a1-b30a-bbffe0e44017", "0", "9792421708733", null, null, "78ba5769-9d1c-49a1-b30a-bbffe0e44017" },
                    { new Guid("7fa24084-f4c6-43d2-8534-632c6e689b8d"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "7fa24084-f4c6-43d2-8534-632c6e689b8d", "4", "9782380043785", null, null, "7fa24084-f4c6-43d2-8534-632c6e689b8d" },
                    { new Guid("89a6258c-7c1b-48d4-8144-41b834a9ab8f"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "89a6258c-7c1b-48d4-8144-41b834a9ab8f", "2", "9794578146000", null, null, "89a6258c-7c1b-48d4-8144-41b834a9ab8f" },
                    { new Guid("8a703c97-b7c8-4ecc-ac88-ccd02bd008e4"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "8a703c97-b7c8-4ecc-ac88-ccd02bd008e4", "2", "9796706749176", null, null, "8a703c97-b7c8-4ecc-ac88-ccd02bd008e4" },
                    { new Guid("8ace0c57-ca5c-47b3-a82b-6e6a48ec0978"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "8ace0c57-ca5c-47b3-a82b-6e6a48ec0978", "2", "9785442092301", null, null, "8ace0c57-ca5c-47b3-a82b-6e6a48ec0978" },
                    { new Guid("92d6fb6e-f09a-46e9-95af-db8353d0a3f1"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "92d6fb6e-f09a-46e9-95af-db8353d0a3f1", "7", "9782350639673", null, null, "92d6fb6e-f09a-46e9-95af-db8353d0a3f1" },
                    { new Guid("933399df-720e-4fe3-9583-a4be57d00ed9"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "933399df-720e-4fe3-9583-a4be57d00ed9", "9", "9792258296625", null, null, "933399df-720e-4fe3-9583-a4be57d00ed9" },
                    { new Guid("97c57e94-fa53-41fc-a99c-d503cba5105e"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "97c57e94-fa53-41fc-a99c-d503cba5105e", "4", "9784197839780", null, null, "97c57e94-fa53-41fc-a99c-d503cba5105e" },
                    { new Guid("9dc5f4a8-0c60-48ea-b5e0-947b2504fb45"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "9dc5f4a8-0c60-48ea-b5e0-947b2504fb45", "6", "9799938600710", null, null, "9dc5f4a8-0c60-48ea-b5e0-947b2504fb45" },
                    { new Guid("a045d54e-b9c4-45f1-858e-641d13378b1a"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "a045d54e-b9c4-45f1-858e-641d13378b1a", "7", "9784666913485", null, null, "a045d54e-b9c4-45f1-858e-641d13378b1a" },
                    { new Guid("a765856f-3c1e-47c1-a50d-a2898e78686a"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "a765856f-3c1e-47c1-a50d-a2898e78686a", "1", "9796617275856", null, null, "a765856f-3c1e-47c1-a50d-a2898e78686a" },
                    { new Guid("aa0ea0e0-e0f3-41fc-b41f-c7b979b02626"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "aa0ea0e0-e0f3-41fc-b41f-c7b979b02626", "3", "9782986826911", null, null, "aa0ea0e0-e0f3-41fc-b41f-c7b979b02626" },
                    { new Guid("ac41fd90-82f8-4a67-a1db-d550d80944b2"), new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"), 0, null, "ac41fd90-82f8-4a67-a1db-d550d80944b2", "2", "9799748061299", null, null, "ac41fd90-82f8-4a67-a1db-d550d80944b2" },
                    { new Guid("b75499cf-fd94-49d4-a3ab-16991931e2e4"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "b75499cf-fd94-49d4-a3ab-16991931e2e4", "5", "9789676908520", null, null, "b75499cf-fd94-49d4-a3ab-16991931e2e4" },
                    { new Guid("b9c3ad0f-3ef5-4215-89f6-7304b37f83b9"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "b9c3ad0f-3ef5-4215-89f6-7304b37f83b9", "5", "9786983195421", null, null, "b9c3ad0f-3ef5-4215-89f6-7304b37f83b9" },
                    { new Guid("bbf5215e-9c3d-46f6-8ad6-f965d0840faf"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "bbf5215e-9c3d-46f6-8ad6-f965d0840faf", "8", "9792050613286", null, null, "bbf5215e-9c3d-46f6-8ad6-f965d0840faf" },
                    { new Guid("ca66d934-9647-4c2e-a387-563a974bd96b"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "ca66d934-9647-4c2e-a387-563a974bd96b", "5", "9784658539426", null, null, "ca66d934-9647-4c2e-a387-563a974bd96b" },
                    { new Guid("cc481dff-1c15-4fef-8d54-629c64b261d7"), new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"), 0, null, "cc481dff-1c15-4fef-8d54-629c64b261d7", "4", "9788471754585", null, null, "cc481dff-1c15-4fef-8d54-629c64b261d7" },
                    { new Guid("ddd1c2fe-bd83-4f10-b1f4-5e0ecf544ab0"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "ddd1c2fe-bd83-4f10-b1f4-5e0ecf544ab0", "4", "9793050300749", null, null, "ddd1c2fe-bd83-4f10-b1f4-5e0ecf544ab0" },
                    { new Guid("e07f5133-ac56-44e6-b2ab-2464058fc796"), new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"), 0, null, "e07f5133-ac56-44e6-b2ab-2464058fc796", "1", "9785171857769", null, null, "e07f5133-ac56-44e6-b2ab-2464058fc796" },
                    { new Guid("f0c30ca7-366b-4b2a-8ff8-31336f205477"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "f0c30ca7-366b-4b2a-8ff8-31336f205477", "8", "9798305684995", null, null, "f0c30ca7-366b-4b2a-8ff8-31336f205477" },
                    { new Guid("f57b959c-266c-443e-9999-29b5157651f2"), new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"), 0, null, "f57b959c-266c-443e-9999-29b5157651f2", "8", "9784716783495", null, null, "f57b959c-266c-443e-9999-29b5157651f2" },
                    { new Guid("fe5a50da-60ea-4305-87d4-a5cbee048026"), new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"), 0, null, "fe5a50da-60ea-4305-87d4-a5cbee048026", "7", "9783661472010", null, null, "fe5a50da-60ea-4305-87d4-a5cbee048026" }
                });

            migrationBuilder.InsertData(
                table: "UserAccessGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("13587099-6e91-4925-aa81-0b51b703cb08") },
                    { 2, new Guid("13587099-6e91-4925-aa81-0b51b703cb08") }
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
