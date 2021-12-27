namespace VaporStore.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.DataProcessor.Dto.Export;

    public static class Serializer
    {
        public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
        {
            var result = context.Genres.ToArray().Where(x => genreNames.Contains(x.Name)).Select(g => new
            {
                Id = g.Id,
                Genre = g.Name,
                Games = g.Games.Select(game => new
                {
                    Id = game.Id,
                    Title = game.Name,
                    Developer = game.Developer.Name,
                    Tags = string.Join(", ", game.GameTags.Select(x => x.Tag.Name)),
                    Players = game.Purchases.Count
                }).Where(x => x.Players > 0).OrderByDescending(x => x.Players).ThenBy(x => x.Id),
                TotalPlayers = g.Games.Sum(x => x.Purchases.Count)
            }).OrderByDescending(x => x.TotalPlayers).ThenBy(x => x.Id).ToArray();
            return JsonConvert.SerializeObject(result, Formatting.Indented);
        }

        public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
        {
            var result = context.Users.ToList()
                .Where(c => c.Cards.Any(p => p.Purchases.Any(p => p.Type.ToString() == storeType)))
                .Select(u => new ExportUserPurchases()
                {
                    Username = u.Username,
                    TotalSpend = u.Cards.Sum(x => x.Purchases.Where(t => t.Type.ToString() == storeType).Sum(y => y.Game.Price)),

                    Purchases = u.Cards.SelectMany(x => x.Purchases)
                .Where(p => p.Type.ToString() == storeType)
                .Select(p => new ExportPurchases()
                {
                    Card = p.Card.Number,
                    Cvc = p.Card.Cvc,
                    Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
                    Game = new ExportGame()
                    {
                        GameName = p.Game.Name,
                        Genre = p.Game.Genre.Name,
                        Price = p.Game.Price,
                    }
                })
                .OrderBy(x => x.Date)
                .ToArray(),

                })
                .OrderByDescending(x => x.TotalSpend)
                .ThenBy(x => x.Username)
                .ToArray();
            var Xmlseri = new XmlSerializer(typeof(ExportUserPurchases[]), new XmlRootAttribute("Users"));
            var sw = new StringWriter();
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            Xmlseri.Serialize(sw, result, ns);
            return sw.ToString().TrimEnd();

        }
    }
}