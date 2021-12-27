namespace Artillery.DataProcessor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Artillery.Data;
    using Artillery.Data.Models;
    using Artillery.Data.Models.Enums;
    using Artillery.DataProcessor.ImportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage =
                "Invalid data.";
        private const string SuccessfulImportCountry =
            "Successfully import {0} with {1} army personnel.";
        private const string SuccessfulImportManufacturer =
            "Successfully import manufacturer {0} founded in {1}.";
        private const string SuccessfulImportShell =
            "Successfully import shell caliber #{0} weight {1} kg.";
        private const string SuccessfulImportGun =
            "Successfully import gun {0} with a total weight of {1} kg. and barrel length of {2} m.";

        public static string ImportCountries(ArtilleryContext context, string xmlString)
        {
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportCountries[]),
                new XmlRootAttribute("Countries"));
            var countries =
                (ImportCountries[])xmlSerializer.Deserialize(
                    new StringReader(xmlString));
            foreach (var country in countries)
            {
                if (!IsValid(country))
                {
                    output.AppendLine("Invalid data.");
                    continue;
                }
                var countryForAdd = new Country
                {
                    CountryName = country.CountryName,
                    ArmySize = country.ArmySize
                };
                context.Countries.Add(countryForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully import {countryForAdd.CountryName} with {countryForAdd.ArmySize} army personnel.");
            }
            return output.ToString().TrimEnd();
        }

        public static string ImportManufacturers(ArtilleryContext context, string xmlString)
        {
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportManufacturers[]),
                new XmlRootAttribute("Manufacturers"));
            var manufacturers =
                (ImportManufacturers[])xmlSerializer.Deserialize(
                    new StringReader(xmlString));
                var names = new List<string>();
            foreach (var manu in manufacturers)
            {
                manu.ManufacturerName.Distinct();
                if (!IsValid(manu)|| names.Contains(manu.ManufacturerName))
                {
                    output.AppendLine("Invalid data.");
                    continue;
                }
                var manuForAdd = new Manufacturer
                {
                    ManufacturerName = manu.ManufacturerName,
                    Founded = string.Join(", ", manu.Founded)
                };
                var neshto = string.Join(", ", manu.Founded).ToList();
                var neshto1 = manu.Founded.Split(", ").ToList();
                Stack<string> numbers = new Stack<string>();
                foreach (var item in neshto1)
                {
                    numbers.Push(item);
                }
                names.Add(manu.ManufacturerName);
                var countryName = numbers.Pop();
                var townName = numbers.Pop();
                context.Manufacturers.Add(manuForAdd);
                output.AppendLine($"Successfully import manufacturer {manuForAdd.ManufacturerName} founded in {townName}, {countryName}.");
            }
               context.SaveChanges();
            return output.ToString().TrimEnd();

        }

        public static string ImportShells(ArtilleryContext context, string xmlString)
        {
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportShells[]),
                new XmlRootAttribute("Shells"));
            var shells =
                (ImportShells[])xmlSerializer.Deserialize(
                    new StringReader(xmlString));
            foreach (var shel in shells)
            {
                if (!IsValid(shel))
                {
                    output.AppendLine("Invalid data.");
                    continue;
                }
                var shelForAdd = new Shell
                {
                    ShellWeight = shel.ShellWeight,
                    Caliber = shel.Caliber,
                };
                 context.Shells.Add(shelForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully import shell caliber #{shelForAdd.Caliber} weight {shelForAdd.ShellWeight} kg.");
            }

            return output.ToString().TrimEnd();

        }

        public static string ImportGuns(ArtilleryContext context, string jsonString)
        {
            var output = new StringBuilder();
            var guns = JsonConvert
                .DeserializeObject<IEnumerable<ImportGuns>>(jsonString);
            foreach (var gun in guns)
            {
                var result2 = Enum.TryParse<GunType>(gun.GunType, out var flag);
                if (!IsValid(gun)|| result2 == false)
                {
                    output.AppendLine("Invalid data.");
                    continue;
                }
                var gunForAdd = new Gun
                {
                    ManufacturerId = gun.ManufacturerId,
                    GunWeight = gun.GunWeight,
                    BarrelLength = gun.BarrelLength,
                    NumberBuild = gun.NumberBuild.HasValue ? gun.NumberBuild : null,
                    Range = gun.Range,
                    GunType = Enum.Parse<GunType>(gun.GunType),
                    ShellId = gun.ShellId

                };
                foreach (var id in gun.Countries)
                {
                    var ii = new Country { Id = id.Id };
                    var idForAdd = new Country
                    {
                        Id = id.Id,
                    };
                    gunForAdd.CountriesGuns.Add(new CountryGun { CountryId = id.Id });
                }
                context.Guns.Add(gunForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully import gun {gunForAdd.GunType} with a total weight of {gunForAdd.GunWeight} kg. and barrel length of {gunForAdd.BarrelLength} m.");
            }


            return output.ToString().TrimEnd();

        }
        private static bool IsValid(object obj)
        {
            var validator = new ValidationContext(obj);
            var validationRes = new List<ValidationResult>();
            var result = Validator.TryValidateObject(obj, validator, validationRes, true);
            return result;
        }
    }
}
