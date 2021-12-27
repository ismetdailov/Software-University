namespace BookShop.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using Formatting = Newtonsoft.Json.Formatting;

    public class Serializer
    {
        public static string ExportMostCraziestAuthors(BookShopContext context)
        {
            var result = context.Authors.ToList().Select(x => new
            {
                AuthorName = x.FirstName +' '+ x.LastName,
                Books = x.AuthorsBooks.ToList().OrderByDescending(p => p.Book.Price).Select(b => new
                {
                    BookName = b.Book.Name,
                    BookPrice = b.Book.Price.ToString("f2")
                }).ToList()
            }).OrderByDescending(o => o.Books.Count).ThenBy(a => a.AuthorName);
            return JsonConvert.SerializeObject(result, Formatting.Indented);

        }

        public static string ExportOldestBooks(BookShopContext context, DateTime date)
        {
            return "YOU ARE THE Best ";
        }
    }
}