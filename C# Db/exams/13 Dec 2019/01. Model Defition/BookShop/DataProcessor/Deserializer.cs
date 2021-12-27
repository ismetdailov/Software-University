namespace BookShop.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using BookShop.Data.Models;
    using BookShop.Data.Models.Enums;
    using BookShop.DataProcessor.ImportDto;
    using Data;
    using Newtonsoft.Json;
    using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedBook
            = "Successfully imported book {0} for {1:F2}.";

        private const string SuccessfullyImportedAuthor
            = "Successfully imported author - {0} with {1} books.";

        public static string ImportBooks(BookShopContext context, string xmlString)
        {
            var output = new StringBuilder();
            var xmlSerializer = new XmlSerializer(
                typeof(ImportBook[]), // ТУК НЕМОЖЕМ ДА ИЗПОЛЗВАМЕ ieNUMERUBLE И ЗАТОВА И ЗАТОВА ИЗПОЛЗВАМЕ МАСИВ
                new XmlRootAttribute("Books"));// ТУК СЛАГАМЕ РООТ ЕЛЕМЕНТА ТОВА Е ЗАГЛАВИЕТО
                                                   // НА ХМЛА ВИЖ ПЪРЧАСЕ ЕЛЕМЕНТ
            var purchases = // ТУК ПОЛУЧАВАМЕ ДАННИТЕ
                (ImportBook[])xmlSerializer.Deserialize(
                    new StringReader(xmlString));
            foreach (var book in purchases)
            {
                if (!IsValid(book))
                {
                  output.AppendLine(string.Format(ErrorMessage));
                    continue;
                }
                var publishedBool = DateTime.TryParseExact(book.PublishedOn, "MM/dd/yyyy", CultureInfo.InvariantCulture,DateTimeStyles.None, out var puplishedDate);
                if (!publishedBool)
                {
                    output.AppendLine(string.Format(ErrorMessage));
                    continue;
                }
                var price = String.Format("{0:0.00}", book.Price);
                var pri = decimal.Parse(price);
                var boolForAdd = new Book
                {
                    Name = book.Name,
                    Genre = (Genre)book.Genre,
                    Price = pri,
                Pages = book.Pages,
                    PublishedOn = puplishedDate,
                };
                context.Books.Add(boolForAdd);
                context.SaveChanges();
                output.AppendLine($"Successfully imported book {boolForAdd.Name} for {boolForAdd.Price}.");
            }
            return output.ToString().TrimEnd();
        }

        public static string ImportAuthors(BookShopContext context, string jsonString)
        {
            var output = new StringBuilder();
            var data = JsonConvert.DeserializeObject<IEnumerable<ImportAuthors>>(jsonString);
            foreach (var author in data)
            {
                if (!IsValid(author))
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                var email = context.Authors.FirstOrDefault(x => x.Email == author.Email);

                if (email!= null|| author.Email == null)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                var authorForAdd = new Author
                {
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Phone = author.Phone,
                    Email = author.Email,
                };
                if (author.Books.Count() ==0)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                foreach (var bookId in author.Books)
                {
                    var book = context.Books.FirstOrDefault(x => x.Id == bookId.Id);
                    if (book == null|| bookId.Id == null)
                    {
                        continue;
                    }
                    if (!IsValid(bookId))
                    {
                        output.AppendLine(ErrorMessage);

                        continue;
                    }
                    
                    var IdBook = new AuthorBook
                    {
                        BookId = book.Id
                    };
                    authorForAdd.AuthorsBooks.Add(IdBook);
                }
                if (authorForAdd.AuthorsBooks.Count()==0)
                {
                    output.AppendLine(ErrorMessage);
                    continue;
                }
                if(authorForAdd.AuthorsBooks.Count()>0)
                {
                context.Authors.Add(authorForAdd);
                output.AppendLine($"Successfully imported author - {authorForAdd.FirstName} {authorForAdd.LastName} with {authorForAdd.AuthorsBooks.Count()} books.");
                context.SaveChanges();
                   
                }
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