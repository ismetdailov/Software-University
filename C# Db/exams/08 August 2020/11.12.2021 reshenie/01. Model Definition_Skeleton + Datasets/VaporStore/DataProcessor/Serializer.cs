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
			var result = context.Genres.ToArray().Where(x => genreNames.Contains(x.Name)).ToArray().Select(p => new
			{
				Id = p.Id,
				Genre = p.Name,
				Games = p.Games.Select(g => new
				{
					Id = g.Id,
					Title = g.Name,
					Developer = g.Developer.Name,
					Tags = string.Join(", ", g.GameTags.Select(x => x.Tag.Name)),
					Players = g.Purchases.Count(),
				})
				.Where(e=>e.Players>0)
				.OrderByDescending(x => x.Players)
				.ThenBy(g => g.Id).ToArray(),
				TotalPlayers = p.Games.Sum(x => x.Purchases.Count)
			}).OrderByDescending(x => x.TotalPlayers).ThenBy(s => s.Id).ToArray();
			return JsonConvert.SerializeObject(result,Formatting.Indented);

		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var result = context.Users.ToArray().Where(x => x.Cards.Any(x => x.Purchases.Any(v => v.Type.ToString() == storeType))).ToArray().Select(p => new ExportUserPurchases
			{
				Username = p.Username,
				TotalSpent = p.Cards.Sum(x => x.Purchases.Where(b=>b.Type.ToString()==storeType).Sum(s => s.Game.Price)),
				Purchases = p.Cards.SelectMany(x => x.Purchases).Where(f => f.Type.ToString() == storeType).Select(f => new ExportPurchases
				{
					Card = f.Card.Number,
					Cvc = f.Card.Cvc,
					Date = f.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
					Game = new ExportGame
					{
						Title = f.Game.Name,
						Genre = f.Game.Genre.Name,
						Price = f.Game.Price.ToString()
					}

				}).ToArray().OrderBy(x=>x.Date).ToArray()

			}).OrderByDescending(x=>x.TotalSpent).ThenBy(u=>u.Username).ToArray();
			var vml = new XmlSerializer(typeof(ExportUserPurchases[]), new XmlRootAttribute("Users"));
			var sw = new StringWriter();
			var ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			vml.Serialize(sw, result, ns);
			return sw.ToString().TrimEnd();

		}
	}
}