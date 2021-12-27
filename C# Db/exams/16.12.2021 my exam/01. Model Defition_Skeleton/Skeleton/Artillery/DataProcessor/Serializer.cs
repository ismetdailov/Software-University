
namespace Artillery.DataProcessor
{
    using Artillery.Data;
    using Artillery.DataProcessor.ExportDto;
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportShells(ArtilleryContext context, double shellWeight)
        {
            var result = context.Shells.ToArray()
                .Where(x => x.ShellWeight > shellWeight).ToArray().Select(s => new
            {
                ShellWeight = s.ShellWeight,
                Caliber = s.Caliber,
                Guns = s.Guns.ToArray()
                .Where(x => x.GunType.ToString() == "AntiAircraftGun" ).Select(g => new
                {
                    GunType = g.GunType.ToString(),
                    GunWeight = g.GunWeight,
                    BarrelLength = g.BarrelLength,
                    Range = g.Range > 3000 ? "Long-range" : "Regular range"
                }).ToArray().OrderByDescending(x => x.GunWeight).ToArray()
            }).OrderBy(x=>x.ShellWeight).ToArray();
            return JsonConvert.SerializeObject(result, Formatting.Indented);

        }

        public static string ExportGuns(ArtilleryContext context, string manufacturer)
        {
            var data = context.Guns.ToArray().Where(x => x.Manufacturer.ManufacturerName == manufacturer)
                .Select(g => new exporttGun
            {
                Manufacturer = g.Manufacturer.ManufacturerName,
                GunType = g.GunType.ToString(),
                GunWeight = g.GunWeight,
                BarrelLength = g.BarrelLength,
                Range = g.Range,
                Countries = g.CountriesGuns.ToArray().Where(x => x.Country.ArmySize > 4500000).Select(c => new ExportCountries
                {
                    Country = c.Country.CountryName,
                    ArmySize = c.Country.ArmySize
                }).ToArray().OrderBy(x => x.ArmySize).ToArray()

            }).ToArray().OrderBy(c => c.BarrelLength).ToArray();

            var xmlSerializer = new XmlSerializer(typeof(exporttGun[]),
               new XmlRootAttribute("Guns"));
            var stringR = new StringWriter();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            xmlSerializer.Serialize(stringR, data, ns);
            return stringR.ToString();

        }
    }
}
