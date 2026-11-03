using Hizhar.Data.Models;
using Hizhar.Data.Models.Enums;

namespace Hizhar.Data.Seeding
{
    public static class SeedData
    {
        public static IEnumerable<Mountain> GetMountains()
        {
            return new List<Mountain>
            {
                new Mountain
                {
                    Id = 1,
                    Name = "Рила",
                    Description = "Най-високата планина на Балканския полуостров с връх Мусала (2925м), над 180 ледникови езера и едно от най-посещаваните хижи в България."
                },
                new Mountain
                {
                    Id = 2,
                    Name = "Пирин",
                    Description = "Алпийска планина с мраморни и гранитни върхове, сред които Вихрен (2914м) и Кутело. Голяма част от нея е национален парк и обект на ЮНЕСКО."
                },
                new Mountain
                {
                    Id = 3,
                    Name = "Стара планина",
                    Description = "Най-дългата планина в България, която пресича страната от запад на изток. Най-висок е връх Ботев (2376 м), а под него е Райското пръскало."
                },
                new Mountain
                {
                    Id = 4,
                    Name = "Витоша",
                    Description = "Планината до София – лесно достъпна, с много маршрути за начинаещи и най-висок Черни връх (2290 м)."
                },
                new Mountain
                {
                    Id = 5,
                    Name = "Родопи",
                    Description = "Най-обширната планина в България – заоблени била, иглолистни гори, пещери и езера. Най-висок е Голям Перелик (2191 м)."
                }
            };
        }
        public static IEnumerable<Hut> GetHuts()
        {
            return new List<Hut>
            {
                new Hut
                {
                    Id = 1, MountainId = 1, Name = "Хижа „Мусала“", Altitude = 2389,
                    Latitude = 42.1931, Longitude = 23.5836, BedsCount = 90, PricePerNight = 30m,
                    HasWater = true, HasElectricity = true, HasFood = true, PhoneNumber = "+359 888 000 001",
                    Description = "Най-високо разположената хижа в Рила, на брега на Мусаленските езера. Изходна точка за изкачване на връх Мусала."
                },
                new Hut
                {
                    Id = 2, MountainId = 1, Name = "Хижа „Мальовица“", Altitude = 2000,
                    Latitude = 42.1900, Longitude = 23.3930, BedsCount = 80, PricePerNight = 35m,
                    HasWater = true, HasElectricity = true, HasFood = true, PhoneNumber = "+359 888 000 002",
                    Description = "Легендарна хижа в Северозападна Рила, център на българския алпинизъм. Оттук тръгват маршрути към Мальовица и Седемте езера."
                },
                new Hut
                {
                    Id = 3, MountainId = 1, Name = "Хижа „Скакавица“", Altitude = 1876,
                    Latitude = 42.2380, Longitude = 23.3330, BedsCount = 40, PricePerNight = 25m,
                    HasWater = true, HasElectricity = false, HasFood = true,
                    Description = "Първата хижа в България (1922 г.), до едноименния водопад. Уютна и спокойна, подходяща за семейства."
                },
                new Hut
                {
                    Id = 4, MountainId = 2, Name = "Хижа „Вихрен“", Altitude = 1950,
                    Latitude = 41.7660, Longitude = 23.4160, BedsCount = 110, PricePerNight = 35m,
                    HasWater = true, HasElectricity = true, HasFood = true, PhoneNumber = "+359 888 000 004",
                    Description = "Основната изходна точка за връх Вихрен. Намира се над Банско, до нея се стига с автомобил."
                },
                new Hut
                {
                    Id = 5, MountainId = 2, Name = "Хижа „Бъндерица“", Altitude = 1810,
                    Latitude = 41.7790, Longitude = 23.4300, BedsCount = 60, PricePerNight = 30m,
                    HasWater = true, HasElectricity = true, HasFood = true,
                    Description = "Хижа в долината на река Бъндерица, близо до Байкушевата мура – най-старото дърво в България."
                },
                new Hut
                {
                    Id = 6, MountainId = 3, Name = "Хижа „Рай“", Altitude = 1520,
                    Latitude = 42.7250, Longitude = 24.9100, BedsCount = 60, PricePerNight = 25m,
                    HasWater = true, HasElectricity = false, HasFood = true,
                    Description = "Хижа под Райското пръскало – най-високия водопад на Балканите. Изходна точка към връх Ботев."
                },
                new Hut
                {
                    Id = 7, MountainId = 3, Name = "Хижа „Амбарица“", Altitude = 1550,
                    Latitude = 42.7570, Longitude = 24.7800, BedsCount = 70, PricePerNight = 25m,
                    HasWater = true, HasElectricity = true, HasFood = false,
                    Description = "Хижа в Национален парк „Централен Балкан“ под връх Амбарица, на прехода Ком – Емине."
                },
                new Hut
                {
                    Id = 8, MountainId = 4, Name = "Хижа „Алеко“", Altitude = 1810,
                    Latitude = 42.5810, Longitude = 23.2930, BedsCount = 50, PricePerNight = 40m,
                    HasWater = true, HasElectricity = true, HasFood = true, PhoneNumber = "+359 888 000 008",
                    Description = "Най-популярната хижа на Витоша, до която има асфалтов път. Идеална за кратки разходки и ски."
                },
                new Hut
                {
                    Id = 9, MountainId = 5, Name = "Хижа „Смолянски езера“", Altitude = 1500,
                    Latitude = 41.6150, Longitude = 24.6750, BedsCount = 40, PricePerNight = 25m,
                    HasWater = true, HasElectricity = true, HasFood = true,
                    Description = "Хижа сред Смолянските езера в Родопите – спокойни гористи маршрути и красиви гледки."
                }
            };
        }
        public static IEnumerable<Peak> GetPeaks()
        {
            return new List<Peak>
            {
                new Peak { Id = 1, MountainId = 1, Name = "Мусала", Elevation = 2925, Latitude = 42.1795, Longitude = 23.5852, IsNationalTouristSite = true,
                    Description = "Най-високият връх на България и на Балканския полуостров." },
                new Peak { Id = 2, MountainId = 1, Name = "Иречек", Elevation = 2852, Latitude = 42.1720, Longitude = 23.5710, IsNationalTouristSite = false,
                    Description = "Вторият по височина връх в Рила, близо до Мусала." },
                new Peak { Id = 3, MountainId = 1, Name = "Мальовица", Elevation = 2729, Latitude = 42.1753, Longitude = 23.3700, IsNationalTouristSite = true,
                    Description = "Емблематичен връх в Северозападна Рила с впечатляваща северна стена." },
                new Peak { Id = 4, MountainId = 2, Name = "Вихрен", Elevation = 2914, Latitude = 41.7672, Longitude = 23.3994, IsNationalTouristSite = true,
                    Description = "Най-високият връх на Пирин – мраморна пирамида над Банско." },
                new Peak { Id = 5, MountainId = 2, Name = "Кутело", Elevation = 2908, Latitude = 41.7744, Longitude = 23.3966, IsNationalTouristSite = false,
                    Description = "Връх до Вихрен, свързан с него чрез ръба Кончето." },
                new Peak { Id = 6, MountainId = 2, Name = "Тодорка", Elevation = 2746, Latitude = 41.7956, Longitude = 23.4417, IsNationalTouristSite = false,
                    Description = "Връх над Банско с лесен достъп от ски зоната." },
                new Peak { Id = 7, MountainId = 3, Name = "Ботев", Elevation = 2376, Latitude = 42.7172, Longitude = 24.9172, IsNationalTouristSite = true,
                    Description = "Най-високият връх на Стара планина." },
                new Peak { Id = 8, MountainId = 3, Name = "Ком", Elevation = 2016, Latitude = 43.1747, Longitude = 23.0522, IsNationalTouristSite = false,
                    Description = "Началото на прехода Ком – Емине по билото на Стара планина." },
                new Peak { Id = 9, MountainId = 4, Name = "Черни връх", Elevation = 2290, Latitude = 42.5636, Longitude = 23.2783, IsNationalTouristSite = true,
                    Description = "Най-високата точка на Витоша с метеорологична станция." },
                new Peak { Id = 10, MountainId = 5, Name = "Голям Перелик", Elevation = 2191, Latitude = 41.6045, Longitude = 24.5717, IsNationalTouristSite = true,
                    Description = "Най-високият връх на Родопите." }
            };
        }
        public static IEnumerable<Trail> GetTrails()
        {
            return new List<Trail>
            {
                new Trail
                {
                    Id = 1, MountainId = 1, Name = "Боровец – хижа „Мусала“",
                    LengthKm = 10, ElevationGain = 1050, DurationMinutes = 270,
                    Difficulty = Difficulty.Medium, Season = Season.SpringToAutumn,
                    Description = "Класически маршрут от курорта Боровец през гората и покрай Бистрица до хижа „Мусала“. Алтернатива е лифтът „Ярема“."
                },
                new Trail
                {
                    Id = 2, MountainId = 1, Name = "Хижа „Мусала“ – връх Мусала",
                    LengthKm = 3.2, ElevationGain = 540, DurationMinutes = 120,
                    Difficulty = Difficulty.Medium, Season = Season.Summer,
                    Description = "Добре маркирана пътека покрай Леденото езеро и заслона до най-високата точка на Балканите."
                },
                new Trail
                {
                    Id = 3, MountainId = 1, Name = "Хижа „Мальовица“ – връх Мальовица",
                    LengthKm = 6.5, ElevationGain = 730, DurationMinutes = 210,
                    Difficulty = Difficulty.Hard, Season = Season.Summer,
                    Description = "Маршрут през Еленино езеро и Мальовишкия циркус. Последната част е стръмна и камениста."
                },
                new Trail
                {
                    Id = 4, MountainId = 2, Name = "Хижа „Вихрен“ – връх Вихрен",
                    LengthKm = 4.5, ElevationGain = 965, DurationMinutes = 210,
                    Difficulty = Difficulty.Hard, Season = Season.Summer,
                    Description = "Стръмно изкачване през седловината Кабата до мраморния връх. Зимно време е подходящ само за опитни."
                },
                new Trail
                {
                    Id = 5, MountainId = 4, Name = "Хижа „Алеко“ – Черни връх",
                    LengthKm = 4, ElevationGain = 480, DurationMinutes = 90,
                    Difficulty = Difficulty.Easy, Season = Season.AllYear,
                    Description = "Най-лесният път до Черни връх – широка пътека по склона, подходяща и за деца."
                },
                new Trail
                {
                    Id = 6, MountainId = 3, Name = "Хижа „Рай“ – връх Ботев",
                    LengthKm = 7, ElevationGain = 860, DurationMinutes = 240,
                    Difficulty = Difficulty.Hard, Season = Season.Summer,
                    Description = "Маршрут покрай Райското пръскало и по стръмната пътека през Джендема до връх Ботев."
                }
            };
        }

