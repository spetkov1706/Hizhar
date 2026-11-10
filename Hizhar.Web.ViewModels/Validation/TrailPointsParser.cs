using System.Globalization;
using Hizhar.Data.Models.Enums;
using Hizhar.Web.ViewModels.Trails;
using static Hizhar.Common.ValidationConstants.Coordinates;
using static Hizhar.Common.ValidationConstants.TrailPoint;

namespace Hizhar.Web.ViewModels.Validation
{
    public static class TrailPointsParser
    {
        public const int MinPoints = 2;

        public static bool TryParse(string? text, out List<TrailPointViewModel> points, out string? error)
        {
            points = new List<TrailPointViewModel>();
            error = null;

            if (string.IsNullOrWhiteSpace(text))
            {
                error = "Въведете поне две точки на маршрута.";
                return false;
            }

            string[] lines = text
                .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Where(l => l.Length > 0)
                .ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                int lineNumber = i + 1;
                string[] parts = lines[i].Split(';', StringSplitOptions.TrimEntries);

                if (parts.Length < 3 || parts.Length > 4)
                {
                    error = $"Ред {lineNumber}: очакван формат „Име; ширина; дължина; тип“.";
                    return false;
                }

                string name = parts[0];
                if (name.Length < NameMinLength || name.Length > NameMaxLength)
                {
                    error = $"Ред {lineNumber}: името трябва да е между {NameMinLength} и {NameMaxLength} символа.";
                    return false;
                }

                if (!double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude)
                    || latitude < LatitudeMin || latitude > LatitudeMax)
                {
                    error = $"Ред {lineNumber}: невалидна ширина (между {LatitudeMin} и {LatitudeMax}, с точка за десетичен знак).";
                    return false;
                }

                if (!double.TryParse(parts[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude)
                    || longitude < LongitudeMin || longitude > LongitudeMax)
                {
                    error = $"Ред {lineNumber}: невалидна дължина (между {LongitudeMin} и {LongitudeMax}, с точка за десетичен знак).";
                    return false;
                }

                TrailPointType type = TrailPointType.Waypoint;
                if (parts.Length == 4 && parts[3].Length > 0)
                {
                    if (!Enum.TryParse(parts[3], ignoreCase: true, out type) || !Enum.IsDefined(type))
                    {
                        error = $"Ред {lineNumber}: непознат тип „{parts[3]}“. Позволени: Trailhead, Hut, Peak, Waypoint.";
                        return false;
                    }
                }

                points.Add(new TrailPointViewModel
                {
                    Order = lineNumber,
                    Name = name,
                    Latitude = latitude,
                    Longitude = longitude,
                    PointType = type
                });
            }

            if (points.Count < MinPoints)
            {
                error = "Маршрутът трябва да има поне две точки – начало и край.";
                return false;
            }

            return true;
        }

        public static string ToText(IEnumerable<TrailPointViewModel> points)
        {
            return string.Join(Environment.NewLine, points
                .OrderBy(p => p.Order)
                .Select(p => string.Join("; ",
                    p.Name,
                    p.Latitude.ToString(CultureInfo.InvariantCulture),
                    p.Longitude.ToString(CultureInfo.InvariantCulture),
                    p.PointType.ToString())));
        }
    }
}
