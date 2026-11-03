using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hizhar.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mountains",
                columns: new[] { "Id", "Description", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, "Най-високата планина на Балканския полуостров с връх Мусала (2925м), над 180 ледникови езера и едно от най-посещаваните хижи в България.", null, "Рила" },
                    { 2, "Алпийска планина с мраморни и гранитни върхове, сред които Вихрен (2914м) и Кутело. Голяма част от нея е национален парк и обект на ЮНЕСКО.", null, "Пирин" },
                    { 3, "Най-дългата планина в България, която пресича страната от запад на изток. Най-висок е връх Ботев (2376 м), а под него е Райското пръскало.", null, "Стара планина" },
                    { 4, "Планината до София – лесно достъпна, с много маршрути за начинаещи и най-висок Черни връх (2290 м).", null, "Витоша" },
                    { 5, "Най-обширната планина в България – заоблени била, иглолистни гори, пещери и езера. Най-висок е Голям Перелик (2191 м).", null, "Родопи" }
                });

            migrationBuilder.InsertData(
                table: "Huts",
                columns: new[] { "Id", "Altitude", "BedsCount", "Description", "HasElectricity", "HasFood", "HasWater", "ImageUrl", "IsDeleted", "Latitude", "Longitude", "MountainId", "Name", "OwnerId", "PhoneNumber", "PricePerNight" },
                values: new object[,]
                {
                    { 1, 2389, 90, "Най-високо разположената хижа в Рила, на брега на Мусаленските езера. Изходна точка за изкачване на връх Мусала.", true, true, true, null, false, 42.193100000000001, 23.583600000000001, 1, "Хижа „Мусала“", null, "+359 888 000 001", 30m },
                    { 2, 2000, 80, "Легендарна хижа в Северозападна Рила, център на българския алпинизъм. Оттук тръгват маршрути към Мальовица и Седемте езера.", true, true, true, null, false, 42.189999999999998, 23.393000000000001, 1, "Хижа „Мальовица“", null, "+359 888 000 002", 35m },
                    { 3, 1876, 40, "Първата хижа в България (1922 г.), до едноименния водопад. Уютна и спокойна, подходяща за семейства.", false, true, true, null, false, 42.238, 23.332999999999998, 1, "Хижа „Скакавица“", null, null, 25m },
                    { 4, 1950, 110, "Основната изходна точка за връх Вихрен. Намира се над Банско, до нея се стига с автомобил.", true, true, true, null, false, 41.765999999999998, 23.416, 2, "Хижа „Вихрен“", null, "+359 888 000 004", 35m },
                    { 5, 1810, 60, "Хижа в долината на река Бъндерица, близо до Байкушевата мура – най-старото дърво в България.", true, true, true, null, false, 41.779000000000003, 23.43, 2, "Хижа „Бъндерица“", null, null, 30m },
                    { 6, 1520, 60, "Хижа под Райското пръскало – най-високия водопад на Балканите. Изходна точка към връх Ботев.", false, true, true, null, false, 42.725000000000001, 24.91, 3, "Хижа „Рай“", null, null, 25m },
                    { 7, 1550, 70, "Хижа в Национален парк „Централен Балкан“ под връх Амбарица, на прехода Ком – Емине.", true, false, true, null, false, 42.756999999999998, 24.780000000000001, 3, "Хижа „Амбарица“", null, null, 25m },
                    { 8, 1810, 50, "Най-популярната хижа на Витоша, до която има асфалтов път. Идеална за кратки разходки и ски.", true, true, true, null, false, 42.581000000000003, 23.292999999999999, 4, "Хижа „Алеко“", null, "+359 888 000 008", 40m },
                    { 9, 1500, 40, "Хижа сред Смолянските езера в Родопите – спокойни гористи маршрути и красиви гледки.", true, true, true, null, false, 41.615000000000002, 24.675000000000001, 5, "Хижа „Смолянски езера“", null, null, 25m }
                });

            migrationBuilder.InsertData(
                table: "Peaks",
                columns: new[] { "Id", "Description", "Elevation", "IsNationalTouristSite", "Latitude", "Longitude", "MountainId", "Name" },
                values: new object[,]
                {
                    { 1, "Най-високият връх на България и на Балканския полуостров.", 2925, true, 42.179499999999997, 23.5852, 1, "Мусала" },
                    { 2, "Вторият по височина връх в Рила, близо до Мусала.", 2852, false, 42.171999999999997, 23.571000000000002, 1, "Иречек" },
                    { 3, "Емблематичен връх в Северозападна Рила с впечатляваща северна стена.", 2729, true, 42.1753, 23.370000000000001, 1, "Мальовица" },
                    { 4, "Най-високият връх на Пирин – мраморна пирамида над Банско.", 2914, true, 41.767200000000003, 23.3994, 2, "Вихрен" },
                    { 5, "Връх до Вихрен, свързан с него чрез ръба Кончето.", 2908, false, 41.7744, 23.396599999999999, 2, "Кутело" },
                    { 6, "Връх над Банско с лесен достъп от ски зоната.", 2746, false, 41.7956, 23.441700000000001, 2, "Тодорка" },
                    { 7, "Най-високият връх на Стара планина.", 2376, true, 42.717199999999998, 24.917200000000001, 3, "Ботев" },
                    { 8, "Началото на прехода Ком – Емине по билото на Стара планина.", 2016, false, 43.174700000000001, 23.052199999999999, 3, "Ком" },
                    { 9, "Най-високата точка на Витоша с метеорологична станция.", 2290, true, 42.563600000000001, 23.278300000000002, 4, "Черни връх" },
                    { 10, "Най-високият връх на Родопите.", 2191, true, 41.604500000000002, 24.5717, 5, "Голям Перелик" }
                });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "Description", "Difficulty", "DurationMinutes", "ElevationGain", "LengthKm", "MountainId", "Name", "Season" },
                values: new object[,]
                {
                    { 1, "Класически маршрут от курорта Боровец през гората и покрай Бистрица до хижа „Мусала“. Алтернатива е лифтът „Ярема“.", 2, 270, 1050, 10.0, 1, "Боровец – хижа „Мусала“", 3 },
                    { 2, "Добре маркирана пътека покрай Леденото езеро и заслона до най-високата точка на Балканите.", 2, 120, 540, 3.2000000000000002, 1, "Хижа „Мусала“ – връх Мусала", 2 },
                    { 3, "Маршрут през Еленино езеро и Мальовишкия циркус. Последната част е стръмна и камениста.", 3, 210, 730, 6.5, 1, "Хижа „Мальовица“ – връх Мальовица", 2 },
                    { 4, "Стръмно изкачване през седловината Кабата до мраморния връх. Зимно време е подходящ само за опитни.", 3, 210, 965, 4.5, 2, "Хижа „Вихрен“ – връх Вихрен", 2 },
                    { 5, "Най-лесният път до Черни връх – широка пътека по склона, подходяща и за деца.", 1, 90, 480, 4.0, 4, "Хижа „Алеко“ – Черни връх", 1 },
                    { 6, "Маршрут покрай Райското пръскало и по стръмната пътека през Джендема до връх Ботев.", 3, 240, 860, 7.0, 3, "Хижа „Рай“ – връх Ботев", 2 }
                });

            migrationBuilder.InsertData(
                table: "TrailPoints",
                columns: new[] { "Id", "Latitude", "Longitude", "Name", "Order", "PointType", "TrailId" },
                values: new object[,]
                {
                    { 1, 42.265999999999998, 23.606000000000002, "Боровец", 1, 1, 1 },
                    { 2, 42.213000000000001, 23.585000000000001, "Горна станция Ярема", 2, 4, 1 },
                    { 3, 42.193100000000001, 23.583600000000001, "Хижа „Мусала“", 3, 2, 1 },
                    { 4, 42.193100000000001, 23.583600000000001, "Хижа „Мусала“", 1, 2, 2 },
                    { 5, 42.182499999999997, 23.583500000000001, "Заслон „Леденото езеро“", 2, 4, 2 },
                    { 6, 42.179499999999997, 23.5852, "Връх Мусала", 3, 3, 2 },
                    { 7, 42.189999999999998, 23.393000000000001, "Хижа „Мальовица“", 1, 2, 3 },
                    { 8, 42.182000000000002, 23.382000000000001, "Еленино езеро", 2, 4, 3 },
                    { 9, 42.1753, 23.370000000000001, "Връх Мальовица", 3, 3, 3 },
                    { 10, 41.765999999999998, 23.416, "Хижа „Вихрен“", 1, 2, 4 },
                    { 11, 41.770800000000001, 23.406400000000001, "Седловина Кабата", 2, 4, 4 },
                    { 12, 41.767200000000003, 23.3994, "Връх Вихрен", 3, 3, 4 },
                    { 13, 42.581000000000003, 23.292999999999999, "Хижа „Алеко“", 1, 2, 5 },
                    { 14, 42.563600000000001, 23.278300000000002, "Черни връх", 2, 3, 5 },
                    { 15, 42.725000000000001, 24.91, "Хижа „Рай“", 1, 2, 6 },
                    { 16, 42.720999999999997, 24.913, "Райското пръскало", 2, 4, 6 },
                    { 17, 42.717199999999998, 24.917200000000001, "Връх Ботев", 3, 3, 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Huts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Peaks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TrailPoints",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Mountains",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Mountains",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mountains",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mountains",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Mountains",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
