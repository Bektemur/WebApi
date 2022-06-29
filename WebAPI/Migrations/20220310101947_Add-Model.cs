using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class AddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telephone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                });

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerTelephone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerLineId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "TypeProperties",
                columns: table => new
                {
                    TypePropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeProperties", x => x.TypePropertyId);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_Province_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId");
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalPropertyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishingState = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionThai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ForRent = table.Column<bool>(type: "bit", nullable: false),
                    ForSale = table.Column<bool>(type: "bit", nullable: false),
                    PriceSale = table.Column<double>(type: "float", nullable: false),
                    PriceSaleSqm = table.Column<double>(type: "float", nullable: false),
                    PriceRent = table.Column<double>(type: "float", nullable: false),
                    LivingArea = table.Column<double>(type: "float", nullable: false),
                    LandArea = table.Column<double>(type: "float", nullable: false),
                    Bathrooms = table.Column<int>(type: "int", nullable: false),
                    Bedrooms = table.Column<int>(type: "int", nullable: false),
                    Parking = table.Column<int>(type: "int", nullable: false),
                    PublicDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TypePropertyId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StationId = table.Column<int>(type: "int", nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    HouseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubDistrict = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyId);
                    table.ForeignKey(
                        name: "FK_Properties_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "ContactId");
                    table.ForeignKey(
                        name: "FK_Properties_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_Stations_StationId",
                        column: x => x.StationId,
                        principalTable: "Stations",
                        principalColumn: "StationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Properties_TypeProperties_TypePropertyId",
                        column: x => x.TypePropertyId,
                        principalTable: "TypeProperties",
                        principalColumn: "TypePropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Callbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Callbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Callbacks_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FilesOnFileSystem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilesOnFileSystem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilesOnFileSystem_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Improvements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Improvements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Improvements_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId");
                });

            migrationBuilder.CreateTable(
                name: "ImprovementToProperty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyId = table.Column<int>(type: "int", nullable: false),
                    ImprovementId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImprovementToProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImprovementToProperty_Improvements_ImprovementId",
                        column: x => x.ImprovementId,
                        principalTable: "Improvements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImprovementToProperty_Properties_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Properties",
                        principalColumn: "PropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Improvements",
                columns: new[] { "Id", "Name", "PropertyId", "Type" },
                values: new object[,]
                {
                    { 1, "Parking", null, 2 },
                    { 2, "TV", null, 2 },
                    { 3, "Refrigerator", null, 2 },
                    { 4, "Washing Machine", null, 2 },
                    { 5, "Kitchen", null, 2 },
                    { 6, "Balcony", null, 2 },
                    { 7, "Internet", null, 2 },
                    { 8, "Drying Machine", null, 2 },
                    { 9, "Private garden", null, 2 },
                    { 10, "Roof Terrace", null, 2 },
                    { 11, "Open kitchen", null, 2 },
                    { 12, "Cooker Hob & Hood", null, 2 },
                    { 13, "Closed kitchen", null, 2 },
                    { 14, "Water Heater", null, 2 },
                    { 15, "Common garden", null, 5 },
                    { 16, "Function Room", null, 5 },
                    { 17, "Common jacuzzi", null, 5 },
                    { 18, "Lounge", null, 5 },
                    { 19, "Restaurant", null, 5 },
                    { 20, "Building security", null, 5 },
                    { 21, "Garage", null, 5 },
                    { 22, "Onsen Spa", null, 5 },
                    { 23, "Sauna", null, 5 },
                    { 24, "Cafe", null, 5 },
                    { 25, "Gym/Fitness", null, 5 },
                    { 26, "Library", null, 5 },
                    { 27, "Outdoor swimming pool", null, 5 },
                    { 28, "Spa", null, 5 },
                    { 29, "Children playroom", null, 5 },
                    { 30, "Indoor swimming pool", null, 5 },
                    { 31, "Lift", null, 5 },
                    { 32, "Playground", null, 5 },
                    { 33, "Steamroom", null, 5 },
                    { 34, "European", null, 3 },
                    { 35, "Industrial", null, 3 },
                    { 36, "Minimalistic", null, 3 },
                    { 37, "Modern", null, 3 },
                    { 38, "Thai", null, 3 },
                    { 39, "Not Decorated", null, 3 },
                    { 40, "Air Conditioning", null, 4 },
                    { 41, "Bathtub", null, 4 },
                    { 42, "Oven", null, 4 }
                });

            migrationBuilder.InsertData(
                table: "Improvements",
                columns: new[] { "Id", "Name", "PropertyId", "Type" },
                values: new object[,]
                {
                    { 43, "Cooking gas", null, 4 },
                    { 44, "Private Pool", null, 4 },
                    { 45, "Fan", null, 4 },
                    { 46, "Stove", null, 4 },
                    { 47, "Goverment electricity meter", null, 7 },
                    { 48, "Shopping Mall", null, 7 },
                    { 49, "Public water meter", null, 7 },
                    { 50, "Golf Simulator", null, 7 },
                    { 51, "Co-Working Space", null, 7 },
                    { 52, "Cleaning service", null, 7 },
                    { 53, "Mini Market", null, 7 },
                    { 55, "Wheelchair accessible", null, 6 },
                    { 56, "Badminton Court", null, 6 },
                    { 57, "Company Registration", null, 6 },
                    { 58, "High Floor", null, 6 },
                    { 59, "Low Floor", null, 6 },
                    { 60, "Scenic View", null, 6 },
                    { 61, "BBQ Area", null, 6 },
                    { 62, "New Project", null, 6 },
                    { 63, "Pool View", null, 6 },
                    { 64, "Sea View", null, 6 },
                    { 65, "Allows Pets", null, 6 },
                    { 66, "Shuttle service", null, 6 },
                    { 67, "Jogging Track", null, 6 },
                    { 68, "Luxury", null, 6 },
                    { 69, "Park View", null, 6 },
                    { 70, "Rent Guarantee", null, 6 },
                    { 71, "Allow Short-term", null, 6 },
                    { 72, "City View", null, 6 },
                    { 73, "Lake View", null, 6 },
                    { 74, "Maid's Room", null, 6 },
                    { 75, "Penthouse", null, 6 },
                    { 76, "Partially Furnished", null, 1 },
                    { 77, "Fully Furnished", null, 1 },
                    { 78, "Needs renovation", null, 1 },
                    { 79, "Renovated", null, 1 },
                    { 80, "To be renovated", null, 1 },
                    { 81, "Unfurnished", null, 1 },
                    { 82, "Jacuzzi", null, 1 },
                    { 83, "Walk-in-Wardrobe", null, 1 },
                    { 84, "Maid's Room", null, 1 },
                    { 85, "Tennis Court", null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "Name", "StationType" },
                values: new object[,]
                {
                    { 1, "National Stadium", 0 },
                    { 2, "Siam", 0 },
                    { 3, "Ratchadamri", 0 },
                    { 4, "Sala Daeng", 0 },
                    { 5, "Surasak", 0 },
                    { 6, "Saphan Taksin", 0 },
                    { 7, "Krung Thon Buri", 0 },
                    { 8, "Wongwian Yai", 0 },
                    { 9, "Pho Nimit", 0 },
                    { 10, "Talat Phlu", 0 },
                    { 11, "Wutthakat", 0 },
                    { 12, "Bang Wa", 0 },
                    { 13, "Khu Khot", 1 },
                    { 14, "Yaek Kor Por Aor", 1 },
                    { 15, "Royal Thai Air Force Museum", 1 },
                    { 16, "Bhumibol Adulyadej Hospital", 1 },
                    { 17, "Saphan Mai", 1 },
                    { 18, "Sai Yud", 1 },
                    { 19, "Phahon Yothin 59", 1 },
                    { 20, "Wat Phra Sri Mahathat", 1 },
                    { 21, "11th Infantry Regiment", 1 },
                    { 22, "Bang Bua", 1 },
                    { 23, "Royal Forest Department", 1 },
                    { 24, "Kasetsart University", 1 },
                    { 25, "Sena Nikhom", 1 },
                    { 26, "Ratchayothin", 1 },
                    { 27, "Phahon Yothin 24", 1 },
                    { 28, "Ha Yaek Lat Phrao", 1 },
                    { 29, "Mo Chit", 1 },
                    { 30, "Saphan Kwai", 1 },
                    { 31, "Ari", 1 },
                    { 32, "Sanam Pao", 1 },
                    { 33, "Victory Monument", 1 },
                    { 34, "Phaya Thai", 1 },
                    { 35, "Ratchathewi", 1 },
                    { 36, "Chit Lom", 1 },
                    { 37, "Ploen Chit", 1 },
                    { 38, "Nana", 1 },
                    { 39, "Asok", 1 },
                    { 40, "Phrom Phong", 1 },
                    { 41, "Thong Lo", 1 },
                    { 42, "Ekkamai", 1 }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "Name", "StationType" },
                values: new object[,]
                {
                    { 43, "Phra Khanong", 1 },
                    { 44, "On Nut", 1 },
                    { 45, "Bang Chak", 1 },
                    { 46, "Punnawithi", 1 },
                    { 47, "Udom Suk", 1 },
                    { 48, "Bang Na", 1 },
                    { 49, "Bearing", 1 },
                    { 50, "Samrong", 1 },
                    { 51, "Pu Chao", 1 },
                    { 52, "Chang Erawan", 1 },
                    { 53, "Royal Thai Naval Academy", 1 },
                    { 54, "Pak Nam", 1 },
                    { 55, "Srinagarindra", 1 },
                    { 56, "Phraek Sa", 1 },
                    { 57, "Sai Luat", 1 },
                    { 58, "Kheha", 1 },
                    { 59, "Hua Lamphong", 2 },
                    { 60, "Sam Yan", 2 },
                    { 61, "Si Lom", 2 },
                    { 62, "Lumphini", 2 },
                    { 63, "Khlong Toei", 2 },
                    { 64, "Queen Sirikit National Convention Centre", 2 },
                    { 65, "Sukhumvit", 2 },
                    { 66, "Phetchaburi", 2 },
                    { 67, "Phra Ram 9", 2 },
                    { 68, "Thailand Cultural Centre", 2 },
                    { 69, "Huai Khwang", 2 },
                    { 70, "Sutthisan", 2 },
                    { 71, "Ratchadaphisek", 2 },
                    { 72, "Lat Phrao", 2 },
                    { 73, "Phahon Yothin", 2 },
                    { 75, "Mo Chit", 2 },
                    { 76, "Kamphaeng Phet", 2 },
                    { 77, "Bang Sue", 2 },
                    { 78, "Hua Lamphong", 2 },
                    { 79, "Tao Poon ", 2 },
                    { 80, "Tha Phra", 2 },
                    { 81, "Charan 13", 2 },
                    { 82, "Fai Chai", 2 },
                    { 83, "Bang Khun Non", 2 },
                    { 84, "Bang Yi Khan", 2 },
                    { 85, "Sirindhorn", 2 }
                });

            migrationBuilder.InsertData(
                table: "Stations",
                columns: new[] { "StationId", "Name", "StationType" },
                values: new object[,]
                {
                    { 86, "Bang Phlat", 2 },
                    { 87, "Bang O", 2 },
                    { 88, "Bang Pho", 2 },
                    { 89, "Wat Mangkon", 2 },
                    { 90, "Sam Yot", 2 },
                    { 91, "Sanam Chai", 2 },
                    { 92, "Itsaraphap", 2 },
                    { 93, "Tha Phra", 2 },
                    { 94, "Bang Phai", 2 },
                    { 95, "Bang Wa", 2 },
                    { 96, "Phetkasem 48", 2 },
                    { 97, "Phasi Charoen", 2 },
                    { 98, "Bang Khae", 2 },
                    { 99, "Lak Song", 2 },
                    { 100, "Khlong Bang Phai", 2 },
                    { 101, "Talad Bang Yai", 2 },
                    { 102, "Sam Yaek Bang Yai", 2 },
                    { 103, "Bang Phlu", 2 },
                    { 104, "Bang Rak Yai", 2 },
                    { 105, "Bang Rak Noi Tha It", 2 },
                    { 106, "Sai Ma", 2 },
                    { 107, "Phra Nangklao Bridge", 2 },
                    { 108, "Yaek Nonthaburi 1", 2 },
                    { 109, "Bang Krasor", 2 },
                    { 110, "Nonthaburi Civic Centre", 2 },
                    { 111, "Ministry Of Public Health", 2 },
                    { 112, "Yaek Tiwanon", 2 },
                    { 113, "Wong Sawang", 2 },
                    { 114, "Bang Son", 2 }
                });

            migrationBuilder.InsertData(
                table: "TypeProperties",
                columns: new[] { "TypePropertyId", "Name" },
                values: new object[,]
                {
                    { 1, "Unspecified" },
                    { 2, "Townhouse" },
                    { 3, "House" },
                    { 4, "Condominium" },
                    { 5, "Appartment" },
                    { 6, "Office" },
                    { 7, "Land" },
                    { 8, "Penthouse" },
                    { 9, "Serviced Apartment" },
                    { 10, "Shop house" },
                    { 11, "Retail" },
                    { 12, "Business" },
                    { 13, "Factory" }
                });

            migrationBuilder.InsertData(
                table: "TypeProperties",
                columns: new[] { "TypePropertyId", "Name" },
                values: new object[] { 14, "Commercial Building" });

            migrationBuilder.InsertData(
                table: "TypeProperties",
                columns: new[] { "TypePropertyId", "Name" },
                values: new object[] { 15, "Hotel / Resort" });

            migrationBuilder.InsertData(
                table: "TypeProperties",
                columns: new[] { "TypePropertyId", "Name" },
                values: new object[] { 16, "Other Commertcial" });

            migrationBuilder.CreateIndex(
                name: "IX_Callbacks_PropertyId",
                table: "Callbacks",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_FilesOnFileSystem_PropertyId",
                table: "FilesOnFileSystem",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Improvements_PropertyId",
                table: "Improvements",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementToProperty_ImprovementId",
                table: "ImprovementToProperty",
                column: "ImprovementId");

            migrationBuilder.CreateIndex(
                name: "IX_ImprovementToProperty_PropertyId",
                table: "ImprovementToProperty",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ApplicationUserId",
                table: "Properties",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CityId",
                table: "Properties",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CompanyId",
                table: "Properties",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactId",
                table: "Properties",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_OwnerId",
                table: "Properties",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ProjectId",
                table: "Properties",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_StationId",
                table: "Properties",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_TypePropertyId",
                table: "Properties",
                column: "TypePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Province_StationId",
                table: "Province",
                column: "StationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Callbacks");

            migrationBuilder.DropTable(
                name: "FilesOnFileSystem");

            migrationBuilder.DropTable(
                name: "ImprovementToProperty");

            migrationBuilder.DropTable(
                name: "Improvements");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "TypeProperties");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.DropTable(
                name: "Stations");
        }
    }
}
