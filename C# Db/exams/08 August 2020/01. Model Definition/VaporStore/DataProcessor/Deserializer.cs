namespace VaporStore.DataProcessor
{
	using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Data;
    using Newtonsoft.Json;
    using VaporStore.Data.Models;
    using VaporStore.DataProcessor.Dto;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable<ImportGamesDevelopers>>(jsonString);
            foreach (var game in data)
            {
                if (!IsValid(game) || !game.Tags.Any())
                {
					output.AppendLine("Invalid Data" );
					continue;
                }
				var developer = context.Developers.FirstOrDefault(x => x.Name == game.Developer)
					?? new Developer { Name = game.Developer };
           
				var genre = context.Genres.FirstOrDefault(x => x.Name == game.Genre)
					?? new Genre { Name = game.Genre };
				
				var gameJson = new Game
				{
					Name = game.Name,
					Genre = genre,
					Developer = developer,
					Price = game.Price,
					ReleaseDate = game.ReleaseDate.Value,

					
				};
                foreach (var tag in game.Tags)
                {
					var tagg = context.Tags.FirstOrDefault(x => x.Name == tag)
						?? new Tag { Name = tag };
					gameJson.GameTags.Add(new GameTag { Tag = tagg });

                }
				context.Games.Add(gameJson);
				context.SaveChanges();
				output.AppendLine($"Added { game.Name} ({ game.Genre}) with { game.Tags.Length} tags");
            }
			return output.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable<ImportUsers>>(jsonString);
            foreach (var userJson in data)
            {
                if (!IsValid(userJson))
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var user = new User
				{
					FullName = userJson.FullName,
					Username = userJson.Username,
					Email = userJson.Email,
					Age = userJson.Age
				};
                foreach (var card in userJson.Cards)
                {
					if (!IsValid(card))
					{
						output.AppendLine("Invalid Data");
						continue;
					}
					var cardJson = new Card
					{
						Number = card.Number,
						Cvc = card.CVC,
						Type = card.Type
					};
					user.Cards.Add(cardJson);
				}
				context.Users.Add(user);
				context.SaveChanges();
				output.AppendLine($"Imported {user.Username} with {user.Cards.Count} cards");
            }
			return output.ToString().TrimEnd();

		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var output = new StringBuilder();

			var xmlSerializer = new XmlSerializer(typeof(ImportPurchase[]),
				new XmlRootAttribute("Purchases"));
			var purchase = (ImportPurchase[])xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var purch in purchase)
            {
                if (!IsValid(purch))
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var parseDate = DateTime.TryParseExact(purch.Date, "dd/MM/yyyy HH:mm",CultureInfo.InvariantCulture,DateTimeStyles.None, out var date);
                if (!parseDate)
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var purchasJson = new Purchase
				{
					Date = date,
					Type = purch.Type.Value,
					ProductKey = purch.Key,
				};
				purchasJson.Card = context.Cards.FirstOrDefault(x => x.Number == purch.Card);
				purchasJson.Game = context.Games.FirstOrDefault(x => x.Name == purch.Title);
				context.Purchases.Add(purchasJson);
				context.SaveChanges();
				var username = context.Users.Where(x => x.Id == purchasJson.Card.UserId).Select(x => x.Username).FirstOrDefault();
				output.AppendLine($"Imported {purch.Title} for {username}");
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