        public static IEnumerable<TrailPoint> GetTrailPoints()
        {
            return new List<TrailPoint>
            {
                new TrailPoint { Id = 1, TrailId = 1, Order = 1, Name = "Боровец", Latitude = 42.2660, Longitude = 23.6060, PointType = TrailPointType.Trailhead },
                new TrailPoint { Id = 2, TrailId = 1, Order = 2, Name = "Горна станция Ярема", Latitude = 42.2130, Longitude = 23.5850, PointType = TrailPointType.Waypoint },
                new TrailPoint { Id = 3, TrailId = 1, Order = 3, Name = "Хижа „Мусала“", Latitude = 42.1931, Longitude = 23.5836, PointType = TrailPointType.Hut },

                new TrailPoint { Id = 4, TrailId = 2, Order = 1, Name = "Хижа „Мусала“", Latitude = 42.1931, Longitude = 23.5836, PointType = TrailPointType.Hut },
                new TrailPoint { Id = 5, TrailId = 2, Order = 2, Name = "Заслон „Леденото езеро“", Latitude = 42.1825, Longitude = 23.5835, PointType = TrailPointType.Waypoint },
                new TrailPoint { Id = 6, TrailId = 2, Order = 3, Name = "Връх Мусала", Latitude = 42.1795, Longitude = 23.5852, PointType = TrailPointType.Peak },

                new TrailPoint { Id = 7, TrailId = 3, Order = 1, Name = "Хижа „Мальовица“", Latitude = 42.1900, Longitude = 23.3930, PointType = TrailPointType.Hut },
                new TrailPoint { Id = 8, TrailId = 3, Order = 2, Name = "Еленино езеро", Latitude = 42.1820, Longitude = 23.3820, PointType = TrailPointType.Waypoint },
                new TrailPoint { Id = 9, TrailId = 3, Order = 3, Name = "Връх Мальовица", Latitude = 42.1753, Longitude = 23.3700, PointType = TrailPointType.Peak },

                new TrailPoint { Id = 10, TrailId = 4, Order = 1, Name = "Хижа „Вихрен“", Latitude = 41.7660, Longitude = 23.4160, PointType = TrailPointType.Hut },
                new TrailPoint { Id = 11, TrailId = 4, Order = 2, Name = "Седловина Кабата", Latitude = 41.7708, Longitude = 23.4064, PointType = TrailPointType.Waypoint },
                new TrailPoint { Id = 12, TrailId = 4, Order = 3, Name = "Връх Вихрен", Latitude = 41.7672, Longitude = 23.3994, PointType = TrailPointType.Peak },

                new TrailPoint { Id = 13, TrailId = 5, Order = 1, Name = "Хижа „Алеко“", Latitude = 42.5810, Longitude = 23.2930, PointType = TrailPointType.Hut },
                new TrailPoint { Id = 14, TrailId = 5, Order = 2, Name = "Черни връх", Latitude = 42.5636, Longitude = 23.2783, PointType = TrailPointType.Peak },

                new TrailPoint { Id = 15, TrailId = 6, Order = 1, Name = "Хижа „Рай“", Latitude = 42.7250, Longitude = 24.9100, PointType = TrailPointType.Hut },
                new TrailPoint { Id = 16, TrailId = 6, Order = 2, Name = "Райското пръскало", Latitude = 42.7210, Longitude = 24.9130, PointType = TrailPointType.Waypoint },
                new TrailPoint { Id = 17, TrailId = 6, Order = 3, Name = "Връх Ботев", Latitude = 42.7172, Longitude = 24.9172, PointType = TrailPointType.Peak }
            };
        }
    }
}
