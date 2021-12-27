namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.Json.Serialization;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.Data.Models.Enums;
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable<ImportGames>>(jsonString);

			foreach (var game in data)
			{
				if (!IsValid(game) || game.Tags.Count() == 0)
				{
					output.AppendLine("Invalid Data");
					continue;
				}
				var developer = context.Developers.FirstOrDefault(x => x.Name == game.Developer) ?? new Developer{Name = game.Developer};
				var genre = context.Genres.FirstOrDefault(x => x.Name == game.Genre) ?? new Genre{Name = game.Genre};
				var GameForAdd = new Game
				{
					Name =game.Name,
					Price = game.Price,
					ReleaseDate = game.ReleaseDate.Value,
					Developer = developer,
					Genre = genre,
				};
                foreach (var tag in game.Tags)
                {
                    if (!IsValid(tag))
                    {
						output.AppendLine("Invalid Data");
						continue;
					}
					var taga = context.Tags.FirstOrDefault(x => x.Name == tag) ?? new Tag { Name = tag };
					var tagForAdd = new GameTag
					{
						Tag = taga
					};
					GameForAdd.GameTags.Add(tagForAdd);
                }
				context.Games.Add(GameForAdd);
				context.SaveChanges();
				output.AppendLine($"Added {GameForAdd.Name} ({GameForAdd.Genre.Name}) with {GameForAdd.GameTags.Count} tags");
            }
			return output.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable<ImportUsers>>(jsonString);
            foreach (var user in data)
            {
                if (!IsValid(user))
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var userForAdd = new User
				{
					FullName = user.FullName,
					Username = user.Username,
					Email = user.Email,
					Age = user.Age
				};
                foreach (var card in user.Cards)
                {
                    if (!IsValid(card))
                    {
						output.AppendLine("Invalid Data");
						continue;
					}
					var carForAdd = new Card
					{
						Number = card.Number,
						Cvc = card.CVC,
						Type = card.Type.Value
					};
					userForAdd.Cards.Add(carForAdd);
                }
				context.Users.Add(userForAdd);
				context.SaveChanges();
				output.AppendLine($"Imported {userForAdd.Username} with {userForAdd.Cards.Count} cards");
            }
			return output.ToString().TrimEnd();

		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var output = new StringBuilder();
			var xmls = new XmlSerializer(typeof(ImportPurchases[]), new XmlRootAttribute("Purchases"));
			var data = (ImportPurchases[])xmls.Deserialize(new StringReader(xmlString));
            foreach (var purch in data)
            {
                if (!IsValid(purch))
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var dataBuul = DateTime.TryParseExact(purch.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date);
                if (!dataBuul)
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var purc = new Purchase
				{
					Type = purch.Type.Value,
					ProductKey = purch.Key,
					Card = context.Cards.FirstOrDefault(x => x.Number == purch.Card),
					Game = context.Games.FirstOrDefault(x=>x.Name == purch.Title),
					Date = date
				};
				context.Purchases.Add(purc);
				context.SaveChanges();
				output.AppendLine($"Imported {purc.Game.Name} for {purc.Card.User.Username}");
            }
			return output.ToString().TrimEnd();

		}

		private static bool IsValid(object dto)
		{
			var validationContext = new ValidationContext(dto);
			var validationResult = new List<ValidationResult>();

			return Validator.TryValidateObject(dto, validationContext, validationResult, true);
		}
	}
}