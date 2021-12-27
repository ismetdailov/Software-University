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
		{// ВЗИМАМЕ ДАННИТЕ ОТ ЖАНРОВЕТЕ ИМАМЕ СПИСЪК СЪС ЖАНРОВЕ И
		 // ПРОВЕРЯВАМЕ ДАЛИ ЕКСПОРТВАНИТЕ ЖАНРОВЕ СЪВПАДАТ С ТЯХ
		 // И ВИНАГИ СЛАГАМЕ ТОЛИСТ ТУК ЗА ДЖЪДЖ
			var data = context.Genres.ToList().Where(x => genreNames.Contains(x.Name))
				// ЗА ВСЕКИ ЖАНР ВЗИМАМЕ СЛЕДНАТА ИНФОРМАЦИЯ ПО ТОЗИ НАЧИН
				.Select(x => new
				{
					Id = x.Id,
					Genre = x.Name,// Genre - НИЕ СИ ИЗМИСЛЯМЕ ТЕЗИ ИМЕНА НО ГИ ВЗИМАМЕ ОТ ФАЙЛА
					Games = x.Games.Select(g => new // ПРАВИМ НОВА ПРОЕКЦИЯ НА ГАМЕС ЗАЩОТО Е КЛАС
					{
						Id = g.Id,
						Title = g.Name,
						Developer = g.Developer.Name,
						Tags = string.Join(", ", g.GameTags.Select(gt => gt.Tag.Name)),
						// ИЗПОЛЗВАМЕ СТРИНГ ДЖОИН ЗАЩОТО ИМА МНОГО ТАГОВЕ И ТАКА ГИ ВЗИМАМЕ
						Players = g.Purchases.Count(),
					})
						.Where(g => g.Players > 0)// ТАКА ВЗИМАМЕ ИГРИТЕ КОЙТО ИМАТ ПОНЕ ЕДНА ПОКУПКА ИЛИ ЕДИН ИГРАЧ
						.OrderByDescending(g => g.Players)// СОРТИРАМЕ ПО БРОЙ ИГРАЧИ
						.ThenBy(g => g.Id),
					TotalPlayers = x.Games.Sum(g => g.Purchases.Count()),// ЗА ДА
																		 // ВЗЕМЕМ ВСИЧКИ ИГРАЧИ ВЛИЗАМЕ В ИГРИ И
																		 // ВЗИМАМЕ ВСИЧКИ ХОРА КОЙТО СА ЗАКУПИЛИ ИГРАТА
				})
				.OrderByDescending(x => x.TotalPlayers).ThenBy(x => x.Id);// СОРТИРАМЕ ПО БРОЙ ИГРАЧИ И ПО ЖАНР ИД

			return JsonConvert.SerializeObject(data, Formatting.Indented);// СЛАГАМЕ
																		  // Formatting.Indented ЗА ДА Е ПОДРЕДЕН КОДА В КОНЗОЛАТА
		}

		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var data = context.Users.ToList()
				.Where(x => x.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == storeType)))
				.Select(x => new XmlExportModel
				{
					Username = x.Username,
					TotalSpent = x.Cards.Sum(
						c => c.Purchases.Where(p => p.Type.ToString() == storeType)
							  .Sum(p => p.Game.Price)),
					Purchases = x.Cards.SelectMany(c => c.Purchases)
						.Where(p => p.Type.ToString() == storeType)
						.Select(p => new XmlExportModel
						{
							Card = p.Card.Number,
							Cvc = p.Card.Cvc,
							Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
							Game = new XmlExportModel
							{
								Title = p.Game.Name,
								Price = p.Game.Price,
								Genre = p.Game.Genre.Name,
							}
						})
						.OrderBy(x => x.Date)
						.ToArray()
				})
				.OrderByDescending(x => x.TotalSpent).ThenBy(x => x.Username).ToArray();

			XmlSerializer xmlSerializer =
				new XmlSerializer(typeof(XmlExportModel[]),
					new XmlRootAttribute("Users"));
			var sw = new StringWriter();
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			xmlSerializer.Serialize(sw, data, ns);
			return sw.ToString();
		}
	}
}