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
			var result = context.Genres.ToList().Where(x=> genreNames.Contains(x.Name))
				.Select(g => new
			{
				Id = g.Id,
				Genre = g.Name,
 				Games = g.Games.Select(m => new
				{
					Id = m.Id,
					Title = m.Name,
					Developer = m.Developer.Name,
					Tags = string.Join(",", m.GameTags.Select(x=>x.Tag.Name)),
					Players = m.Purchases.Count()
				})
				.Where(g=>g.Players>0)
				.OrderByDescending(g=>g.Players)
				.ThenBy(x=>x.Id),
				TotalPlayers = g.Games.Sum(x=>x.Purchases.Count()),
			})
				.OrderByDescending(x=>x.TotalPlayers)
				.ThenBy(x=>x.Id);
			return JsonConvert.SerializeObject(result,Formatting.Indented);

		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var res = context.Users.ToList().Where(x => x.Cards.Any(x => x.Purchases.Any())).Select(p => new ExportUser()
			{
				Username = p.Username,
				TotalSpend = p.Cards.Sum(x => x.Purchases.Where(c => c.Type.ToString() == storeType).Sum(x => x.Game.Price)),
				Purchases = p.Cards.SelectMany(x => x.Purchases).Where(x=>x.Type.ToString()==storeType).Select(c => new ExportUserPurchases()
				{
					Card = c.Card.Number,
					Cvc = c.Card.Cvc.ToString(),
					Date = c.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
					Game = new ExportGame()
					{
						Price = c.Game.Price,
						Genre = c.Game.Genre.Name,
						Title = c.Game.Name
					},

				}).OrderBy(x=>x.Date).ToArray(),

			}).OrderByDescending(x => x.TotalSpend).ThenBy(x => x.Username).ToArray();

			XmlSerializer xmSerializer = new XmlSerializer(typeof(ExportUser[]), new XmlRootAttribute("Users"));
			var sw = new StringWriter();
				var ns =new XmlSerializerNamespaces();
			ns.Add("", "");
			xmSerializer.Serialize(sw, res, ns);
			return sw.ToString().TrimEnd();

		}
	}
}