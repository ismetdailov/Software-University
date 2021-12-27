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
    using VaporStore.DataProcessor.Dto.Import;

    public static class Deserializer
	{
		public static string ImportGames(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable< ImportGamesDevelopersGenresTags>>(jsonString);
            foreach (var game in data)
            {
                if (!IsValid(game))
                {
					output.AppendLine("Invalid Data");
					continue;
                }
				var dev = context.Developers.FirstOrDefault(x => x.Name == game.Developer) ?? new Developer {Name = game.Name};
				var gen = context.Genres.FirstOrDefault(x => x.Name == game.Genre) ?? new Genre { Name = game.Genre };
				var gameForAdd = new Game
				{
					Name = game.Name,
					ReleaseDate = game.ReleaseDate,
					Developer = dev,
					Genre = gen,
				};
				if (game.Tags.Length <= 0 || game.Tags == null)
				{
					output.AppendLine("Invalid Data");
					continue;
				}
				foreach (var tag in game.Tags)
                {
                    if (game.Tags.Length<=0 || game.Tags == null)
                    {
						output.AppendLine("Invalid Data");
						continue;
                    }
					var ta = context.Tags.FirstOrDefault(x => x.Name == tag) ?? new Tag { Name = tag };
					var tagForAdd = new GameTag
					{
						Tag = ta
					};
					gameForAdd.GameTags.Add(tagForAdd);
                };
				context.Games.Add(gameForAdd);
				output.AppendLine($"Added {gameForAdd.Name} ({gameForAdd.Genre.Name}) with {gameForAdd.GameTags.Count} tags");
				context.SaveChanges();
            }
			return output.ToString().TrimEnd();
		}

		public static string ImportUsers(VaporStoreDbContext context, string jsonString)
		{
			var output = new StringBuilder();
			var data = JsonConvert.DeserializeObject<IEnumerable<ImportUsersCards>>(jsonString);
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
					var cardForAdd = new Card
					{
						Number = card.Number,
						Cvc = card.CVC,
						Type = card.Type
					};
					userForAdd.Cards.Add(cardForAdd);
                }
				context.Users.Add(userForAdd);
				output.AppendLine($"Imported {userForAdd.Username} with {userForAdd.Cards.Count} cards");
				context.SaveChanges();
            }
			return output.ToString().TrimEnd();

		}

		public static string ImportPurchases(VaporStoreDbContext context, string xmlString)
		{
			var output = new StringBuilder();
			var xmlserializer = new XmlSerializer(typeof(ImportPurchases[]), new XmlRootAttribute("Purchases"));
			var data = (ImportPurchases[])xmlserializer.Deserialize(new StringReader(xmlString));
            foreach (var purch in data)
            {
                if (!IsValid(purch))
                {
					output.AppendLine("Invalid Data");
					continue;
				}
				var dateBool = DateTime.TryParseExact(purch.Date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateDa);
				var game = context.Games.FirstOrDefault(x => x.Name == purch.Title);
				var cardNu = context.Cards.FirstOrDefault(x => x.Number == purch.Card);

				var purchForAdd = new Purchase
				{
					Game = game,
					Type = purch.Type,
					ProductKey = purch.Key,
					Card = cardNu,
					Date = dateDa
				};
				context.Purchases.Add(purchForAdd);
				output.AppendLine($"Imported {purchForAdd.Game.Name} for {purchForAdd.Card.User.Username}");
				context.SaveChanges();
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