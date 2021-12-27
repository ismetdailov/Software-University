namespace Cinema.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using Cinema.Data.Models;
    using Cinema.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfulImportMovie 
            = "Successfully imported {0} with genre {1} and rating {2}!";

        private const string SuccessfulImportProjection 
            = "Successfully imported projection {0} on {1}!";

        private const string SuccessfulImportCustomerTicket 
            = "Successfully imported customer {0} {1} with bought tickets: {2}!";

        public static string ImportMovies(CinemaContext context, string jsonString)
        {
            var output = new StringBuilder();
            var movies = JsonConvert
                .DeserializeObject<List<ImportMobies>>(jsonString);
            var title1 = context.Movies.ToList().Select(x=>x.Title).ToList();
            var list = new List<string>();

            foreach (var movie in movies)
            {

                var vvv = movie.Title;
               // var caount = list.Count();
                if (list.Contains(vvv))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                list.Add(vvv);

                var nfefew = context.Movies.Where(x => x.Title == movie.Title);
                var mfefwef = title1.Any(s => title1.Contains(movie.Title));
                
                if (!IsValid(movie) )
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                if (movie.Rating <1 || movie.Rating>10) 
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
              
                var validmovie = new Movie
                {
                    
                    Title = movie.Title,
                    Genre = Enum.Parse<Genre>(movie.Genre),
                    Duration = TimeSpan.ParseExact(movie.Duration, "c", CultureInfo.InvariantCulture),// movie.Duration= string.Format
                    Rating = movie.Rating,
                    Director = movie.Director

                };

                context.Movies.Add(validmovie);
                output.AppendLine($"Successfully imported {validmovie.Title} with genre {validmovie.Genre} and rating {validmovie.Rating:F2}!");
            }
            context.SaveChanges();
            return output.ToString();
        }

        public static string ImportProjections(CinemaContext context, string xmlString)
        {
            var sb = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportProjections[]),
                new XmlRootAttribute("Projections"));
            var projecti = (ImportProjections[])xmlSerializer.Deserialize(new StringReader(xmlString));

            foreach (var validpro in projecti)
            {
                if (!IsValid(validpro))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
               
                var isValidReleaseDate = DateTime.TryParseExact(validpro.DateTime,
                   "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture,
                   DateTimeStyles.None,
                   out DateTime releaseDate);
                // var datetime = DateTime.ParseExact(validpro.DateTime, "c", CultureInfo.InvariantCulture)
                var newProjec = new Projection
                {
                    DateTime = releaseDate,//validpro.DateTime,//validpro.DateTime,
                    MovieId = (int.Parse(validpro.MovieId))
                   // Movie = validpro.Movie,
                    
                };
                if (newProjec.MovieId >0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                context.Projections.Add(newProjec);
                sb.AppendLine($"Successfully imported projection {newProjec.Movie.Title} on {newProjec.DateTime}!");


            }
            context.SaveChanges();
            return sb.ToString().TrimEnd();

        }

        public static string ImportCustomerTickets(CinemaContext context, string xmlString)
        {
            return "TODO";

        }
        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}