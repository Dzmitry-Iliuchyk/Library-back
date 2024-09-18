using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addrefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("050dee5d-eac6-4116-8e6e-c8366f406475"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("0f873442-c170-4928-8811-fd62c3569e11"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("0fe2a2e7-3a04-464d-a433-fb478a95fdab"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("12844e36-97f0-40b3-9b5c-426597b22b12"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("218a3563-60dd-4b02-93da-d5c560a82587"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2411f385-19df-4cef-9118-70ca66e9045b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2e5efc69-de19-4b37-ae8e-e9d72bd92998"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("3023fcf2-e2b6-4d33-b391-1c5f35d2b06d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("34b4aeef-1857-4d0b-b464-7107a0d7bda9"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("3cba0e69-adf1-4c87-b95f-c192db04d323"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("457b5b8c-82e7-4aef-9652-a26b475a46bf"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("48ce5717-9e30-4577-99b5-8318b25bf51a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("55fc435b-0ff4-42a0-8f2c-669c34ec3119"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5954a06e-b5ff-41e0-9822-7a69ed8f63ad"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("59df2174-4eeb-4a91-976b-dcc0ba6bf638"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5b2b2451-f39f-4d8e-9829-5e30b2958bb7"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6402877c-4c6e-4c79-825a-d43ac60a6b14"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("66f8d39a-2ea5-4b70-94f6-60c9e3223291"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6bebcea5-dd7c-4025-95cc-27d6d14980fd"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6efa8a67-bce1-4379-a4ea-e3e68c6b1372"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6f3ddc7c-1030-4648-89a6-802e6fb29b16"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("6ff9edb7-6cc6-4112-a1f7-4b96f363432e"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("70acad60-1a5a-4c31-bf39-e64f42aa44ef"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("71cdf849-4417-423d-af40-cc851e1b090c"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("73750d59-9374-4f05-8697-fe842fc693f8"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("75a0d61f-7b67-43e8-9e88-043ba7c74170"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("769ecdc9-dd7d-453b-b472-19a47d5631e1"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("78ba5769-9d1c-49a1-b30a-bbffe0e44017"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7fa24084-f4c6-43d2-8534-632c6e689b8d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("89a6258c-7c1b-48d4-8144-41b834a9ab8f"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("8a703c97-b7c8-4ecc-ac88-ccd02bd008e4"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("8ace0c57-ca5c-47b3-a82b-6e6a48ec0978"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("92d6fb6e-f09a-46e9-95af-db8353d0a3f1"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("933399df-720e-4fe3-9583-a4be57d00ed9"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("97c57e94-fa53-41fc-a99c-d503cba5105e"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("9dc5f4a8-0c60-48ea-b5e0-947b2504fb45"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("a045d54e-b9c4-45f1-858e-641d13378b1a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("a765856f-3c1e-47c1-a50d-a2898e78686a"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("aa0ea0e0-e0f3-41fc-b41f-c7b979b02626"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ac41fd90-82f8-4a67-a1db-d550d80944b2"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b75499cf-fd94-49d4-a3ab-16991931e2e4"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b9c3ad0f-3ef5-4215-89f6-7304b37f83b9"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("bbf5215e-9c3d-46f6-8ad6-f965d0840faf"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ca66d934-9647-4c2e-a387-563a974bd96b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("cc481dff-1c15-4fef-8d54-629c64b261d7"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ddd1c2fe-bd83-4f10-b1f4-5e0ecf544ab0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e07f5133-ac56-44e6-b2ab-2464058fc796"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("f0c30ca7-366b-4b2a-8ff8-31336f205477"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("f57b959c-266c-443e-9999-29b5157651f2"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("fe5a50da-60ea-4305-87d4-a5cbee048026"));

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, new Guid("13587099-6e91-4925-aa81-0b51b703cb08") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("13587099-6e91-4925-aa81-0b51b703cb08") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("05b64e93-2e93-4b35-bd7d-b7dddf3c786d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("1b622585-cf17-423a-bb0d-d21ba892c730"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("28e19f4d-ac58-43c7-90df-9c20d904609b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("659ad5a4-943e-48d6-bf02-e09a3ab2c53e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9300db8a-fe2f-4044-b10e-51d4c4a592c3"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("197f8fe8-abf4-42ec-a337-87898dd79d5b"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a250f8f0-05de-408c-8115-4bf4cb315afd"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("b8e0f869-377a-45e9-b9d1-d84c97e0a2d8"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("cf9b7ddb-935d-4306-9917-e05945fcf7dc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("f41faa71-95d2-484c-b800-c27e2ac73648"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("13587099-6e91-4925-aa81-0b51b703cb08"));

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

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Birthday", "Country", "FirstName", "LastName" },
                values: new object[,]
                {
                    { new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), new DateTime(2007, 9, 17, 14, 58, 32, 235, DateTimeKind.Utc).AddTicks(7695), "Belarus", "name4", "LastName4" },
                    { new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), new DateTime(2004, 9, 17, 14, 58, 32, 235, DateTimeKind.Utc).AddTicks(7644), "Belarus", "name0", "LastName0" },
                    { new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), new DateTime(1932, 9, 17, 14, 58, 32, 235, DateTimeKind.Utc).AddTicks(7692), "Belarus", "name3", "LastName3" },
                    { new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), new DateTime(1982, 9, 17, 14, 58, 32, 235, DateTimeKind.Utc).AddTicks(7687), "Belarus", "name2", "LastName2" },
                    { new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), new DateTime(1980, 9, 17, 14, 58, 32, 235, DateTimeKind.Utc).AddTicks(7683), "Belarus", "name1", "LastName1" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "UserName" },
                values: new object[,]
                {
                    { new Guid("19fa892d-dbd4-4571-b39e-2f8b844b7ef1"), "19fa892d-dbd4-4571-b39e-2f8b844b7ef1@test.test", "AQAAAAIAAYagAAAAEHJDeZJaPgrRavkRvBJiqNTSr4czD9i4NGfXP8U4e+9xUihpAb2nhU3WG1IM9MlwtQ==", "user2" },
                    { new Guid("27e299b2-4162-43f1-ad0c-e18298271e13"), "admin@admin.com", "AQAAAAIAAYagAAAAEN3l37k8RgULVNBvRD3KvYbPRgS3dX8oYXOPPAVkO/Ej2abEX2c7f4YvVyoHF+1erg==", "admin" },
                    { new Guid("3d1874e0-142e-4f4e-8377-de6098937963"), "3d1874e0-142e-4f4e-8377-de6098937963@test.test", "AQAAAAIAAYagAAAAEPGjqPg6gNsHiyTO+fos8/zh9hJVf5TWRtUG01tKnbiIeTDDxZi7zzKG86zndvFEzA==", "user4" },
                    { new Guid("90be1435-66db-47a6-bb60-42fb6cfe41f9"), "90be1435-66db-47a6-bb60-42fb6cfe41f9@test.test", "AQAAAAIAAYagAAAAEJPkDUtEJ4go/Q7Ne9d5YpVFDBv+23dRpl/ax7oC+YLy73I/ICjX1TZG++tieQB5rA==", "user0" },
                    { new Guid("d623cbcf-2770-4942-865c-a559a8c8f9fb"), "d623cbcf-2770-4942-865c-a559a8c8f9fb@test.test", "AQAAAAIAAYagAAAAELEOhkn2sfldhX8eHFJu86JRXMDWtzo+azCvCnbbC8QhDa0V/jSKZjOnqC5n3wj/gw==", "user3" },
                    { new Guid("f6b0bb72-25f5-4ed5-832c-6aaf43f574b9"), "f6b0bb72-25f5-4ed5-832c-6aaf43f574b9@test.test", "AQAAAAIAAYagAAAAEAzvfUo9saKPQGEJsHM2cm1yX/JvvuGbOR50yQ9cCKUoDaD34TfkWXV4sv1GQQf9iA==", "user1" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "BookType", "ClientId", "Description", "Genre", "ISBN", "ReturnTo", "TakenAt", "Title" },
                values: new object[,]
                {
                    { new Guid("04533550-af02-406d-b4c9-ccb61acd4cf8"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "04533550-af02-406d-b4c9-ccb61acd4cf8", "4", "9793832285233", null, null, "04533550-af02-406d-b4c9-ccb61acd4cf8" },
                    { new Guid("04ea1062-1833-4f3f-bfc0-5e8d1fe98cdb"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "04ea1062-1833-4f3f-bfc0-5e8d1fe98cdb", "7", "9789655837193", null, null, "04ea1062-1833-4f3f-bfc0-5e8d1fe98cdb" },
                    { new Guid("056fd445-fa2c-480d-a50a-3b4a7edc5cf1"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "056fd445-fa2c-480d-a50a-3b4a7edc5cf1", "5", "9789241604291", null, null, "056fd445-fa2c-480d-a50a-3b4a7edc5cf1" },
                    { new Guid("0c082423-8389-406c-9ad7-e9af9cedada4"), new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), 0, null, "0c082423-8389-406c-9ad7-e9af9cedada4", "7", "9783522503242", null, null, "0c082423-8389-406c-9ad7-e9af9cedada4" },
                    { new Guid("1e4972d2-e6ca-491d-b81a-7e452a90d4fa"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "1e4972d2-e6ca-491d-b81a-7e452a90d4fa", "2", "9780414811805", null, null, "1e4972d2-e6ca-491d-b81a-7e452a90d4fa" },
                    { new Guid("20d8aa12-048f-4913-b1c4-2efb38bdc1b5"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "20d8aa12-048f-4913-b1c4-2efb38bdc1b5", "6", "9789911633033", null, null, "20d8aa12-048f-4913-b1c4-2efb38bdc1b5" },
                    { new Guid("227b862f-efb0-4f10-90c9-717c43026533"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "227b862f-efb0-4f10-90c9-717c43026533", "9", "9789252722687", null, null, "227b862f-efb0-4f10-90c9-717c43026533" },
                    { new Guid("22cac5d3-c71e-4bb3-b434-dc80561b3a46"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "22cac5d3-c71e-4bb3-b434-dc80561b3a46", "4", "9795061346402", null, null, "22cac5d3-c71e-4bb3-b434-dc80561b3a46" },
                    { new Guid("2870af98-345a-469a-8e2a-943ce7960b4c"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "2870af98-345a-469a-8e2a-943ce7960b4c", "0", "9781940747828", null, null, "2870af98-345a-469a-8e2a-943ce7960b4c" },
                    { new Guid("2ed3c307-e9c5-4f3c-9d0c-b04567feadd0"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "2ed3c307-e9c5-4f3c-9d0c-b04567feadd0", "1", "9780827689985", null, null, "2ed3c307-e9c5-4f3c-9d0c-b04567feadd0" },
                    { new Guid("342deb18-539c-4907-8e75-5003db2943d1"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "342deb18-539c-4907-8e75-5003db2943d1", "5", "9787889028820", null, null, "342deb18-539c-4907-8e75-5003db2943d1" },
                    { new Guid("3d9241ff-700f-4ea6-9d45-ebe975158758"), new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), 0, null, "3d9241ff-700f-4ea6-9d45-ebe975158758", "4", "9798722779007", null, null, "3d9241ff-700f-4ea6-9d45-ebe975158758" },
                    { new Guid("4144694d-f4dc-4f95-a3ed-353c85e3f9e2"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "4144694d-f4dc-4f95-a3ed-353c85e3f9e2", "6", "9798438508052", null, null, "4144694d-f4dc-4f95-a3ed-353c85e3f9e2" },
                    { new Guid("419fbcf7-8608-417c-b37c-bd2a4e8077be"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "419fbcf7-8608-417c-b37c-bd2a4e8077be", "3", "9799466597360", null, null, "419fbcf7-8608-417c-b37c-bd2a4e8077be" },
                    { new Guid("41c53d5a-bf3b-4324-a453-ba50960c88e0"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "41c53d5a-bf3b-4324-a453-ba50960c88e0", "1", "9795475512790", null, null, "41c53d5a-bf3b-4324-a453-ba50960c88e0" },
                    { new Guid("4253c174-5c9b-45d8-9e6b-b3ebd86a3f3b"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "4253c174-5c9b-45d8-9e6b-b3ebd86a3f3b", "0", "9791766847886", null, null, "4253c174-5c9b-45d8-9e6b-b3ebd86a3f3b" },
                    { new Guid("4aaf5b26-0683-4559-a747-fbd17620ae20"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "4aaf5b26-0683-4559-a747-fbd17620ae20", "6", "9789291737758", null, null, "4aaf5b26-0683-4559-a747-fbd17620ae20" },
                    { new Guid("4b24d71d-89ab-486a-8ffc-5425a6d46d5b"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "4b24d71d-89ab-486a-8ffc-5425a6d46d5b", "2", "9795259256063", null, null, "4b24d71d-89ab-486a-8ffc-5425a6d46d5b" },
                    { new Guid("554f61ed-224a-4440-9b35-5876a4d98315"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "554f61ed-224a-4440-9b35-5876a4d98315", "3", "9793346374287", null, null, "554f61ed-224a-4440-9b35-5876a4d98315" },
                    { new Guid("5c8a29c8-c29b-4a18-9fc2-10dce0345225"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "5c8a29c8-c29b-4a18-9fc2-10dce0345225", "8", "9792850527141", null, null, "5c8a29c8-c29b-4a18-9fc2-10dce0345225" },
                    { new Guid("5c8df265-039a-40bf-b539-6cbdf937cadf"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "5c8df265-039a-40bf-b539-6cbdf937cadf", "2", "9788627587814", null, null, "5c8df265-039a-40bf-b539-6cbdf937cadf" },
                    { new Guid("5cb938bc-2fb2-4b57-a283-0ece3f3d543e"), new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), 0, null, "5cb938bc-2fb2-4b57-a283-0ece3f3d543e", "1", "9794001341200", null, null, "5cb938bc-2fb2-4b57-a283-0ece3f3d543e" },
                    { new Guid("66a47e33-58db-4fb1-a451-1237ac4638d2"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "66a47e33-58db-4fb1-a451-1237ac4638d2", "1", "9784167055042", null, null, "66a47e33-58db-4fb1-a451-1237ac4638d2" },
                    { new Guid("72344f4f-1242-48e6-8450-f625ddcffb3c"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "72344f4f-1242-48e6-8450-f625ddcffb3c", "2", "9793256552867", null, null, "72344f4f-1242-48e6-8450-f625ddcffb3c" },
                    { new Guid("77b656cd-fa80-467d-a3a4-2fee9d82eadf"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "77b656cd-fa80-467d-a3a4-2fee9d82eadf", "0", "9797426435387", null, null, "77b656cd-fa80-467d-a3a4-2fee9d82eadf" },
                    { new Guid("792cae18-4537-4291-aeae-857bd6e51225"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "792cae18-4537-4291-aeae-857bd6e51225", "1", "9793494573631", null, null, "792cae18-4537-4291-aeae-857bd6e51225" },
                    { new Guid("7da07565-390f-402d-8e9c-740b03fa828f"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "7da07565-390f-402d-8e9c-740b03fa828f", "0", "9785184685748", null, null, "7da07565-390f-402d-8e9c-740b03fa828f" },
                    { new Guid("7deff246-dcc2-48c7-a18b-f769aa9854fa"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "7deff246-dcc2-48c7-a18b-f769aa9854fa", "6", "9787779733292", null, null, "7deff246-dcc2-48c7-a18b-f769aa9854fa" },
                    { new Guid("892767df-c7a4-4e15-a6d7-9c8422b10a14"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "892767df-c7a4-4e15-a6d7-9c8422b10a14", "7", "9798313077727", null, null, "892767df-c7a4-4e15-a6d7-9c8422b10a14" },
                    { new Guid("8c3d3f81-7d05-4983-a611-26bbfc35aaa4"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "8c3d3f81-7d05-4983-a611-26bbfc35aaa4", "8", "9784091062512", null, null, "8c3d3f81-7d05-4983-a611-26bbfc35aaa4" },
                    { new Guid("976ebbb9-f93b-44c4-856c-976015a35c2b"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "976ebbb9-f93b-44c4-856c-976015a35c2b", "0", "9783831600137", null, null, "976ebbb9-f93b-44c4-856c-976015a35c2b" },
                    { new Guid("9c758c56-e286-47cd-9b79-a2c85e1ef25d"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "9c758c56-e286-47cd-9b79-a2c85e1ef25d", "5", "9783649230465", null, null, "9c758c56-e286-47cd-9b79-a2c85e1ef25d" },
                    { new Guid("a25ad456-345d-4426-8caa-97f6dd487264"), new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), 0, null, "a25ad456-345d-4426-8caa-97f6dd487264", "3", "9795203179615", null, null, "a25ad456-345d-4426-8caa-97f6dd487264" },
                    { new Guid("a9a4f485-1519-4813-8a1c-8b76d39e0200"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "a9a4f485-1519-4813-8a1c-8b76d39e0200", "4", "9782769516190", null, null, "a9a4f485-1519-4813-8a1c-8b76d39e0200" },
                    { new Guid("aba2d10c-290e-4873-8db8-d6f68b6e8efc"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "aba2d10c-290e-4873-8db8-d6f68b6e8efc", "2", "9785126421724", null, null, "aba2d10c-290e-4873-8db8-d6f68b6e8efc" },
                    { new Guid("ae3c2891-5ff2-4a46-b53d-4031ee978147"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "ae3c2891-5ff2-4a46-b53d-4031ee978147", "5", "9780881160970", null, null, "ae3c2891-5ff2-4a46-b53d-4031ee978147" },
                    { new Guid("aefae8d0-503d-499f-8b86-aabb9ed30c7c"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "aefae8d0-503d-499f-8b86-aabb9ed30c7c", "3", "9793609845103", null, null, "aefae8d0-503d-499f-8b86-aabb9ed30c7c" },
                    { new Guid("b0658308-fffb-4961-9e89-3495a8cbfe04"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "b0658308-fffb-4961-9e89-3495a8cbfe04", "9", "9781060496927", null, null, "b0658308-fffb-4961-9e89-3495a8cbfe04" },
                    { new Guid("b61d1ed3-781d-4e86-be72-9de09a888818"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "b61d1ed3-781d-4e86-be72-9de09a888818", "7", "9792849866985", null, null, "b61d1ed3-781d-4e86-be72-9de09a888818" },
                    { new Guid("b8380f49-30f1-4c07-89a3-47f1c6a0109d"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "b8380f49-30f1-4c07-89a3-47f1c6a0109d", "7", "9785551218463", null, null, "b8380f49-30f1-4c07-89a3-47f1c6a0109d" },
                    { new Guid("b969f209-fdb7-4bdd-9853-96457cf51751"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "b969f209-fdb7-4bdd-9853-96457cf51751", "8", "9792441084725", null, null, "b969f209-fdb7-4bdd-9853-96457cf51751" },
                    { new Guid("bb09db73-1302-442a-8372-4fbbde185170"), new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"), 0, null, "bb09db73-1302-442a-8372-4fbbde185170", "9", "9792209970734", null, null, "bb09db73-1302-442a-8372-4fbbde185170" },
                    { new Guid("bea7c631-5baa-4686-8add-346f03d475f7"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "bea7c631-5baa-4686-8add-346f03d475f7", "9", "9795595889598", null, null, "bea7c631-5baa-4686-8add-346f03d475f7" },
                    { new Guid("c5b360e0-953d-46b8-ac86-e9d93d9bc67d"), new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"), 0, null, "c5b360e0-953d-46b8-ac86-e9d93d9bc67d", "9", "9786578373166", null, null, "c5b360e0-953d-46b8-ac86-e9d93d9bc67d" },
                    { new Guid("d8661c99-7162-4090-bfbc-c4b7cdf331f6"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "d8661c99-7162-4090-bfbc-c4b7cdf331f6", "3", "9790451765092", null, null, "d8661c99-7162-4090-bfbc-c4b7cdf331f6" },
                    { new Guid("d97dec9b-f813-4832-9943-6d5a4d78dddb"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "d97dec9b-f813-4832-9943-6d5a4d78dddb", "4", "9786671108986", null, null, "d97dec9b-f813-4832-9943-6d5a4d78dddb" },
                    { new Guid("e16a06b6-ead4-4c42-9cdf-d306d88984c6"), new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"), 0, null, "e16a06b6-ead4-4c42-9cdf-d306d88984c6", "8", "9795932726593", null, null, "e16a06b6-ead4-4c42-9cdf-d306d88984c6" },
                    { new Guid("ebabba53-5279-4973-9b36-a21d8fbd770b"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "ebabba53-5279-4973-9b36-a21d8fbd770b", "8", "9798279484164", null, null, "ebabba53-5279-4973-9b36-a21d8fbd770b" },
                    { new Guid("f01710b2-80a1-43ef-850e-641af06f334c"), new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"), 0, null, "f01710b2-80a1-43ef-850e-641af06f334c", "5", "9793015510442", null, null, "f01710b2-80a1-43ef-850e-641af06f334c" },
                    { new Guid("f4fa4033-4348-4318-ae73-f8b5cfb34a02"), new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"), 0, null, "f4fa4033-4348-4318-ae73-f8b5cfb34a02", "6", "9788670112605", null, null, "f4fa4033-4348-4318-ae73-f8b5cfb34a02" }
                });

            migrationBuilder.InsertData(
                table: "UserAccessGroup",
                columns: new[] { "GroupId", "UserId" },
                values: new object[,]
                {
                    { 1, new Guid("27e299b2-4162-43f1-ad0c-e18298271e13") },
                    { 2, new Guid("19fa892d-dbd4-4571-b39e-2f8b844b7ef1") },
                    { 2, new Guid("27e299b2-4162-43f1-ad0c-e18298271e13") },
                    { 2, new Guid("3d1874e0-142e-4f4e-8377-de6098937963") },
                    { 2, new Guid("90be1435-66db-47a6-bb60-42fb6cfe41f9") },
                    { 2, new Guid("d623cbcf-2770-4942-865c-a559a8c8f9fb") },
                    { 2, new Guid("f6b0bb72-25f5-4ed5-832c-6aaf43f574b9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("04533550-af02-406d-b4c9-ccb61acd4cf8"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("04ea1062-1833-4f3f-bfc0-5e8d1fe98cdb"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("056fd445-fa2c-480d-a50a-3b4a7edc5cf1"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("0c082423-8389-406c-9ad7-e9af9cedada4"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("1e4972d2-e6ca-491d-b81a-7e452a90d4fa"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("20d8aa12-048f-4913-b1c4-2efb38bdc1b5"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("227b862f-efb0-4f10-90c9-717c43026533"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("22cac5d3-c71e-4bb3-b434-dc80561b3a46"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2870af98-345a-469a-8e2a-943ce7960b4c"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("2ed3c307-e9c5-4f3c-9d0c-b04567feadd0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("342deb18-539c-4907-8e75-5003db2943d1"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("3d9241ff-700f-4ea6-9d45-ebe975158758"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("4144694d-f4dc-4f95-a3ed-353c85e3f9e2"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("419fbcf7-8608-417c-b37c-bd2a4e8077be"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("41c53d5a-bf3b-4324-a453-ba50960c88e0"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("4253c174-5c9b-45d8-9e6b-b3ebd86a3f3b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("4aaf5b26-0683-4559-a747-fbd17620ae20"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("4b24d71d-89ab-486a-8ffc-5425a6d46d5b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("554f61ed-224a-4440-9b35-5876a4d98315"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5c8a29c8-c29b-4a18-9fc2-10dce0345225"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5c8df265-039a-40bf-b539-6cbdf937cadf"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("5cb938bc-2fb2-4b57-a283-0ece3f3d543e"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("66a47e33-58db-4fb1-a451-1237ac4638d2"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("72344f4f-1242-48e6-8450-f625ddcffb3c"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("77b656cd-fa80-467d-a3a4-2fee9d82eadf"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("792cae18-4537-4291-aeae-857bd6e51225"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7da07565-390f-402d-8e9c-740b03fa828f"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("7deff246-dcc2-48c7-a18b-f769aa9854fa"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("892767df-c7a4-4e15-a6d7-9c8422b10a14"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("8c3d3f81-7d05-4983-a611-26bbfc35aaa4"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("976ebbb9-f93b-44c4-856c-976015a35c2b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("9c758c56-e286-47cd-9b79-a2c85e1ef25d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("a25ad456-345d-4426-8caa-97f6dd487264"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("a9a4f485-1519-4813-8a1c-8b76d39e0200"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("aba2d10c-290e-4873-8db8-d6f68b6e8efc"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ae3c2891-5ff2-4a46-b53d-4031ee978147"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("aefae8d0-503d-499f-8b86-aabb9ed30c7c"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b0658308-fffb-4961-9e89-3495a8cbfe04"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b61d1ed3-781d-4e86-be72-9de09a888818"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b8380f49-30f1-4c07-89a3-47f1c6a0109d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("b969f209-fdb7-4bdd-9853-96457cf51751"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("bb09db73-1302-442a-8372-4fbbde185170"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("bea7c631-5baa-4686-8add-346f03d475f7"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("c5b360e0-953d-46b8-ac86-e9d93d9bc67d"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d8661c99-7162-4090-bfbc-c4b7cdf331f6"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("d97dec9b-f813-4832-9943-6d5a4d78dddb"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("e16a06b6-ead4-4c42-9cdf-d306d88984c6"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("ebabba53-5279-4973-9b36-a21d8fbd770b"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("f01710b2-80a1-43ef-850e-641af06f334c"));

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: new Guid("f4fa4033-4348-4318-ae73-f8b5cfb34a02"));

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 1, new Guid("27e299b2-4162-43f1-ad0c-e18298271e13") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("19fa892d-dbd4-4571-b39e-2f8b844b7ef1") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("27e299b2-4162-43f1-ad0c-e18298271e13") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("3d1874e0-142e-4f4e-8377-de6098937963") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("90be1435-66db-47a6-bb60-42fb6cfe41f9") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("d623cbcf-2770-4942-865c-a559a8c8f9fb") });

            migrationBuilder.DeleteData(
                table: "UserAccessGroup",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 2, new Guid("f6b0bb72-25f5-4ed5-832c-6aaf43f574b9") });

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("399bc81f-b2cb-45d3-a570-fe21db2d7db2"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("4095ad9d-4d25-496c-bd30-09b0754b00fc"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("42539d0c-0d8a-4c90-be9c-54c95a6a33e5"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("7d572d8e-05cd-4b31-87ee-e4af0951f754"));

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: new Guid("a03aed0f-1721-4232-96c5-fd05d3cfbbd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("19fa892d-dbd4-4571-b39e-2f8b844b7ef1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("27e299b2-4162-43f1-ad0c-e18298271e13"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3d1874e0-142e-4f4e-8377-de6098937963"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("90be1435-66db-47a6-bb60-42fb6cfe41f9"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d623cbcf-2770-4942-865c-a559a8c8f9fb"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6b0bb72-25f5-4ed5-832c-6aaf43f574b9"));

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
        }
    }
}
