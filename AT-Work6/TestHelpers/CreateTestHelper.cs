using AT_Work6.Model;
using AT_Work6.Service;
using Serilog;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AT_Work6.TestHelpers
{
    internal static class CreateTestHelper
    {
        public static async void CleanerAsync(BooksClient client, Book desBody)
        {
            var delREsponse = await client.DeleteGuidBook(desBody.id);

            delREsponse.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        }

        public static void MatchResponseValidation(Book book, Book desBody)
        {
            desBody.title.ShouldBe(book.title);
            desBody.author.ShouldBe(book.author);
            desBody.isbn.ShouldBe(book.isbn);
            desBody.publishedDate.ShouldBe(book.publishedDate);
        }

        public static async Task<HttpResponseMessage> CreateBookResponse(BooksClient client, Book book)
        {
            var response = await client.CreateBook(book);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);
            Log.Information("Assertion passed: StatusCode == 201");

            return response;
        }

        private static Book Deserializevalidation(string body)
        {
            var desBody = JsonSerializer.Deserialize<Book>(body);
            desBody.ShouldNotBeNull();
            desBody.id.ShouldNotBe(Guid.Empty);

            return desBody;
        }
    }
}
