using AT_Work6.Model;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace AT_Work6.TestHelpers
{
    internal class GetTestHelper
    {
        public static async Task<T> Read<T>(HttpResponseMessage response)
        {
            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(json);
        }
        public static void BookListFieldsValidation(List<Book> books)
        {
            books.ShouldNotBeNull();
            foreach (Book book in books)
            {
                book.id.ShouldNotBeNull();
                book.title.ShouldNotBeNull();
                book.isbn.ShouldNotBeNull();
                book.author.ShouldNotBeNull();
                book.publishedDate.ShouldNotBe(default);
            }
        }
        public static void BookFieldsValidation(Book bookWeCheck, Book bookShouldBe)
        {
            bookWeCheck.title.ShouldBe(bookShouldBe.title);
            bookWeCheck.author.ShouldBe(bookShouldBe.author);
            bookWeCheck.publishedDate.ShouldBe(bookShouldBe.publishedDate);
        }
    }
}
