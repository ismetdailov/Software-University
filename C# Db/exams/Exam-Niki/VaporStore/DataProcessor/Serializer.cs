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
		// JSON
		public static string ExportGamesByGenres(VaporStoreDbContext context, string[] genreNames)
		{
			// ВЗИМАМЕ ДАННИТЕ ОТ ЖАНРОВЕТЕ ИМАМЕ СПИСЪК СЪС ЖАНРОВЕ И
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

		// XML
		// ЗА НЕГО СИ ПРАВИМ ОТДЕЛНИТЕ ТИПОВЕ КАТО ГАМЕ ПЪРЧАСЕ И ЮЗЕР виж експорт
		public static string ExportUserPurchasesByType(VaporStoreDbContext context, string storeType)
		{
			var data = context.Users.ToList()// ПРОВЕРЯВАМЕ ДАЛИ ИМА ПОНЕ ЕДНА ПОКУПКА С КАРТАТА
				.Where(x => x.Cards.Any(c => c.Purchases.Any(p => p.Type.ToString() == storeType)))//ТУК В ПОСЛЕДНИТЕ
												//СКОБИ ЗА ТЕЗИ ПОРЪЧКИ КОЙТО СА ОТ ДАДЕНИЯ ТИП В СЛУЧАЯ storeType
				.Select(x => new UserXmlExportModel
				{
					Username = x.Username,
					// СУМИРАМЕ ВСЯКА ПОКУПКА НА ЮЗЕРА С ВСИЧКИТЕ МУ
					// ПОКУПКИ НАМИРАМЕ КАРТАТА И КОЛКО ПАРИ Е ПРЪСНАЛ ОТ ТАЗИ
					// КАРТА КАТО СУМИРАМЕ ВСИЧКИТЕ МУ ПОКУПКИ ОТ ТОЗИ ТИП  
					TotalSpent = x.Cards.Sum(
						c => c.Purchases.Where(p => p.Type.ToString() == storeType)
							  .Sum(p => p.Game.Price)),
					// ВЗИМАМЕ КОГАТО ИМАМЕ ЮЗЕРА КОЙТО Е Х ТОЙ ИМА КАРТИ КАРДС И
					// С ВСЯКА КАРТА ТОЙ ИМА ПОКУПКИ СЕЛЕКТ МЕНИ ГРУПИРА ВСИЧКИТЕ КАРТИ НА ПОТРЕБИТЕЛЯ
					// И ТАКА ВЗИМАМЕ ВСИЧКИТЕ МУ ПОКУПКИ ОТ ВСИЧКИ КАРТИ
					// ПОСЛЕ ПРАВИМ ПРОЕКЦИИ КАТО ИЗПОЛЗВАМЕ РАЗЛИЧНИТЕ КЛАСОВЕ КОЙТО СМЕ
					// СЪЗДАЛИ ПРАВИМ ДВЕ ПРОЕКЦИИ ЗАЩОТО ИМАМЕ ДВА КЛАСА В ХМЛ ФАЙЛА
					Purchases = x.Cards.SelectMany(c => c.Purchases)
						.Where(p => p.Type.ToString() == storeType)
						.Select(p => new PurchaseXmlExportModel
						{
							Card = p.Card.Number,
							Cvc = p.Card.Cvc,
							Date = p.Date.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture),
							Game = new GameXmlExportModel
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
				new XmlSerializer(typeof(UserXmlExportModel[]),
					new XmlRootAttribute("Users"));
			var sw = new StringWriter();
			//ТОВА ГО СЛАГАМЕ АКО ИМЕМЕ РАЗЛИКА ОТ НАШИЯ АУТПУТ И АУТПУТА КОЙТО НИ ДАВАТ
			XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
			ns.Add("", "");
			xmlSerializer.Serialize(sw, data, ns);
			return sw.ToString();
		}
	}
}